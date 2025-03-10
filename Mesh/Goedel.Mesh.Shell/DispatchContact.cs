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


namespace Goedel.Mesh.Shell;

public partial class Shell {

    ///// <summary>
    ///// Dispatch method
    ///// </summary>
    ///// <param name="options">The command line options.</param>
    ///// <returns>Mesh result instance</returns>
    //public override ShellResult ContactSelf(ContactSelf options) {
    //    var contextUser = GetContextUser(options);
    //    var file = options.File.Value;

    //    "Need to merge in the self contact info and label with a name.".TaskFunctionality(true);

    //    var entry = contextUser.AddFromFile(file, self: true);

    //    return new ResultEntry() {
    //        Success = true,
    //        CatalogEntry = entry
    //        };
    //    }

    /// <summary>
    /// Dispatch method
    /// </summary>
    /// <param name="options">The command line options.</param>
    /// <returns>Mesh result instance</returns>
    public override ShellResult ContactStatic(ContactStatic options) {
        var contextUser = GetContextUser(options);

        var uri = contextUser.ContactUri(false, null);

        var result = new ResultPublish() {
            Success = true,
            Uri = uri
            };
        return result;
        }

    /// <summary>
    /// Dispatch method
    /// </summary>
    /// <param name="options">The command line options.</param>
    /// <returns>Mesh result instance</returns>
    public override ShellResult ContactDynamic(ContactDynamic options) {
        var contextUser = GetContextUser(options);
        var expiry = DateTime.Now.AddTicks(MeshConstants.DayInTicks);

        var uri = contextUser.ContactUri(true, expiry);

        var result = new ResultPublish() {
            Success = true,
            Uri = uri
            };
        return result;
        }

    /// <summary>
    /// Dispatch method
    /// </summary>
    /// <param name="options">The command line options.</param>
    /// <returns>Mesh result instance</returns>
    public override ShellResult ContactExchange(ContactExchange options) {
        var contextUser = GetContextUser(options);
        var recipient = options.Uri.Value;

        var entry = contextUser.ContactExchange(recipient, true, out var message);

        return new ResultEntrySent() {
            Success = true,
            CatalogEntry = entry,
            Message = message
            };
        }

    /// <summary>
    /// Dispatch method
    /// </summary>
    /// <param name="options">The command line options.</param>
    /// <returns>Mesh result instance</returns>
    public override ShellResult ContactFetch(ContactFetch options) {
        var contextUser = GetContextUser(options);
        var recipient = options.Uri.Value;

        var entry = contextUser.ContactExchange(recipient, false, out _);

        return new ResultEntry() {
            Success = true,
            CatalogEntry = entry
            };

        }

    /// <summary>
    /// Dispatch method
    /// </summary>
    /// <param name="options">The command line options.</param>
    /// <returns>Mesh result instance</returns>
    public override ShellResult ContactImport(ContactImport options) {
        var contextUser = GetContextUser(options);
        var file = options.File.Value;

        var entry = contextUser.AddContactFromFile(file);

        return new ResultEntry() {
            Success = true,
            CatalogEntry = entry
            };
        }

    /// <summary>
    /// Dispatch method
    /// </summary>
    /// <param name="options">The command line options.</param>
    /// <returns>Mesh result instance</returns>
    public override ShellResult ContactGet(ContactGet options) {
        var contextUser = GetContextUser(options);
        var identifier = options.Identifier.Value;

        var result = contextUser.GetContact(identifier);

        return new ResultEntry() {
            Success = result != null,
            CatalogEntry = result
            };
        }

    /// <summary>
    /// Dispatch method
    /// </summary>
    /// <param name="options">The command line options.</param>
    /// <returns>Mesh result instance</returns>
    public override ShellResult ContactDelete(ContactDelete options) {
        var contextUser = GetContextUser(options);
        var key = options.Identifier.Value;

        var transaction = contextUser.TransactBegin();
        var catalog = transaction.GetCatalogContact();
        var result = catalog.Get(key);
        result.AssertNotNull(EntryNotFound.Throw, key);
        transaction.CatalogDelete(catalog, result);
        transaction.Transact();

        return new Result() {
            Success = true
            };
        }


    /// <summary>
    /// Dispatch method
    /// </summary>
    /// <param name="options">The command line options.</param>
    /// <returns>Mesh result instance</returns>
    public override ShellResult ContactDump(ContactDump options) {
        var contextUser = GetContextUser(options);
        var result = new ResultDump() {
            Success = true,
            CatalogedEntries = new List<CatalogedEntry>()
            };


        var catalog = contextUser.GetStore(CatalogContact.Label) as CatalogContact;
        foreach (var entry in catalog) {
            result.CatalogedEntries.Add(entry);
            }

        return result;
        }
    }
