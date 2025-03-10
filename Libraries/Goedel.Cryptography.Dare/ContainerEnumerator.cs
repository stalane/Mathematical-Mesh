﻿#region // Copyright - MIT License
//  © 2021 by Phill Hallam-Baker
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
#endregion

namespace Goedel.Cryptography.Dare;

/// <summary>
/// Enumerator that returns the raw, unencrypted container data.
/// </summary>
public class SequenceEnumeratorRaw : IEnumerator<DareEnvelope> {
    readonly Sequence container;
    readonly int lowIndex;
    readonly bool reverse;
    bool active;

    /// <summary>
    /// Gets the element in the collection at the current position of the enumerator.
    /// </summary>
    public DareEnvelope Current { get; private set; } = null;


    /// <summary>
    /// Create an enumerator for <paramref name="container"/>.
    /// </summary>
    /// <param name="lowIndex">The lowest index to be returned.</param>
    /// <param name="reverse">If true, enumeratre from the last item to <paramref name="lowIndex"/> (inclusive).
    /// otherwise, enumerate from <paramref name="lowIndex"/> to the first.</param>
    /// <param name="container">The container to enumerate.</param>
    public SequenceEnumeratorRaw(Sequence container, int lowIndex = 0, bool reverse = false) {
        this.container = container;
        this.lowIndex = lowIndex;
        this.reverse = reverse;
        Reset();
        }

    /// <summary>
    /// When called on an instance of this class, returns the instance. Thus allowing
    /// selectors to be used in sub classes.
    /// </summary>
    /// <returns>This instance</returns>
    public SequenceEnumeratorRaw GetEnumerator() => this;

    object IEnumerator.Current => Current;

    /// <summary>
    /// Dispose method.
    /// </summary>
    public void Dispose() {
        GC.SuppressFinalize(this);
        }

    /// <summary>
    /// Move to the next item in the enumeration.
    /// </summary>
    /// <returns>If true, the next item was found. Otherwise, the end of the enumeration 
    /// was reached.</returns>
    public bool MoveNext() {
        if (!reverse) {
            Current = container.ReadDirect();
            return Current != null;
            }
        if (!active) {
            return false;
            }
        Current = container.ReadDirectReverse();
        if (Current == null) {
            return false;
            }

        var header = Current.Header;
        return header.SequenceInfo.Index >= lowIndex;
        }

    /// <summary>
    /// Sets the enumerator to its initial position, which is before the first element 
    /// in the collection.
    /// </summary>
    public void Reset() {
        if (reverse) {
            active = container.MoveToLast();
            }
        else {

            container.MoveToIndex(lowIndex);
            }
        }
    }

/// <summary>
/// Enumerator for frames in a container beginning with frame 1.
/// </summary>
public class ContainerEnumerator : IEnumerator<SequenceFrameIndex> {
    readonly Sequence container;

    /// <summary>
    /// Gets the element in the collection at the current position of the enumerator.
    /// </summary>
    public SequenceFrameIndex Current { get; set; }



    /// <summary>
    /// Create an enumerator for <paramref name="container"/>.
    /// </summary>
    /// <param name="container">The container to enumerate.</param>
    public ContainerEnumerator(Sequence container) {
        this.container = container;
        Reset();
        }

    /// <summary>
    /// When called on an instance of this class, returns the instance. Thus allowing
    /// selectors to be used in sub classes.
    /// </summary>
    /// <returns>This instance</returns>
    public ContainerEnumerator GetEnumerator() => this;


    object IEnumerator.Current => Current;

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// 
    public void Dispose() {
        GC.SuppressFinalize(this);
        }

    /// <summary>
    /// Advances the enumerator to the next element of the collection.
    /// </summary>
    /// <returns><code>true</code> if the enumerator was successfully advanced to the next element; 
    /// <code>false</code> if the enumerator has passed the end of the collection.</returns>
    public bool MoveNext() {
        var Result = container.NextFrame();
        Current = Result ? container.GetSequenceFrameIndex() : null;
        return Result;
        }
    /// <summary>
    /// Sets the enumerator to its initial position, which is before the first element in the collection.
    /// </summary>
    public void Reset() {



        container.Start();

        // Hack - should pull the container frame index from the dictionary.

        Current = container.GetSequenceFrameIndex();
        }
    }
