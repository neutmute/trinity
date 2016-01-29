# Build all executable projects in this solution
$thisFolder = (Get-Item -Path ".\" -Verbose).FullName

Write-Host "Executing from '$thisFolder'"
Write-Host "path=$env:PATH"
Write-Host "env.teamcity.build.checkoutDir=$env:teamcity.build.checkoutDir"

$projectsToBuild = @(
    [System.IO.Path]::GetFullPath((Join-Path $thisFolder "\src\Trinity"));
)

foreach($project in $projectsToBuild) {
    Write-Host "Restoring and building project: $project"
    dnu restore $project
    dnu build $project
    dnu pack $project
}
