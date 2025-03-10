

# device

~~~~
<div="helptext">
<over>
device    Device management commands.
    accept   Accept a pending connection
    auth   Authorize device to use application
    complete   Complete a pending request
    delete   Remove device from device catalog
    install   Connect by means of a connection URI from an administration device.
    join   Connect by means of a connection URI from an administration device.
    list   List devices in the device catalog
    pending   Get list of pending connection requests
    preconfig   Generate new device profile and publish as an EARL
    reject   Reject a pending connection
    request   Connect to an existing profile registered at a portal
<over>
</div>
~~~~

# device accept

~~~~
<div="helptext">
<over>
accept   Accept a pending connection
       Fingerprint of connection to accept
       Device identifier
    /auth   (De)Authorize the specified function on the device
    /root   Device as super administration device
    /admin   Device as administration device
    /message   Authorize rights for Mesh messaging
    /web   Authorize rights for Mesh messaging and Web.
    /device   Device restrictive access
    /threshold   Authorize threshold rights for Mesh messaging and Web.
    /ssh   Authorize rights for specified SSH account
    /email   Authorize rights for specified smtp email account
    /member   Authorize member rights for specified Mesh group
    /group   Authorize group administrator rights for specified Mesh group
    /null   Do not authorize any device rights at all (cannot be used with any rights grant))
    /account   Account identifier (e.g. alice@example.com) or profile fingerprint
    /local   Local name for account (e.g. personal)
    /sync   If true, attempt to synchronize the account to the service before operation
    /auto   If true, automatically approve pending requests with prior authorization.
    /verbose   Verbose reports (default)
    /report   Report output (default)
    /json   Report output in JSON format
<over>
</div>
~~~~

The `device accept` command accepts the specified connection request.

The command must specify the connection identifier of the request 
being accepted. The connection identifier may be abbreviated provided that
this uniquely identifies the connection being accepted and that at least 
four characters are given.

The `/id` option may be used to specify a friendly name for the device.

The authorizations to be granted to the device may be specified using
the same syntax as for the `device auth` command with the default authorization
being that all authorizations are denied.


~~~~
<div="terminal">
<cmd>Alice> meshman device accept CI4K-GV7W-VRTX-QTNB-ZNBG-652H-GMWJ /message /web
<rsp></div>
~~~~



# device auth

~~~~
<div="helptext">
<over>
auth   Authorize device to use application
       Device identifier
    /auth   (De)Authorize the specified function on the device
    /root   Device as super administration device
    /admin   Device as administration device
    /message   Authorize rights for Mesh messaging
    /web   Authorize rights for Mesh messaging and Web.
    /device   Device restrictive access
    /threshold   Authorize threshold rights for Mesh messaging and Web.
    /ssh   Authorize rights for specified SSH account
    /email   Authorize rights for specified smtp email account
    /member   Authorize member rights for specified Mesh group
    /group   Authorize group administrator rights for specified Mesh group
    /null   Do not authorize any device rights at all (cannot be used with any rights grant))
    /account   Account identifier (e.g. alice@example.com) or profile fingerprint
    /local   Local name for account (e.g. personal)
    /sync   If true, attempt to synchronize the account to the service before operation
    /auto   If true, automatically approve pending requests with prior authorization.
    /verbose   Verbose reports (default)
    /report   Report output (default)
    /json   Report output in JSON format
<over>
</div>
~~~~

The `device auth` command changes the set of authorizations given to the
specified device, adding or removing authorizations according to the 
flags specified on the command line.

The parameter specifies the device being configured by means of either
the UDF of the device profile or the device identifier.

The `/id` option may be used to specify a friendly name for the device.

Specifying the `/all` option causes the device to be granted all the 
available device authorizations except for those explicitly denied 
by means of a negative authorization grant (e.g. `/nobookmark`).

Specifying the `/noall` option causes the device to be granted no 
available device authorizations except for those explicitly granted 
by means of a positive authorization grant (e.g. `/bookmark`).

If neither the `/all` option or the `/noall` option is specified, the 
device authorizations remain unchanged except where explicitly 
granted or denied.

The following authorizations may be granted or denied:

* `bookmark`: Authorize response to confirmation requests
* `calendar`: Authorize access to calendar catalog
* `contact`: Authorize access to contacts catalog
* `confirm`: Authorize response to confirmation requests
* `mail`: Authorize access to configure SMTP mail services.
* `network`: Authorize access to the network catalog
* `password`: Authorize access to password catalog
* `ssh`: Authorize use of SSH


~~~~
<div="terminal">
<cmd>Alice> meshman device auth Alice5 /all
<rsp>ERROR - The option System.Object[] is not known.
</div>
~~~~



# device complete

~~~~
<div="helptext">
<over>
complete   Complete a pending request
    /account   Account identifier (e.g. alice@example.com) or profile fingerprint
    /local   Local name for account (e.g. personal)
    /sync   If true, attempt to synchronize the account to the service before operation
    /auto   If true, automatically approve pending requests with prior authorization.
    /verbose   Verbose reports (default)
    /report   Report output (default)
    /json   Report output in JSON format
<over>
</div>
~~~~

The `device complete` command attempts to connect by means of a preconfigured
device profile by polling the manufacturer service.


~~~~
<div="terminal">
<cmd>Alice4> meshman device complete
<rsp>   Device UDF = MBYN-VC7T-AIEX-KCK2-WQLP-4B52-3GWY
   Account = alice@example.com
   Account UDF = MARP-HH6B-D2JW-WLQ6-RWPQ-2SLS-AI2V
</div>
~~~~



# device delete

~~~~
<div="helptext">
<over>
delete   Remove device from device catalog
       Device identifier
    /account   Account identifier (e.g. alice@example.com) or profile fingerprint
    /local   Local name for account (e.g. personal)
    /sync   If true, attempt to synchronize the account to the service before operation
    /auto   If true, automatically approve pending requests with prior authorization.
    /verbose   Verbose reports (default)
    /report   Report output (default)
    /json   Report output in JSON format
<over>
</div>
~~~~

The `device delete` command removes the specified device from the catalog.

The parameter specifies the device being configured by means of either
the UDF of the device profile or the device identifier.


~~~~
<div="terminal">
<cmd>Alice> meshman device delete
<rsp>ERROR - Value cannot be null. (Parameter 'key')
</div>
~~~~



# device install

~~~~
<div="helptext">
<over>
install   Connect by means of a connection URI from an administration device.
       The device profile
    /account   Account identifier (e.g. alice@example.com) or profile fingerprint
    /local   Local name for account (e.g. personal)
    /sync   If true, attempt to synchronize the account to the service before operation
    /auto   If true, automatically approve pending requests with prior authorization.
    /verbose   Verbose reports (default)
    /report   Report output (default)
    /json   Report output in JSON format
<over>
</div>
~~~~


~~~~
<div="terminal">
<cmd>Alice4> meshman device install EDRL-4MZP-3OUD-OZBS-SPZN-7OSO-OM.medk
<rsp></div>
~~~~



# device join

~~~~
<div="helptext">
<over>
join   Connect by means of a connection URI from an administration device.
       The device location URI
    /account   Account identifier (e.g. alice@example.com) or profile fingerprint
    /local   Local name for account (e.g. personal)
    /sync   If true, attempt to synchronize the account to the service before operation
    /auto   If true, automatically approve pending requests with prior authorization.
    /verbose   Verbose reports (default)
    /report   Report output (default)
    /json   Report output in JSON format
<over>
</div>
~~~~

The `device join` command attempts to connect a device to a personal profile
by means of a URI supplied by an administration device.


~~~~
<div="terminal">
<cmd>Alice5> meshman device join mcu://alice@example.com/AAOW-DIW2-ZTBT-QUJP-BZZA-HF7N-SA
<rsp>   Device UDF = MDRW-QCTK-RYYW-VMFP-2P5B-LCO5-LRV2
   Witness value = PDH5-DZIR-VZKZ-YWHY-EFS6-BRZI-L5CZ
</div>
~~~~



# device list

~~~~
<div="helptext">
<over>
list   List devices in the device catalog
       Recryption group name in user@example.com format
    /account   Account identifier (e.g. alice@example.com) or profile fingerprint
    /local   Local name for account (e.g. personal)
    /sync   If true, attempt to synchronize the account to the service before operation
    /auto   If true, automatically approve pending requests with prior authorization.
    /verbose   Verbose reports (default)
    /report   Report output (default)
    /json   Report output in JSON format
<over>
</div>
~~~~

The `device list` command lists the device profiles in the device catalog.


~~~~
<div="terminal">
<cmd>Alice> meshman device list
<rsp>ContextDevice Local: -
  Base UDF MDJY-YAXQ-VATX-F52K-YT6M-YMUK-D3PV
  Mesh UDF 
Encrypted: MCPU-YLOY-3PGZ-RS2A-JA2G-73P6-DMQ2
  Profile User
    Signed by: MARP-HH6B-D2JW-WLQ6-RWPQ-2SLS-AI2V
      KeyOfflineSignature: MARP-HH6B-D2JW-WLQ6-RWPQ-2SLS-AI2V 
      AccountAddress : alice@example.com 
      KeyEncryption:       MASL-5B72-3K3B-PKQR-MGHP-L5QP-MVOA 
  Profile Device
    Signed by: MDJY-YAXQ-VATX-F52K-YT6M-YMUK-D3PV
      ProfileSignature: MDJY-YAXQ-VATX-F52K-YT6M-YMUK-D3PV 
      KeySignature:        MCQU-URCN-F6YN-UXJT-HMTB-IT46-LZI3 
      KeyEncryption:       MATZ-4HLY-OSDB-2MPT-UG75-AWY4-EAYU 
      KeyAuthentication:   MC6M-NIE2-IIBE-QOZR-PAWL-VPCY-L7MM 
  Connection Device
    Signed by: MAJ5-HQAF-XXGK-W6BW-EGH3-ELNH-EQ7I
      KeyAuthentication:   MCLZ-7JDK-R6X2-3TL6-ZAMN-3ZDK-5P3C 

ContextDevice Local: -
  Base UDF MCI6-2IJ3-PFLT-GVUQ-5PN7-B337-VWWQ
  Mesh UDF 
Encrypted: MAST-EV3R-CLXM-LVOO-L3FH-QHJ7-WATP
  Profile User
    Signed by: MARP-HH6B-D2JW-WLQ6-RWPQ-2SLS-AI2V
      KeyOfflineSignature: MARP-HH6B-D2JW-WLQ6-RWPQ-2SLS-AI2V 
      AccountAddress : alice@example.com 
      KeyEncryption:       MASL-5B72-3K3B-PKQR-MGHP-L5QP-MVOA 
  Profile Device
    Signed by: MCI6-2IJ3-PFLT-GVUQ-5PN7-B337-VWWQ
      ProfileSignature: MCI6-2IJ3-PFLT-GVUQ-5PN7-B337-VWWQ 
      KeySignature:        MANQ-RGWQ-WVGB-67TK-ZYSG-EHKO-L4VZ 
      KeyEncryption:       MBS6-AI2M-MGJN-GLC5-23C7-VIOW-A2WU 
      KeyAuthentication:   MANG-ZBCQ-EKY7-4COM-7WWA-XA43-HQ36 
  Connection Device
    Signed by: MAJ5-HQAF-XXGK-W6BW-EGH3-ELNH-EQ7I
      KeyAuthentication:   MD75-RPX5-5NK4-FHKK-2PSE-DWSY-WU6Y 

</div>
~~~~



# device pending

~~~~
<div="helptext">
<over>
pending   Get list of pending connection requests
    /account   Account identifier (e.g. alice@example.com) or profile fingerprint
    /local   Local name for account (e.g. personal)
    /sync   If true, attempt to synchronize the account to the service before operation
    /auto   If true, automatically approve pending requests with prior authorization.
    /verbose   Verbose reports (default)
    /report   Report output (default)
    /json   Report output in JSON format
<over>
</div>
~~~~

The `device pending` command lists the pending device connection requests in
the inbound message spool.


~~~~
<div="terminal">
<cmd>Alice> meshman device pending
<rsp>MessageID: CI4K-GV7W-VRTX-QTNB-ZNBG-652H-GMWJ
        Connection Request::
        MessageID: CI4K-GV7W-VRTX-QTNB-ZNBG-652H-GMWJ
        To:  From: 
        Device:  MCI6-2IJ3-PFLT-GVUQ-5PN7-B337-VWWQ
        Witness: CI4K-GV7W-VRTX-QTNB-ZNBG-652H-GMWJ
</div>
~~~~



# device preconfig

~~~~
<div="helptext">
<over>
preconfig   Generate new device profile and publish as an EARL
    /account   Account identifier (e.g. alice@example.com) or profile fingerprint
    /local   Local name for account (e.g. personal)
    /sync   If true, attempt to synchronize the account to the service before operation
    /auto   If true, automatically approve pending requests with prior authorization.
    /verbose   Verbose reports (default)
    /report   Report output (default)
    /json   Report output in JSON format
    /length   Length of PIN to generate in characters
<over>
</div>
~~~~


~~~~
<div="terminal">
<cmd>Maker> meshman device preconfig
<rsp>Device UDF: MBYN-VC7T-AIEX-KCK2-WQLP-4B52-3GWY
File: EDRL-4MZP-3OUD-OZBS-SPZN-7OSO-OM.medk
</div>
~~~~




# device reject

~~~~
<div="helptext">
<over>
reject   Reject a pending connection
       Fingerprint of connection to reject
    /account   Account identifier (e.g. alice@example.com) or profile fingerprint
    /local   Local name for account (e.g. personal)
    /sync   If true, attempt to synchronize the account to the service before operation
    /auto   If true, automatically approve pending requests with prior authorization.
    /verbose   Verbose reports (default)
    /report   Report output (default)
    /json   Report output in JSON format
<over>
</div>
~~~~

The `device reject` command rejects the specified connection request.

The command must specify the connection identifier of the request 
being accepted. The connection identifier may be abbreviated provided that
this uniquely identifies the connection being accepted and that at least 
four characters are given.


~~~~
Missing example 4
~~~~

# device request

~~~~
<div="helptext">
<over>
request   Connect to an existing profile registered at a portal
       The Mesh Service Account
    /pin   One time use authenticator
    /verbose   Verbose reports (default)
    /report   Report output (default)
    /json   Report output in JSON format
    /new   Force creation of new device profile
    /dudf   Device profile fingerprint
    /did   Device identifier
    /dd   Device description
    /auth   (De)Authorize the specified function on the device
    /root   Device as super administration device
    /admin   Device as administration device
    /message   Authorize rights for Mesh messaging
    /web   Authorize rights for Mesh messaging and Web.
    /device   Device restrictive access
    /threshold   Authorize threshold rights for Mesh messaging and Web.
    /ssh   Authorize rights for specified SSH account
    /email   Authorize rights for specified smtp email account
    /member   Authorize member rights for specified Mesh group
    /group   Authorize group administrator rights for specified Mesh group
    /null   Do not authorize any device rights at all (cannot be used with any rights grant))
<over>
</div>
~~~~

The `device request \<account\>` command requests connection of a device to a mesh profile.

The \<account\> parameter specifies the account for which the connection request is
made.

If the account holder has generated an authentication code, this is specified by means of 
the `/pin` option.




~~~~
<div="terminal">
<cmd>Alice2> meshman device request alice@example.com
<rsp>   Device UDF = MCI6-2IJ3-PFLT-GVUQ-5PN7-B337-VWWQ
   Witness value = CI4K-GV7W-VRTX-QTNB-ZNBG-652H-GMWJ
</div>
~~~~




