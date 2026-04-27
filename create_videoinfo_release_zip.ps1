# Create a release zip for VideoInfo
param(
    [string]$Configuration = "Release"
)

$scriptRoot = $PSScriptRoot
if (-not $scriptRoot) {
    $scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
}

Set-Location $scriptRoot

Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "VideoInfo Release Zip Script" -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

# ---------------------------------------------------------------------------
# Versioning: MAJOR/MINOR from VERSION file, PATCH = git commit count
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
Write-Host "Version: $version" -ForegroundColor Green
Write-Host ""

# ---------------------------------------------------------------------------
# Build
# ---------------------------------------------------------------------------
$projectPath = Join-Path $scriptRoot "VideoInfo\VideoInfo.csproj"
Write-Host "Building VideoInfo ($Configuration) version $version..." -ForegroundColor Cyan
dotnet build $projectPath -c $Configuration /p:Version=$version /p:AssemblyVersion=$version /p:FileVersion=$version
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed; aborting packaging." -ForegroundColor Red
    exit $LASTEXITCODE
}
Write-Host ""

# ---------------------------------------------------------------------------
# Locate build output
# ---------------------------------------------------------------------------
$tfm = "net10.0-windows10.0.26100.0"
$buildOutput = Join-Path $scriptRoot "VideoInfo\bin\$Configuration\$tfm"
$exePath = Join-Path $buildOutput "VideoInfo.exe"
if (-not (Test-Path $exePath)) {
    Write-Host "ERROR: Build output not found at $exePath" -ForegroundColor Red
    exit 1
}

# ---------------------------------------------------------------------------
# Stage files into a temp folder
# ---------------------------------------------------------------------------
$tempDir = Join-Path $scriptRoot "release-zip-staging"
if (Test-Path $tempDir) { Remove-Item $tempDir -Recurse -Force }
New-Item -ItemType Directory -Force -Path $tempDir | Out-Null

Write-Host "Staging release files..." -ForegroundColor Cyan

# Copy all runtime files from the build output, excluding logs, cfg files and pdbs
Get-ChildItem -Path $buildOutput -File | Where-Object {
    $_.Extension -notin @('.log', '.pdb') -and
    $_.Name -notlike '*.cfg'
} | ForEach-Object {
    Copy-Item $_.FullName -Destination $tempDir
}

# Copy the DLL subfolder (native DLLs required at runtime)
$dllSubDir = Join-Path $buildOutput "DLL"
if (Test-Path $dllSubDir) {
    $destDllDir = Join-Path $tempDir "DLL"
    Copy-Item $dllSubDir -Destination $destDllDir -Recurse
}

# Copy root-level docs
foreach ($docFile in @("LICENSE.txt", "README.md")) {
    $src = Join-Path $scriptRoot $docFile
    if (Test-Path $src) {
        Copy-Item $src -Destination $tempDir
    }
}

# ---------------------------------------------------------------------------
# Create the zip
# ---------------------------------------------------------------------------
$artifactsDir = Join-Path $scriptRoot "release-zip"
New-Item -ItemType Directory -Force -Path $artifactsDir | Out-Null
$zipPath = Join-Path $artifactsDir "VideoInfo-$version-$Configuration.zip"
if (Test-Path $zipPath) { Remove-Item $zipPath }

Write-Host "Creating $zipPath..." -ForegroundColor Cyan
Compress-Archive -Path "$tempDir\*" -DestinationPath $zipPath -Force

# ---------------------------------------------------------------------------
# Clean up temp folder
# ---------------------------------------------------------------------------
Remove-Item $tempDir -Recurse -Force

Write-Host ""
Write-Host "Release zip created: $zipPath" -ForegroundColor Green
