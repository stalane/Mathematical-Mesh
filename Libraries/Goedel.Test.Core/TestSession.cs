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

namespace Goedel.Test.Core;

/// <summary>
/// Test session, a specialization of the serialized class in which the
/// request and response messages are captured.
/// </summary>
public partial class TestSession : JpcSessionSerialized {

    //public static JpcSession JpcSessionFactory(JpcCredential jpcCredential) =>
    //        new TestSession(jpcCredential as JpcCredentialTest);


    public object Machine;

    public List<Trace> MeshProtocolMessages;


    static ICredential GetPublic(ICredential credential) {
        return credential switch {
            MeshCredentialPrivate meshCredentialPrivate =>
                        meshCredentialPrivate.GetMeshCredentialPublic(),
            MeshKeyCredentialPrivate meshKeyCredentialPrivate =>
                        meshKeyCredentialPrivate.GetMeshKeyCredentialPublic(),
            KeyCredentialPrivate keyCredentialPrivate =>
                        keyCredentialPrivate.GetKeyCredentialPublic(),
            _ => credential
            };


        }

    /// <summary>
    /// Create a remote session with authentication under the
    /// specified credential.
    /// </summary>
    /// <param name="host">The host implementation</param>
    /// <param name="Domain">Portal address</param>
    /// <param name="Account">User account</param>
    /// <param name="UDF">Authentication key identifier.</param>
    public TestSession(JpcInterface host, ICredential credential,
                List<Trace> meshProtocolMessages, object machine) :
                    base(host, GetPublic(credential)) {
        Machine = machine;
        MeshProtocolMessages = meshProtocolMessages;
        }


    /// <summary>
    /// Post a request and retrieve the response.
    /// </summary>
    /// <param name="data">StreamBuffer object containing JSON encoded request.</param>
    /// <returns>StreamBuffer object containing JSON encoded response.</returns>
    public override Stream Post(MemoryStream data, JsonObject Request) {
        var requestBytes = data.ToArray();

        var JSONReader = new JsonReader(requestBytes);
        var result = Host.Dispatch(this, JSONReader);
        var responseBytes = result.GetBytes();

        var trace = new Trace(requestBytes, responseBytes, Host);

        MeshProtocolMessages.Add(trace);
        return new MemoryStream(responseBytes);
        }

    }
