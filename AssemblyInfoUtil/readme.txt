GMS.Util.AssemblyInfoUtil
===========
Many thanks to Sergiy Korzh https://www.codeproject.com/Articles/31236/How-To-Update-Assembly-Version-Number-Automaticall for the starting point 
on this utility.

Usage: AssemblyInfoUtil <path to AssemblyInfo.cs or AssemblyInfo.vb file> [options]

Options:
  -set:<new version number> - set new version number (in xxxxx.xxxxx.xxxxx.xxxxx format)
       -set:1.5648.0.0: Will overide the current value in file and set this.
  -inc:<parameter index>  - increases the parameter with specified index (can be from 1 to 4)
       -inc:1 - Major version - 1.0.0.0 -> 2.0.0.0
       -inc:2 - Minor version - 1.0.0.0 -> 1.1.0.0
       -inc:3 - Build - 1.0.0.0 -> 1.0.1.0
       -inc:4 - Revision - 1.0.0.0 -> 1.0.0.1
  -rst:<parameter index> - Reset to 0 the parameter specified index (can be from 2 to 4)
       -rst:2 - Minor version - 1.5648.0.0 -> 1.0.0.0
       -rst:3 - Build - 1.0.4567.0 -> 1.0.0.0
       -rst:4 - Revision - 1.0.0.4567 -> 1.0.0.0

Notes:
1. The maximum value for each of the version sections is 65534.
2. Using the inc: with 1,2,3,4 will increase only that version section and when reach the 65534 value, will increase the previous ones.
3. The reset option will be executed always at the end, so the -set will be first and the -inc will be sencond and then the reset.
	-inc:4 -rst:4 -> 1.0.65534.3 -> 1.0.65534.4 -> 1.0.65534.0 .This example has no sense, but... :P
	-inc:3 -rst:4 -> 1.0.65534.3 -> 1.1.0.3 -> 1.1.0.0

Possible usages:
	When developing and testing, in the "Pre-Build event command line:" in the project properties you could add the following lines

	if $(ConfigurationName) == Debug (
		$(SolutionDir).AssemblyInfoUtil\GMS.Utils.AssemblyInfoUtil.exe "$(ProjectDir)Properties\AssemblyInfo.cs" -inc:4		
	) else (
		$(SolutionDir).AssemblyInfoUtil\GMS.Utils.AssemblyInfoUtil.exe "$(ProjectDir)Properties\AssemblyInfo.cs" -inc:3 -rst:4
	)

This lines on Debug will update the Revision number (Major.Minor.Build.Revision) on every build/compile. On Release will update the Build number and 
reset the Revision number (Major.Minor.Build.Revision). This could be a great idea to have some kind of versioning.
Examples:
1.0.0.1 -> 1.0.0.2 -> 1.0.0.3 -> .... -> 1.0.0.789 - Always in debug you will have 789 builds.
Changing to Release, build and return to debug:
1.0.0.789 -> 1.0.1.0 - Release
1.0.1.0 -> 1.0.1.1 -> 1.0.1.2 ->...-> 1.0.1.456 ->... -> 1.0.1.45000 - Always in debug you will have 45000 builds.
