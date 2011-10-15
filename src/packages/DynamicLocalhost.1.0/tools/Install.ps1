#First some common params, delivered by the nuget package installer 
param($installPath, $toolsPath, $package, $project) 

$targetsPath = "`$(SolutionDir)\packages\$($package.Id).$($package.Version)\tools\DynamicLocalhost.targets"


Import-Module (Join-Path $toolsPath DynamicLocalhost.psm1)

Add-DynamicLocalhostTargets -ProjectName $($project.Name) -targetsPath $targetsPath

$project.DTE.ItemOperations.Navigate('http://dynamiclocalhost.codeplex.com/wikipage?title=NuGet')