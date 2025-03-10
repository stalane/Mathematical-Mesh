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


using Microsoft.Extensions.Logging;

namespace Goedel.Mesh.Server;

/// <summary>
/// Account privileges
/// </summary>
[Flags]
public enum AccountPrivilege {
    ///<summary>Unbind the account from the service permitting it to be deleted.</summary> 
    Unbind = 0b0000_0001,
    ///<summary>Operations requiring a connection to the account</summary> 
    Connected = 0b0000_0010,
    ///<summary>Device in a pending state, authenticated by key alone.</summary> 
    Pending = 0b0000_0100,
    ///<summary>???</summary> 
    Device = 0b0000_1000,
    ///<summary>???</summary> 
    Post = 0b0001_0000,
    ///<summary>???</summary> 
    Local = 0b0010_0000,
    ///<summary>???</summary> 
    Operate = 0b0100_0000,

    ///<summary>All privileges.</summary> 
    All = 0xFFF
    }


/// <summary>
/// Account handle, implements the security monitor mediating all access to 
/// account stores.
/// </summary>
public class AccountHandleLocked : Disposable {

    #region // Public and private properties
    ///<summary>The service provider.</summary> 
    public string Provider;

    ///<summary>The privilege granted to the client.</summary> 
    public AccountPrivilege AccountPrivilege { get; init; }

    /// <summary>
    /// The account description. This is only accessible through the account handle.
    /// </summary>
    protected AccountContext AccountContext { get; }


    ///<summary>Convenience accessor for the account address</summary> 
    public string AccountAddress => AccountContext.AccountEntry.ProfileUdf;

    ///<summary>The account entry in the service catalog.</summary> 
    protected AccountUser AccountUser => AccountContext.AccountEntry as AccountUser;

    ///<summary>The account profile</summary> 
    public ProfileAccount ProfileAccount => AccountUser.GetProfileAccount();

    ///<summary>The directory in which all the account data is stored.</summary> 
    string Directory => AccountContext.Directory;

    ///<summary>The enveloped catalog device.</summary> 
    public Enveloped<CatalogedDevice> EnvelopedCatalogedDevice =>
                AccountContext.AccessCapability?.EnvelopedCatalogedDevice;

    ///<summary>Digest of the cataloged device envelope, used to detect changes.</summary> 
    public string CatalogedDeviceDigest =>
                        AccountContext.AccessCapability?.CatalogedDeviceDigest;

    Dictionary<string, Sequence> DictionarySequences { get; set; }

    private ILogger Logger { get; }
    #endregion
    #region // Disposal

    ///<inheritdoc/>
    protected override void Disposing() {
        //Screen.Write($"AccountContext close {AccountContext?.AccountEntry?.AccountAddress}");

        if (DictionarySequences != null) {
            foreach (var sequenceEntry in DictionarySequences) {
                //Screen.WriteLine($"Delete Sequence {sequenceEntry.Key}");
                sequenceEntry.Value.Dispose();
                }
            }


        //Logger.LockRelease(AccountContext.AccountEntry.AccountAddress);
        //System.Threading.Monitor.Exit(AccountContext.AccountEntry);
        AccountContext?.Dispose();

        //Screen.WriteLine($"Deleted context");
        }

    #endregion
    #region // Constructors

    /// <summary>
    /// Return an account handle for the account context <paramref name="accountContext"/>.
    /// </summary>
    /// <param name="accountContext">The account context.</param>
    /// <param name="logger">Loger to output context to.</param>
    public AccountHandleLocked(AccountContext accountContext, ILogger logger) {
        //Screen.WriteLine($"AccountContext open {accountContext?.AccountEntry?.AccountAddress}");
        Logger = logger;


        accountContext.AssertNotNull(NYI.Throw);
        AccountContext = accountContext;

        }

    #endregion
    #region // Methods

    Sequence MakeNewSequence(string label, List<DareEnvelope> envelopes) {
        var fileName = Store.FileName(Directory, label);
        var sequence = Sequence.MakeNewSequence(fileName, null, envelopes);
        //Screen.Write($"Create {label}");
        DictionarySequences ??= new();
        DictionarySequences.Add(label, sequence);
        return sequence;
        }

    /// <summary>
    /// Open the sequence <paramref name="label"/>.
    /// </summary>
    /// <param name="label">The sequence to open.</param>
    /// <param name="existing">If true the sequence MUST exist.</param>
    /// <returns>The sequence class.</returns>
    public Sequence GetSequence(string label, bool existing = true) {
        //Screen.Write($"Try open {label}");

        if (label == CatalogAccess.Label) {
            var catalog = AccountContext.GetCatalogCapability();
            //Screen.WriteLine($" [Access]");
            return catalog.Container;
            }
        DictionarySequences ??= new();
        if (DictionarySequences.TryGetValue(label, out var sequence)) {
            //Screen.WriteLine($" [cached]");
            return sequence;
            }
        var fileName = Store.FileName(Directory, label);

        if (existing) {
            sequence = Sequence.OpenExisting(fileName, FileStatus.ConcurrentLocked, decrypt: false);
            //Screen.WriteLine($" Open {sequence != null} {sequence.DisposeJBCDStream != null}");
            }
        else {
            sequence = Sequence.Open(fileName, FileStatus.ConcurrentLocked, decrypt: false, create: false);
            //Screen.WriteLine($" OpenCreate {sequence != null} {sequence.DisposeJBCDStream != null}");
            }
        DictionarySequences.Add(label, sequence);

        return sequence;
        }

    /// <summary>
    /// Return the status of the catalog <paramref name="label"/>.
    /// </summary>
    /// <param name="label">Catalog to return status on.</param>
    /// <returns>The status vector.</returns>
    public ContainerStatus GetStatusStore(string label) {
        var sequence = GetSequence(label, false);
        return sequence == null ? null : new ContainerStatus() {
            // Bug: This should populate the TreeDigest
            Digest = sequence.HeaderFinal?.TreeDigest ?? sequence.TrailerLast?.TreeDigest,
            Index = (int)sequence.FrameCount,
            Container = label
            };
        }


    /// <summary>
    /// Return the publication catalog. This is a catalog that the service MUST have
    /// read access to. Not clear that the clients need access though.
    /// </summary>
    /// <returns></returns>
    public CatalogAccess GetCatalogCapability() =>
        new(Directory, keyCollection: AccountContext.KeyCollection);

    /// <summary>
    /// Return the publication catalog. This is a catalog that the service MUST have
    /// read access to. Not clear that the clients need access though.
    /// </summary>
    /// <returns></returns>
    public CatalogPublication GetCatalogPublication() =>
        new(Directory);


    /// <summary>
    /// Post a message to the spool associated with the account. This is the only operation
    /// that is supported for a device that is not connected to the account profile.
    /// </summary>
    /// <param name="envelope">The message to post.</param>
    public void PostInbound(DareEnvelope envelope) {

        "Implement fine grain access control".TaskFunctionality(suppress: Assert.HaltPhase1);

        //AccountPrivilege.HasFlag(AccountPrivilege.Post).AssertTrue(NotAuthorized.Throw);

        //using var container = new Spool(Directory, SpoolInbound.Label);
        //container.Add(dareMessage);


        var sequence = GetSequence(SpoolInbound.Label);
        sequence.Append(envelope);
        }

    /// <summary>
    /// Post a message to the spool associated with the account. This is the only operation
    /// that is supported for a device that is not connected to the account profile.
    /// </summary>
    /// <param name="envelope">The message to post.</param>
    public void PostLocal(DareEnvelope envelope) {
        //AccountPrivilege.HasFlag(AccountPrivilege.Local).AssertTrue(NotAuthorized.Throw);

        "Implement fine grain access control".TaskFunctionality(suppress: Assert.HaltPhase1);

        //using var container = new Spool(Directory, SpoolLocal.Label);
        //container.Add(envelope);


        var sequence = GetSequence(SpoolLocal.Label);
        sequence.Append(envelope);
        }

    /// <summary>
    /// Append the envelopes <paramref name="envelopes"/> to the store named
    /// <paramref name="label"/>.
    /// </summary>
    /// <param name="label">The store to add the envelopes to.</param>
    /// <param name="envelopes">The envelopes to append.</param>
    public void StoreAppend(string label, List<DareEnvelope> envelopes) {
        "Implement fine grain access control".TaskFunctionality(suppress: Assert.HaltPhase1);



        envelopes.AssertNotNull(Internal.Throw);
        if (envelopes.Count == 0) {
            return;
            }


        //foreach (var envelope in envelopes) {
        //    envelope.Header.PayloadDigest.AssertNotNull(NYI.Throw);
        //    envelope.Header.TreeDigest.AssertNotNull(NYI.Throw);
        //    }


        if (envelopes[0].Header.SequenceInfo.Index == 0) {
            MakeNewSequence(label, envelopes);
            }
        else {
            var sequence = GetSequence(label);
            sequence.Append(envelopes);
            }
        }

    void AddStatusStore(List<ContainerStatus> statuses, string label) {
        var result = GetStatusStore(label);
        if (result != null) {
            statuses.Add(result);
            }
        }



    /// <summary>
    /// Obtain status values for all the stores associated with an account.
    /// </summary>
    /// <returns>The list of container status entries.</returns>
    public List<ContainerStatus> GetContainerStatuses() {
        try {
            var result = new List<ContainerStatus>();
            AddStatusStore(result, SpoolInbound.Label);
            AddStatusStore(result, SpoolOutbound.Label);
            AddStatusStore(result, SpoolLocal.Label);
            AddStatusStore(result, CatalogAccess.Label);

            switch (AccountContext.AccountEntry) {
                //case AccountGroup _: {
                //    result.Add(GetStatusStore(CatalogMember.Label));
                //    break;
                //    }

                case Goedel.Mesh.Server.AccountUser _: {
                        AddStatusStore(result, CatalogCredential.Label);
                        AddStatusStore(result, CatalogDevice.Label);
                        AddStatusStore(result, CatalogContact.Label);
                        AddStatusStore(result, CatalogApplication.Label);
                        AddStatusStore(result, CatalogPublication.Label);
                        AddStatusStore(result, CatalogBookmark.Label);
                        AddStatusStore(result, CatalogTask.Label);
                        break;
                        }
                }

            return result;
            }
        catch {
            throw new NYI();
            }
        }

    /// <summary>
    /// Return the message with identifier <paramref name="messageId"/> from the local spool.
    /// </summary>
    /// <param name="messageId">Message to return.</param>
    /// <returns>The message (if found).</returns>
    public DareEnvelope GetLocal(string messageId) {
        var envelopeId = Message.GetEnvelopeId(messageId);

        using var spoolLocal = GetSequence(SpoolLocal.Label);

        foreach (var message in spoolLocal.Select(0, reverse: true)) {
            if (message?.EnvelopeId == envelopeId) {
                return message;
                }

            }
        return null;
        }


    #endregion
    }
