# Create a release zip for NVAPIWrapper
param(
    [string]$Configuration = "Release",
    [switch]$IncludeSources
)

$scriptRoot = $PSScriptRoot
if (-not $scriptRoot) {
    $scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
}

Set-Location $scriptRoot

$NVIDIAHeader = Join-Path $scriptRoot "nvapi/nvapi.h"
if (-not (Test-Path $NVIDIAHeader)) {
    Write-Host "NVIDIA SDK headers not found. Run ./prepare_nvapi.ps1 first." -ForegroundColor Red
    exit 1
}

# ---------------------------------------------------------------------------
# Versioning (match build_nvidia.ps1: MAJOR/MINOR from VERSION, PATCH = git rev-list --count HEAD)
# ---------------------------------------------------------------------------
$versionFile = Join-Path $scriptRoot "VERSION"
$major = 1
$minor = 0
if (Test-Path $versionFile) {
    foreach ($line in Get-Content $versionFile) {
        if ($line -match "^MAJOR=(\d+)") { $major = [int]$matches[1] }
        elseif ($line -match "^MINOR=(\d+)") { $minor = [int]$matches[1] }
    }
}
$patch = 0
try {
    $gitPath = Get-Command git -ErrorAction SilentlyContinue
    if ($gitPath) {
        $commitCount = & git rev-list --count HEAD 2>$null
        if ($LASTEXITCODE -eq 0 -and $commitCount -match "^\d+$") {
            $patch = [int]$commitCount
        }
    }
} catch {}
$version = "$major.$minor.$patch"

$projectPath = Join-Path $scriptRoot "NVAPIWrapper/NVAPIWrapper.csproj"
Write-Host "Building NVAPIWrapper ($Configuration) version $version..." -ForegroundColor Cyan
dotnet build $projectPath -c $Configuration /p:Version=$version /p:AssemblyVersion=$version /p:FileVersion=$version
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed; aborting packaging." -ForegroundColor Red
    exit $LASTEXITCODE
}

$buildOutput = Join-Path $scriptRoot "NVAPIWrapper/bin/$Configuration/net10.0"
$assemblyPath = Join-Path $buildOutput "NVAPIWrapper.dll"
if (-not (Test-Path $assemblyPath)) {
    Write-Host "Build output not found at $assemblyPath" -ForegroundColor Red
    exit 1
}

$artifactsDir = Join-Path $scriptRoot "release-zip"
New-Item -ItemType Directory -Force -Path $artifactsDir | Out-Null
$zipPath = Join-Path $artifactsDir "NVAPIWrapper-$version-$Configuration.zip"
if (Test-Path $zipPath) { Remove-Item $zipPath }

$pathsToPack = @()
$pathsToPack += $assemblyPath
$pathsToPack += Join-Path $buildOutput "NVAPIWrapper.pdb"
$pathsToPack += Join-Path $buildOutput "NVAPIWrapper.deps.json"
$pathsToPack += Join-Path $buildOutput "NVAPIWrapper.xml"
$pathsToPack += Join-Path $scriptRoot "LICENSE"
$pathsToPack += Join-Path $scriptRoot "README.md"
$pathsToPack += Join-Path $scriptRoot "NVAPIWrapper/README.md"
$pathsToPack = $pathsToPack | Where-Object { Test-Path $_ }

if ($IncludeSources) {
    $pathsToPack += (Join-Path $scriptRoot "NVAPIWrapper/cs_generated")
    $sourceFiles = Get-ChildItem -Path (Join-Path $scriptRoot "NVAPIWrapper") -Filter *.cs -File
    $pathsToPack += $sourceFiles.FullName
    $pathsToPack += (Join-Path $scriptRoot "NVAPIWrapper/NVAPIWrapper.csproj")
}

$pathsToPack = $pathsToPack | Select-Object -Unique

Write-Host "Creating $zipPath..." -ForegroundColor Cyan
Compress-Archive -Path $pathsToPack -DestinationPath $zipPath -Force
Write-Host "Release zip created at $zipPath" -ForegroundColor Green
