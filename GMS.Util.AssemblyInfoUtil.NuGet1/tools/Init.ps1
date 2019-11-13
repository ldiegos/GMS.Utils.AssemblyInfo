param($installPath, $toolsPath, $package)

$solutionNode = Get-Interface $dte.Solution ([EnvDTE80.Solution2])

$solutionItemsNode = $solutionNode.Projects | where-object { $_.ProjectName -eq ".AssemblyInfoUtil" } | select -first 1

if (!$solutionItemsNode) {
	$solutionItemsNode = $solutionNode.AddSolutionFolder(".AssemblyInfoUtil")
}

$solutionItemsProjectItems = Get-Interface $solutionItemsNode.ProjectItems ([EnvDTE.ProjectItems])

$rootDir = (Get-Item $installPath).parent.parent.fullname

#$deploySource = join-path $installPath '\sln\'
$deploySource = join-path $installPath '\lib\net461\'
$deploySourceSln = join-path $installPath '\sln\'
$deployTarget = join-path $rootDir '\.AssemblyInfoUtil\'

New-Item -ItemType Directory -Force -Path $deployTarget

$filename = '\GMS.Utils.AssemblyInfoUtil.exe'
$deploySourceFilename = join-path $deploySource $filename
$deployTargetFilename = join-path $deployTarget $filename

Copy-Item $deploySourceFilename $deployTargetFilename -Recurse -Force
$solutionItemsProjectItems.AddFromFile($deployTargetFilename) > $null

#Copy the readme file to the solution and open it on the Visual Studio.
$filename = '\readme.txt'
$deploySourceFilename = join-path $deploySourceSln $filename
$deployTargetFilename = join-path $deployTarget $filename

Copy-Item $deploySourceFilename $deployTargetFilename -Recurse -Force
$solutionItemsProjectItems.AddFromFile($deployTargetFilename) > $null

#Open readme file.
$DTE.ItemOperations.OpenFile($deployTargetFilename) 


#ls $deploySource | foreach-object {
#	$targetFile = join-path $deployTarget $_.Name
#	Copy-Item $_.FullName $targetFile -Recurse -Force
#	$solutionItemsProjectItems.AddFromFile($targetFile) > $null
#} > $null
