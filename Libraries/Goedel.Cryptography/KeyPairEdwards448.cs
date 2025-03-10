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

namespace Goedel.Cryptography;



/// <summary>
/// KeyPair binding for Ed448 signature and exchange.
/// </summary>
public class KeyPairEd448 : KeyPairEdwards {


    #region // Properties and fields 
    ///<summary>The implementation public key value</summary>
    public CurveEdwards448Public PublicKey { get; set; }

    ///<summary>The implementation private key value (if exportable)</summary>
    public CurveEdwards448Private PrivateKey { get; set; }

    ///<summary>The implementation public key value</summary>
    public override IKeyAdvancedPublic IKeyAdvancedPublic => PublicKey;

    ///<summary>The implementation private key value (if exportable)</summary>
    public override IKeyAdvancedPrivate IKeyAdvancedPrivate => PrivateKey;

    ///<summary>The private key parameters represented in PKIX form</summary>
    public override PKIXPrivateKeyECDH PKIXPrivateKeyECDH { get; }

    ///<summary>The public key parameters represented in PKIX form</summary>
    public override PKIXPublicKeyECDH PKIXPublicKeyECDH { get; }

    ///<summary>If true, the key only has access to public key values.</summary>
    public override bool PublicOnly => PrivateKey == null;

    /// <summary>The supported key uses (e.g. signing, encryption)</summary>
    public override KeyUses KeyUses { get; } = KeyUses.Any;

    /// <summary>
    /// The byte encoding of the public key
    /// </summary>
    public override byte[] PublicData => PublicKey.Encoding;

    ///<summary>The length of a signature in bytes.</summary> 
    public override int LengthSignature => 114;
    #endregion


    readonly byte[] encodedPrivateKey = null;

    /// <summary>
    /// Construct a KeyPairEd25519 instance for the specified key data in interchange 
    /// format. 
    /// </summary>
    /// <param name="key">The key data as specified in RFC8032.</param>
    /// <param name="keySecurity">The key security model.</param>
    /// <param name="keyUses">The permitted key uses.</param>
    /// <param name="cryptoAlgorithmID">Specifies the default algorithm variation for use
    /// in signature operations.</param>
    public KeyPairEd448(
                byte[] key,
                KeySecurity keySecurity = KeySecurity.Public,
                KeyUses keyUses = KeyUses.Any,
                CryptoAlgorithmId cryptoAlgorithmID = CryptoAlgorithmId.Default) {

        CryptoAlgorithmId = cryptoAlgorithmID.DefaultMeta(CryptoAlgorithmId.Ed448);
        base.KeySecurity = keySecurity;
        KeyUses = keyUses;
        if (keySecurity == KeySecurity.Public) {
            PublicKey = new CurveEdwards448Public(key);
            PKIXPublicKeyECDH = new PKIXPublicKeyEd448(PublicKey.Encoding);
            }
        else {
            encodedPrivateKey = key;
            var exportable = keySecurity.IsExportable();
            PrivateKey = new CurveEdwards448Private(key, exportable);
            PublicKey = PrivateKey.Public;
            PKIXPublicKeyECDH = new PKIXPublicKeyEd448(PublicKey.Encoding);
            if (exportable) {
                PKIXPrivateKeyECDH = new PKIXPrivateKeyEd448(key, PKIXPublicKeyECDH);
                }
            }

        }

    /// <summary>
    /// Construct a key via parameters presented to KDF-HMAC-SHA-2-512. 
    /// <para>The values <paramref name="ikm"/> and <paramref name="salt"/> are used to
    /// generate the key data value as specified by RFC8032.</para>
    /// </summary>
    /// <param name="ikm">The initial keying material.</param>
    /// <param name="salt">Salt value.</param>
    /// <param name="keySecurity">The key security model.</param>
    /// <param name="keyUses">The permitted key uses.</param>
    /// <param name="cryptoAlgorithmID">Specifies the default algorithm variation for use
    /// in signature operations.</param>
    public KeyPairEd448(byte[] ikm, byte[] salt,
                KeySecurity keySecurity = KeySecurity.Public,
                KeyUses keyUses = KeyUses.Any,
                CryptoAlgorithmId cryptoAlgorithmID = CryptoAlgorithmId.Default) :
                this(KeyDeriveHKDF.Derive(ikm, salt, null, 448, CryptoAlgorithmId.HMAC_SHA_2_512), keySecurity, keyUses, cryptoAlgorithmID) {
        }


    /// <summary>
    /// Construct a KeyPairEd25519 instance for a secret scalar. This is used to create
    /// private keys using cogeneration.
    /// </summary>
    /// <param name="privateKey">The secret scalar value.</param>
    /// <param name="keySecurity">The key security model.</param>
    /// <param name="keyUses">The permitted key uses.</param>
    /// <param name="cryptoAlgorithmID">Specifies the default algorithm variation for use
    /// in signature operations.</param>
    public KeyPairEd448(
                CurveEdwards448Private privateKey,
                KeySecurity keySecurity = KeySecurity.Bound,
                KeyUses keyUses = KeyUses.Any,
                CryptoAlgorithmId cryptoAlgorithmID = CryptoAlgorithmId.Default) {
        CryptoAlgorithmId = cryptoAlgorithmID.DefaultMeta(CryptoAlgorithmId.Ed448);
        PrivateKey = privateKey;
        PublicKey = privateKey.Public;
        PKIXPublicKeyECDH = new PKIXPublicKeyEd448(PublicKey.Encoding);
        KeySecurity = keySecurity;
        KeyUses = keyUses;
        if (keySecurity.IsExportable()) {
            PKIXPrivateKeyECDH = new PKIXPrivateKeyEd448(privateKey.Secret, PKIXPublicKeyECDH) {
                IsRecryption = privateKey.IsRecryption
                };
            }
        }



    /// <summary>
    /// Construct class from a public key value
    /// </summary>
    /// <param name="publicKey">The public key value</param>
    /// <param name="cryptoAlgorithmID">Specifies the default algorithm variation for use
    /// in signature operations.</param>
    /// <param name="keyUses">The permitted key uses.</param>
    public KeyPairEd448(IKeyAdvancedPublic publicKey,
                CryptoAlgorithmId cryptoAlgorithmID = CryptoAlgorithmId.Default,
                KeyUses keyUses = KeyUses.Any) {
        CryptoAlgorithmId = cryptoAlgorithmID == CryptoAlgorithmId.Default ?
            CryptoAlgorithmId.Ed448 : cryptoAlgorithmID;
        this.PublicKey = publicKey as CurveEdwards448Public;
        PKIXPublicKeyECDH = new PKIXPublicKeyEd448(this.PublicKey.Encoding);
        KeyUses = keyUses;
        }

    /// <summary>
    /// Generate a new private key.
    /// </summary>
    /// <param name="keyType">The key storage class.</param>
    /// <param name="keyUses">The permitted key uses</param>
    /// <param name="cryptoAlgorithmID">Cryptoraphic algorithm</param>
    /// <returns>The created key pair.</returns>
    public static KeyPairEd448 Generate(
                KeySecurity keyType = KeySecurity.Public,
                KeyUses keyUses = KeyUses.Any,
                CryptoAlgorithmId cryptoAlgorithmID = CryptoAlgorithmId.Default) =>
        new(Platform.GetRandomBits(448), keyType, keyUses, cryptoAlgorithmID);



    /// <summary>
    /// Factory method to produce a key pair from key parameters.
    /// </summary>
    /// <param name="privateKey">The private key</param>
    /// <param name="keySecurity">The key security model.</param>
    /// <param name="keyUses">The permitted key uses.</param>
    /// <returns>The key pair created.</returns>
    public override KeyPairAdvanced KeyPair(IKeyAdvancedPrivate privateKey,
                KeySecurity keySecurity = KeySecurity.Bound,
                KeyUses keyUses = KeyUses.Any) =>
        new KeyPairEd448((CurveEdwards448Private)privateKey, keySecurity, keyUses);

    /// <summary>
    /// Factory method to produce a key pair from implementation public key parameters
    /// </summary>
    /// <param name="publicKey">The public key</param>
    /// <param name="keyUses">The permitted key uses.</param>
    /// <returns>The key pair created.</returns>
    public override KeyPairAdvanced KeyPair(IKeyAdvancedPublic publicKey,
                KeyUses keyUses = KeyUses.Any) =>
        new KeyPairEd448((CurveEdwards448Public)publicKey, keyUses: keyUses);

    /// <summary>
    /// Returns a new KeyPair instance which only has the public values.
    /// </summary>
    /// <returns>The new keypair that contains only the public values.</returns>
    public override KeyPair KeyPairPublic() => new KeyPairEd448(PublicKey, keyUses: KeyUses);

    /// <summary>
    /// Persist the key to a key collection.
    /// </summary>
    /// <param name="keyCollection"></param>
    public override void Persist(KeyCollection keyCollection) {
        Assert.AssertTrue(PersistPending, CryptographicException.Throw);
        var pkix = PKIXPrivateKeyECDH ?? new PKIXPrivateKeyEd448(encodedPrivateKey, PKIXPublicKeyECDH) { };
        keyCollection.Persist(KeyIdentifier, pkix, KeySecurity.IsExportable());
        }

    #region // Operations
    /// <summary>
    /// Perform a Diffie Hellman Key Agreement to a private key
    /// </summary>
    /// <param name="publicKey">Public key parameters</param>
    /// <param name="carry">Carried result to add in to the agreement (for recryption)</param>
    /// <returns>The key agreement value ZZ</returns>
    ResultECDH Agreement(KeyPairEd448 publicKey, CurveEdwards448Result carry = null) {
        Assert.AssertTrue(KeyUses.HasFlag(KeyUses.Encrypt), CryptographicOperationNotSupported.Throw);

        CurveEdwards448 Agreement;
        if (carry == null) {
            Agreement = PrivateKey.Agreement(publicKey.PublicKey);
            }
        else {
            Agreement = PrivateKey.Agreement(publicKey.PublicKey, carry.AgreementEd448);
            }
        return new CurveEdwards448Result() { AgreementEd448 = Agreement };
        }

    /// <summary>
    /// Encrypt a bulk key.
    /// </summary>
    /// <returns>The encoder</returns>
    /// <param name="key">The key to encrypt.</param>
    /// <param name="ephemeral">The ephemeral key to use for the exchange (if used)</param>
    /// <param name="exchange">The private key to use for the exchange.</param>
    /// <param name="salt">Optional salt value for use in key derivation.</param>
    public override void Encrypt(byte[] key,
        out byte[] exchange,
        out KeyPair ephemeral,
        byte[] salt = null) => PublicKey.Agreement().Encrypt(key, out exchange, out ephemeral, salt);


    /// <summary>
    /// Perform a key exchange to encrypt a bulk or wrapped key under this one.
    /// </summary>
    /// <param name="encryptedKey">The encrypted session</param>
    /// <param name="ephemeral">Ephemeral key input (required for DH)</param>
    /// <param name="algorithmID">The algorithm to use.</param>
    /// <param name="partial">Partial key agreement carry in (for recryption)</param>
    /// <param name="salt">Optional salt value for use in key derivation. If specified
    /// must match the salt used to encrypt.</param>        
    /// <returns>The decoded data instance</returns>
    public override byte[] Decrypt(byte[] encryptedKey,
        KeyPair ephemeral = null,
        CryptoAlgorithmId algorithmID = CryptoAlgorithmId.Default,
        KeyAgreementResult partial = null, byte[] salt = null) {

        var KeyPairEd448 = ephemeral as KeyPairEd448;
        Assert.AssertNotNull(KeyPairEd448, KeyTypeMismatch.Throw);

        var Agreementx = Agreement(KeyPairEd448, partial as CurveEdwards448Result);
        return Agreementx.Decrypt(encryptedKey, ephemeral, partial, salt);
        }



    /// <summary>
    /// Sign a precomputed digest
    /// </summary>
    /// <param name="data">The data to sign.</param>
    /// <param name="algorithmID">The algorithm to use.</param>
    /// <param name="context">Additional data added to the signature scope
    /// for protocol isolation.</param>
    /// <returns>The signature data</returns>
    public override byte[] SignHash(
            byte[] data,
            CryptoAlgorithmId algorithmID = CryptoAlgorithmId.Default,
            byte[] context = null) {
        Assert.AssertTrue((KeyUses & KeyUses.Sign) != 0, CryptographicOperationNotSupported.Throw);

        algorithmID = algorithmID == CryptoAlgorithmId.Default ? CryptoAlgorithmId : algorithmID;
        if (algorithmID == CryptoAlgorithmId.Ed448ph) {
            using var shake256 = new SHAKE256(64 * 8);
            data = shake256.ComputeHash(data);
            }

        var dom4 = CurveEdwards448.Dom4(algorithmID, context);
        return PrivateKey.Sign(data, dom4);
        }

    /// <summary>
    /// Begin the process of signing the data <paramref name="data"/> according to the
    /// algorithm specifier <paramref name="algorithmID"/> and optional context value
    /// <paramref name="context"/>.
    /// </summary>
    /// <param name="data">The data to sign</param>
    /// <param name="algorithmID">The specific signature algorithm variant.</param>
    /// <param name="context">Optional context value.</param>
    /// <returns>The signature context.</returns>
    public override ThresholdSignatureEdwards SignHashThreshold(byte[] data,
    CryptoAlgorithmId algorithmID = CryptoAlgorithmId.Default,
        byte[] context = null) => new ThresholdSignatureEdwards448(PrivateKey);


    /// <summary>
    /// Verify a signature over the purported data digest.
    /// </summary>
    /// <param name="signature">The signature blob value.</param>
    /// <param name="algorithmID">The signature and hash algorithm to use.</param>
    /// <param name="context">Additional data added to the signature scope
    /// for protocol isolation.</param>
    /// <param name="digest">The digest value to be verified.</param>
    /// <returns>True if the signature is valid, otherwise false.</returns>
    public override bool VerifyHash(
        byte[] digest,
        byte[] signature,
        CryptoAlgorithmId algorithmID = CryptoAlgorithmId.Default,
            byte[] context = null) {
        Assert.AssertTrue((KeyUses & KeyUses.Sign) != 0, CryptographicOperationNotSupported.Throw);

        algorithmID = algorithmID == CryptoAlgorithmId.Default ? CryptoAlgorithmId : algorithmID;
        if (algorithmID == CryptoAlgorithmId.Ed448ph) {
            using var shake256 = new SHAKE256(64 * 8);
            digest = shake256.ComputeHash(digest);
            }

        var dom4 = CurveEdwards448.Dom4(algorithmID, context);
        return PublicKey.Verify(signature, digest, dom4);
        }


    #endregion
    }
