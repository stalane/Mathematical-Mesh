
### Phase 1

The manufacturer preconfigures the device


~~~~
<div="terminal">
<cmd>Maker> meshman device preconfig
<rsp>Device UDF: MBYN-VC7T-AIEX-KCK2-WQLP-4B52-3GWY
File: EDRL-4MZP-3OUD-OZBS-SPZN-7OSO-OM.medk
</div>
~~~~

This results in the creation of a primary secret which is used to compute a ProfileDevice
and corresponding connection records signed by the manufacturer's administrator key.

The data is combined to create a DevicePreconfiguration record that is provisioned to
the firmware of the device being preconfigured.

~~~~
{
  "DevicePreconfigurationPrivate":{
    "EnvelopedProfileDevice":[{
        "EnvelopeId":"MBYN-VC7T-AIEX-KCK2-WQLP-4B52-3GWY",
        "dig":"S512",
        "ContentMetaData":"ewogICJVbmlxdWVJZCI6ICJNQllOLVZDN1QtQU
  lFWC1LQ0syLVdRTFAtNEI1Mi0zR1dZIiwKICAiTWVzc2FnZVR5cGUiOiAiUHJvZml
  sZURldmljZSIsCiAgImN0eSI6ICJhcHBsaWNhdGlvbi9tbW0vb2JqZWN0IiwKICAi
  Q3JlYXRlZCI6ICIyMDIyLTA1LTAzVDE2OjQ3OjU3WiJ9"},
      "ewogICJQcm9maWxlRGV2aWNlIjogewogICAgIlByb2ZpbGVTaWduYXR1cm
  UiOiB7CiAgICAgICJVZGYiOiAiTUJZTi1WQzdULUFJRVgtS0NLMi1XUUxQLTRCNTI
  tM0dXWSIsCiAgICAgICJQdWJsaWNQYXJhbWV0ZXJzIjogewogICAgICAgICJQdWJs
  aWNLZXlFQ0RIIjogewogICAgICAgICAgImNydiI6ICJFZDQ0OCIsCiAgICAgICAgI
  CAiUHVibGljIjogIlZMOFZVYkNZX0YzaWV5TE52eG94UlVDYlV1Qkd5Ul8wX1BxaV
  NEZmhyUnpTektSM3JKVGEKICAyU0w0enRTaHROdVVRUVZEQS1XSERkT0EifX19LAo
  gICAgIkVuY3J5cHRpb24iOiB7CiAgICAgICJVZGYiOiAiTUQ3VC03UERWLURINFIt
  T0U0QS1TSUNTLTdHVzYtVVlZUiIsCiAgICAgICJQdWJsaWNQYXJhbWV0ZXJzIjoge
  wogICAgICAgICJQdWJsaWNLZXlFQ0RIIjogewogICAgICAgICAgImNydiI6ICJYND
  Q4IiwKICAgICAgICAgICJQdWJsaWMiOiAiUXFyRmYwUW1RMklZYVlCbDNQb3g5LW4
  tbDJTMW1GQVpQZjNza3FMWUVoRVhIcnB0ajNwRQogIEJSaE1BVUVUSmVRYW1ZQ25x
  SUp1N3FJQSJ9fX0sCiAgICAiU2lnbmF0dXJlIjogewogICAgICAiVWRmIjogIk1CR
  zMtQjZVTy1VQ01SLUI3NUstQU9MTi1VNFlMLUZJWlMiLAogICAgICAiUHVibGljUG
  FyYW1ldGVycyI6IHsKICAgICAgICAiUHVibGljS2V5RUNESCI6IHsKICAgICAgICA
  gICJjcnYiOiAiRWQ0NDgiLAogICAgICAgICAgIlB1YmxpYyI6ICJtSTZWZ2x3ekZ6
  UERFcExfWi1LVk9yWVJBelg0cHpYalpwcVFNdjczakptSWo1Q1dtNmNXCiAgT3Z5T
  TJFY090OEF3NFhTTDBXbjUwS2tBIn19fSwKICAgICJBdXRoZW50aWNhdGlvbiI6IH
  sKICAgICAgIlVkZiI6ICJNQUU1LUJRNkwtVldXQi03NUdULUZQTU0tVkJPTi0zRUR
  aIiwKICAgICAgIlB1YmxpY1BhcmFtZXRlcnMiOiB7CiAgICAgICAgIlB1YmxpY0tl
  eUVDREgiOiB7CiAgICAgICAgICAiY3J2IjogIlg0NDgiLAogICAgICAgICAgIlB1Y
  mxpYyI6ICJCSURWckJWZVI0dXEzUHNoWktSeUdkemdCTjRzM3B2bTZ5MXZadTRBU3
  dwbG5aT1F4Si14CiAgT0lSa1pha2x5U2s2S2dxOXE2Y011dzhBIn19fX19",
      {
        "signatures":[{
            "alg":"S512",
            "kid":"MBYN-VC7T-AIEX-KCK2-WQLP-4B52-3GWY",
            "signature":"mp5Ofh5Np7Gchj8pdjwhqklVREaqGs_OxKxCiANB
  thH9Cf59mncP5Hoe_11c-ewZubHbqfqvJ2aA_X34eiRktQgUwbqwuE1QFfaKthSGA
  boV5agneQSi32-KeG8r1aA63t8vx76QAQ0whbvineCyjT0A"}
          ],
        "PayloadDigest":"KiSCaDqYWvlb00VZtcCtZwNpXA-7VbQE8xshUBrz
  chnQb3HUaHxUCO3121yN4JwkKCy748TvYloewv9vcWujpg"}
      ],
    "EnvelopedConnectionDevice":[{
        "dig":"S512",
        "ContentMetaData":"ewogICJNZXNzYWdlVHlwZSI6ICJDb25uZWN0aW
  9uRGV2aWNlIiwKICAiY3R5IjogImFwcGxpY2F0aW9uL21tbS9vYmplY3QiLAogICJ
  DcmVhdGVkIjogIjIwMjItMDUtMDNUMTY6NDc6NTdaIn0"},
      "ewogICJDb25uZWN0aW9uRGV2aWNlIjogewogICAgIkF1dGhlbnRpY2F0aW
  9uIjogewogICAgICAiVWRmIjogIk1EN1QtN1BEVi1ESDRSLU9FNEEtU0lDUy03R1c
  2LVVZWVIiLAogICAgICAiUHVibGljUGFyYW1ldGVycyI6IHsKICAgICAgICAiUHVi
  bGljS2V5RUNESCI6IHsKICAgICAgICAgICJjcnYiOiAiWDQ0OCIsCiAgICAgICAgI
  CAiUHVibGljIjogIlFxckZmMFFtUTJJWWFZQmwzUG94OS1uLWwyUzFtRkFaUGYzc2
  txTFlFaEVYSHJwdGozcEUKICBCUmhNQVVFVEplUWFtWUNucUlKdTdxSUEifX19LAo
  gICAgIlNpZ25hdHVyZSI6IHsKICAgICAgIlVkZiI6ICJNQkczLUI2VU8tVUNNUi1C
  NzVLLUFPTE4tVTRZTC1GSVpTIiwKICAgICAgIlB1YmxpY1BhcmFtZXRlcnMiOiB7C
  iAgICAgICAgIlB1YmxpY0tleUVDREgiOiB7CiAgICAgICAgICAiY3J2IjogIkVkND
  Q4IiwKICAgICAgICAgICJQdWJsaWMiOiAibUk2Vmdsd3pGelBERXBMX1otS1ZPcll
  SQXpYNHB6WGpacHFRTXY3M2pKbUlqNUNXbTZjVwogIE92eU0yRWNPdDhBdzRYU0ww
  V241MEtrQSJ9fX0sCiAgICAiRW5jcnlwdGlvbiI6IHsKICAgICAgIlVkZiI6ICJNR
  DdULTdQRFYtREg0Ui1PRTRBLVNJQ1MtN0dXNi1VWVlSIiwKICAgICAgIlB1YmxpY1
  BhcmFtZXRlcnMiOiB7CiAgICAgICAgIlB1YmxpY0tleUVDREgiOiB7CiAgICAgICA
  gICAiY3J2IjogIlg0NDgiLAogICAgICAgICAgIlB1YmxpYyI6ICJRcXJGZjBRbVEy
  SVlhWUJsM1BveDktbi1sMlMxbUZBWlBmM3NrcUxZRWhFWEhycHRqM3BFCiAgQlJoT
  UFVRVRKZVFhbVlDbnFJSnU3cUlBIn19fX19",
      {
        "signatures":[{
            "alg":"S512",
            "kid":"MAQX-NI73-XDNK-AUOJ-STUL-FKKV-HTDJ",
            "signature":"zfJHzUqTFSblQNq2_XLv8Jf0EmLIuIvpRCDveNq1
  uNjN6nbwGPkjQuvi5rjAcWp_nEUHGiBX6pIAyvz5CkXMIz4kjapdVVZi0CNBsZAD5
  _03TE6k1g3XtNmEWDSHRYjnX9UEYk8FzZ0qOI32YcYm7jsA"}
          ],
        "PayloadDigest":"HyZgOTfCxHoR-CB8P-7799H00mMkCKqotq73sOky
  noOQgmE-kMNbNw51ssw4Y1WQURo62rSVDMiHTwE9XfDQJw"}
      ],
    "EnvelopedConnectionService":[{
        "dig":"S512",
        "ContentMetaData":"ewogICJNZXNzYWdlVHlwZSI6ICJDb25uZWN0aW
  9uU2VydmljZSIsCiAgImN0eSI6ICJhcHBsaWNhdGlvbi9tbW0vb2JqZWN0IiwKICA
  iQ3JlYXRlZCI6ICIyMDIyLTA1LTAzVDE2OjQ3OjU3WiJ9"},
      "ewogICJDb25uZWN0aW9uU2VydmljZSI6IHsKICAgICJBdXRoZW50aWNhdG
  lvbiI6IHsKICAgICAgIlVkZiI6ICJNRDdULTdQRFYtREg0Ui1PRTRBLVNJQ1MtN0d
  XNi1VWVlSIiwKICAgICAgIlB1YmxpY1BhcmFtZXRlcnMiOiB7CiAgICAgICAgIlB1
  YmxpY0tleUVDREgiOiB7CiAgICAgICAgICAiY3J2IjogIlg0NDgiLAogICAgICAgI
  CAgIlB1YmxpYyI6ICJRcXJGZjBRbVEySVlhWUJsM1BveDktbi1sMlMxbUZBWlBmM3
  NrcUxZRWhFWEhycHRqM3BFCiAgQlJoTUFVRVRKZVFhbVlDbnFJSnU3cUlBIn19fX1
  9",
      {
        "signatures":[{
            "alg":"S512",
            "kid":"MAQX-NI73-XDNK-AUOJ-STUL-FKKV-HTDJ",
            "signature":"jz3fZDdd_A7bJz1NB0tsWddko2qqhwbn4gD1P5j-
  PvvGwSKURGje011GVsDiz4ocLsE2Mx9NstqA_JN6vICyjOzRDwdpQxr4an2vj3EsS
  SoT3oeK7poau4giPRttjqMDn0psqA2INmreu-fkEkL8MAMA"}
          ],
        "PayloadDigest":"78qxV-6PGxtC-596T5a6y2LS8ivzaNvxnRfUhS8B
  FMhKlPrsGB32chhVwv4jvyXUbSkzPbhT9XI3Y2pWWKo2hg"}
      ],
    "PrivateKey":{
      "PrivateKeyUDF":{
        "PrivateValue":"ZAAQ-AHCC-PWZG-EQXO-M4QM-OYWT-HDPG-DQ3D-HMI
G-ABHP-LED2-KPAG-LOTB-QMJ7",
        "KeyType":"MeshProfileDevice"}},
    "ConnectUri":"mcu://maker@example.com/EDRL-4MZP-3OUD-OZBS-SPZN-
7OSO-OM"}}
~~~~

An EARL is created specifying the means by which an administration device can acquire the
information required to complete a connection to the device:

~~~~
QR = {Connect.ConnectEARL}
~~~~

The preconfigured ProfileDevice is encrypted under the encryption key and published to
the location key derived from the EARL.


### Phase 2 & 3

The administration device scans the QR code and obtains the Device Description using
the Claim operation as shown in section $$$$. The administration device creates the 
ActivationDevice and CatalogedDevice records and populates the service as before.


~~~~
<div="terminal">
<cmd>Alice> meshman account connect ^
    mcu://maker@example.com/EDRL-4MZP-3OUD-OZBS-SPZN-7OSO-OM /web
</div>
~~~~

### Phase 4

The device polls the publication service until a claim message is returned.


~~~~
<div="terminal">
<cmd>Alice4> meshman device complete
<rsp>   Device UDF = MBYN-VC7T-AIEX-KCK2-WQLP-4B52-3GWY
   Account = alice@example.com
   Account UDF = MARP-HH6B-D2JW-WLQ6-RWPQ-2SLS-AI2V
</div>
~~~~

### Phase 5

Having been advised that an account has published a claim to bind to it, the device
posts a connection Complete request to the specified account and completes the
connection process as before.

