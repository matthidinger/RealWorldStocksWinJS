#First some common params, delivered by the nuget package installer 
param($installPath, $toolsPath, $package, $project) 


Import-Module (Join-Path $toolsPath DynamicLocalhost.psm1)

Remove-DynamicLocalhostTargets $(SolutionDir)
