REM USE IT ALWAYS IN DEBUG MODE FOR DEVELOPMENT BRANCH.
REM USE IT ALWAYS IN RELEASE MODE FOR MASTER BRANCH.
REM ENVIRONMENT=Debug;ENVIRONMENT=Release
SET ENVIRONMENT=Release
SET SolutionDir=D:\Desarrollo\TFS-Git\Personal\!ProyectosGlobales\AssemblyInfoUtil\
SET NugetProject=GMS.Util.AssemblyInfoUtil.NuGet1\
SET NugetProjectPath=%SolutionDir%%NugetProject%
REM "bin\Debug";bin\Release
SET OutDir=""

IF Debug==%ENVIRONMENT% (
	SET OutDir=bin\Debug\
) ELSE (
	SET OutDir=bin\Release\
)

SET TargetPath=%NugetProjectPath%%OutDir%

xcopy "%TargetPath%GMS.Utils.AssemblyInfoUtil.exe" "%TargetPath%sln\" -y
del "%TargetPath%*.pdb"
del "%TargetPath%*.nupkg"
"%SolutionDir%.NuGetPack\NuGetPack.exe" "%NugetProjectPath%" "%TargetPath%" %ENVIRONMENT%

REM $(SolutionDir).NuGetPack\NuGet push -SkipDuplicate $(TargetDir)*.nupkg TmJgEsOE1DKWWzMA2u3R -Source http://192.168.1.6:90/NuGetServer/nuget 

REM .\.NuGetPack\NuGet push -SkipDuplicate .\GMS.Util.AssemblyInfoUtil.NuGet1\bin\Debug\*.nupkg oy2jagm6jccdxj3u5heqr37rqbqbvzru7lgmrqtvgnnj5q -Source https://api.nuget.org/v3/index.json
timeout 3
pause


