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

using System.Collections.Generic;

using Goedel.Cryptography;
using Goedel.Test;
using Goedel.Utilities;

using Xunit;
//using Goedel.Cryptography.Framework;

namespace Goedel.XUnit;

public partial class TestGoedelCryptography {
    readonly List<TestVectorWrap> testVectorsAESWrap = new() {
        // 128 Key with 128 KEK
        new TestVectorWrap() {
            KEK = "000102030405060708090A0B0C0D0E0F".FromBase16(),
            Key = "00112233445566778899AABBCCDDEEFF".FromBase16(),
            Ciphertext = "1FA68B0A8112B447 AEF34BD8FB5A7B82 9D3E862371D2CFE5".FromBase16(),
            },
        // 128 Key with 192 KEK
        new TestVectorWrap() {
            KEK = "000102030405060708090A0B0C0D0E0F1011121314151617".FromBase16(),
            Key = "00112233445566778899AABBCCDDEEFF".FromBase16(),
            Ciphertext = "96778B25AE6CA435 F92B5B97C050AED2 468AB8A17AD84E5D".FromBase16(),
            },
        // 128 Key with 256 KEK
        new TestVectorWrap() {
            KEK = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F".FromBase16(),
            Key = "00112233445566778899AABBCCDDEEFF".FromBase16(),
            Ciphertext = "64E8C3F9CE0F5BA2 63E9777905818A2A 93C8191E7D6E8AE7".FromBase16(),
            },
        // 192 Key with 129 KEK
        new TestVectorWrap() {
            KEK = "000102030405060708090A0B0C0D0E0F1011121314151617".FromBase16(),
            Key = "00112233445566778899AABBCCDDEEFF0001020304050607".FromBase16(),
            Ciphertext = ("031D33264E15D332 68F24EC260743EDC" +
                    "E1C6C7DDEE725A93 6BA814915C6762D2").FromBase16(),
            },
        // 129 Key with 256 KEK
        new TestVectorWrap() {
            KEK = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F".FromBase16(),
            Key = "00112233445566778899AABBCCDDEEFF0001020304050607".FromBase16(),
            Ciphertext = ("A8F9BC1612C68B3F F6E6F4FBE30E71E4" +
                        "769C8B80A32CB895 8CD5D17D6B254DA1").FromBase16(),
            },
        // 256 Key with 256 KEK
        new TestVectorWrap() {
            KEK = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F".FromBase16(),
            Key = "00112233445566778899AABBCCDDEEFF000102030405060708090A0B0C0D0E0F".FromBase16(),
            Ciphertext = ("28C9F404C4B810F4 CBCCB35CFB87F826 3F5786E2D80ED326"
                        + "CBC7F0E71A99F43B FB988B9B7A02DD21").FromBase16(),
            },
        };

    [Fact]
    public void Test_Wrap_3394() {
        foreach (var TestVector in testVectorsAESWrap) {
            var KeyWrap = new KeyWrapRFC3394();

            TestVector.Verify(KeyWrap);
            }
        }
    }


public class TestVectorWrap {
    public byte[] KEK { get; set; }

    public byte[] Key { get; set; }

    public byte[] Ciphertext { get; set; }


    public void Verify(KeyWrap KeyWrap) {
        var Wrapped = KeyWrap.Wrap(KEK, Key);
        var Unwrapped = KeyWrap.Unwrap(KEK, Wrapped);

        Wrapped.TestEqual(Ciphertext);
        Unwrapped.TestEqual(Key);
        }


    }
