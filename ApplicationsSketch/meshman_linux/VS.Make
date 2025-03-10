
# Supplemental Makefile for Visual Studios Projects
#
# Visual Studio runs tools for most projects but not for shared projects.
# 
# Prebuild items supported:
#   [None currently]
# 
# PostBuild items supported:
#   * Copy library to locations on disk
#   * Build ilMerge executables
#   * Postprocess VSIX projects
#   * Copy executables


.PHONY : all always clean install publish prebuild prebuildRecurse postbuild postbuildRecurse

MSBuildThisFileDirectory = 

LinkFiles = \
    Goedel.ASN.dll\
    Goedel.Command.dll\
    Goedel.Discovery.dll\
    Goedel.FSR.dll\
    Goedel.IO.dll\
    Goedel.Registry.dll\
    Goedel.Utilities.dll\
    Goedel.Cryptography.Jose.dll\
    Goedel.Cryptography.KeyFile.dll\
    Goedel.Cryptography.Linux.dll\
    Goedel.Cryptography.Standard.dll\
    Goedel.Cryptography.dll\
    Goedel.Persistence.dll\
    Goedel.Protocol.Debug.dll\
    Goedel.Protocol.dll\
    Goedel.Mesh.Portal.Client.dll\
    Goedel.Mesh.Portal.dll

ToolTargets = 


prebuildRecurse : 
	cd ..\..\Libraries\MeshMan && nmake /c /f VS.make prebuild 


postbuildRecurse :
	cd ..\..\Libraries\MeshMan && nmake /c /f VS.make postbuild 

# Non shared project, nothing to do
prebuild : prebuildRecurse $(ToolTargets)

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget meshman.exe $(LinkFiles) 

