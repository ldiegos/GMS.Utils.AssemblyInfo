REM USE IT ALWAYS IN DEBUG MODE FOR DEVELOPMENT BRANCH.
REM USE IT ALWAYS IN RELEASE MODE FOR MASTER BRANCH.
REM ENVIRONMENT=DEBUG;ENVIRONMENT=RELEASE
SET ENVIRONMENT=RELEASE
SET SolutionDir=D:\Desarrollo\TFS-Git\Personal\!ProyectosGlobales\AssemblyInfoUtil\
REM "bin\Debug";bin\Release
SET OutDir=""

IF DEBUG==%ENVIRONMENT% (
	SET OutDir=bin\Debug\
) ELSE (
	SET OutDir=bin\Release\
)

IF DEBUG==%ENVIRONMENT% (
	"%SolutionDir%AssemblyInfoUtil\%OutDir%GMS.Utils.AssemblyInfoUtil.exe" "%SolutionDir%AssemblyInfoUtil\Properties\AssemblyInfo.cs" -inc:4
	"%SolutionDir%AssemblyInfoUtil\%OutDir%GMS.Utils.AssemblyInfoUtil.exe" "%SolutionDir%GMS.Util.AssemblyInfoUtil.NuGet1\Properties\VersionInfo.cs" -inc:4
) ELSE (
	"%SolutionDir%AssemblyInfoUtil\%OutDir%GMS.Utils.AssemblyInfoUtil.exe" "%SolutionDir%AssemblyInfoUtil\Properties\AssemblyInfo.cs"  -inc:3 -rst:4
	"%SolutionDir%AssemblyInfoUtil\%OutDir%GMS.Utils.AssemblyInfoUtil.exe" "%SolutionDir%GMS.Util.AssemblyInfoUtil.NuGet1\Properties\VersionInfo.cs"  -inc:3 -rst:4	
)

timeout 3
