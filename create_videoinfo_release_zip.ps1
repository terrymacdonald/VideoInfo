#
# VideoInfo Release Zip Script (PowerShell)
# Builds VideoInfo in Release configuration and packages it into a zip file
# ready for a user to extract and run.
#
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

# ============================================================================
# Versioning: MAJOR/MINOR from VERSION file, PATCH = git commit count
# ============================================================================
Write-Host "Determining version number..." -ForegroundColor Yellow

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

# ============================================================================
# Verify dotnet CLI is available
# ============================================================================
Write-Host "Checking for .NET CLI..." -ForegroundColor Yellow

try {
    $dotnetPath = Get-Command dotnet -ErrorAction Stop
    $dotnetVersion = & dotnet --version 2>&1
    Write-Host ".NET CLI found: $($dotnetPath.Source)" -ForegroundColor Green
    Write-Host ".NET version: $dotnetVersion" -ForegroundColor Green
    Write-Host ""
} catch {
    Write-Host ""
    Write-Host "ERROR: dotnet CLI not found in PATH" -ForegroundColor Red
    Write-Host "Please ensure .NET 10.0 SDK is installed." -ForegroundColor Yellow
    Write-Host "Download from: https://dotnet.microsoft.com/download/dotnet/10.0" -ForegroundColor Cyan
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit 1
}

# ============================================================================
# Check .NET 10.0 SDK availability
# ============================================================================
Write-Host "Checking for .NET 10.0 SDK..." -ForegroundColor Yellow

try {
    $sdks = & dotnet --list-sdks 2>&1
    $net10Sdk = $sdks | Where-Object { $_ -match "10\.0\." }

    if ($net10Sdk) {
        Write-Host ".NET 10.0 SDK found:" -ForegroundColor Green
        $net10Sdk | ForEach-Object { Write-Host "  $_" -ForegroundColor Green }
        Write-Host ""
    } else {
        Write-Host ""
        Write-Host "ERROR: .NET 10.0 SDK not found" -ForegroundColor Red
        Write-Host ""
        Write-Host "Available SDKs:" -ForegroundColor Yellow
        $sdks | ForEach-Object { Write-Host "  $_" -ForegroundColor Gray }
        Write-Host ""
        Write-Host "Please install .NET 10.0 SDK from: https://dotnet.microsoft.com/download/dotnet/10.0" -ForegroundColor Cyan
        Write-Host ""
        Read-Host "Press Enter to exit"
        exit 1
    }
} catch {
    Write-Host ""
    Write-Host "ERROR: Failed to check .NET SDK version: $_" -ForegroundColor Red
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit 1
}

# ============================================================================
# Restore NuGet packages
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "Restoring NuGet packages..." -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

$solutionPath = Join-Path $scriptRoot "VideoInfo.sln"
if (-not (Test-Path $solutionPath)) {
    Write-Host "ERROR: Solution file not found: $solutionPath" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

try {
    & dotnet restore $solutionPath
    if ($LASTEXITCODE -ne 0) { throw "Restore failed with exit code $LASTEXITCODE" }
    Write-Host ""
    Write-Host "NuGet packages restored successfully!" -ForegroundColor Green
    Write-Host ""
} catch {
    Write-Host ""
    Write-Host "ERROR: Failed to restore NuGet packages: $_" -ForegroundColor Red
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit 1
}

# ============================================================================
# Build in the requested configuration
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "Building VideoInfo ($Configuration) version $version..." -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

$buildProps = @("/p:Version=$version", "/p:AssemblyVersion=$version", "/p:FileVersion=$version")

try {
    Write-Host "dotnet build $solutionPath --configuration $Configuration --no-restore $($buildProps -join ' ')" -ForegroundColor Gray
    & dotnet build $solutionPath --configuration $Configuration --no-restore $buildProps
    if ($LASTEXITCODE -ne 0) { throw "Build failed with exit code $LASTEXITCODE" }
    Write-Host ""
    Write-Host "Build completed successfully!" -ForegroundColor Green
    Write-Host ""
} catch {
    Write-Host ""
    Write-Host "ERROR: Build failed: $_" -ForegroundColor Red
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit 1
}

# ============================================================================
# Locate build output
# ============================================================================
$tfm = "net10.0-windows10.0.26100.0"
$buildOutput = Join-Path $scriptRoot "VideoInfo\bin\$Configuration\$tfm"
$exePath = Join-Path $buildOutput "VideoInfo.exe"
if (-not (Test-Path $exePath)) {
    Write-Host "ERROR: Build output not found at $exePath" -ForegroundColor Red
    Write-Host "Expected TFM output folder: $buildOutput" -ForegroundColor Yellow
    Read-Host "Press Enter to exit"
    exit 1
}

# ============================================================================
# Stage files into a temp folder
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "Staging release files..." -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

$tempDir = Join-Path $scriptRoot "release-zip-staging"
if (Test-Path $tempDir) { Remove-Item $tempDir -Recurse -Force }
New-Item -ItemType Directory -Force -Path $tempDir | Out-Null

# Copy all runtime files from the build output, excluding .log, .pdb and .cfg files
Get-ChildItem -Path $buildOutput -File | Where-Object {
    $_.Extension -notin @('.log', '.pdb') -and
    $_.Name -notlike '*.cfg'
} | ForEach-Object {
    Copy-Item $_.FullName -Destination $tempDir
    Write-Host "  Staged: $($_.Name)" -ForegroundColor Gray
}

# Copy the DLL subfolder if present (native DLLs required at runtime)
$dllSubDir = Join-Path $buildOutput "DLL"
if (Test-Path $dllSubDir) {
    $destDllDir = Join-Path $tempDir "DLL"
    Copy-Item $dllSubDir -Destination $destDllDir -Recurse
    Write-Host "  Staged: DLL\ folder" -ForegroundColor Gray
}

# Copy root-level documentation
foreach ($docFile in @("LICENSE.txt", "README.md")) {
    $src = Join-Path $scriptRoot $docFile
    if (Test-Path $src) {
        Copy-Item $src -Destination $tempDir
        Write-Host "  Staged: $docFile" -ForegroundColor Gray
    }
}

Write-Host ""

# ============================================================================
# Create the zip
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "Creating release zip..." -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

$artifactsDir = Join-Path $scriptRoot "release-zip"
New-Item -ItemType Directory -Force -Path $artifactsDir | Out-Null
$zipPath = Join-Path $artifactsDir "VideoInfo-$version-$Configuration.zip"
if (Test-Path $zipPath) { Remove-Item $zipPath -Force }

try {
    Compress-Archive -Path "$tempDir\*" -DestinationPath $zipPath -Force
} catch {
    Write-Host "ERROR: Failed to create zip: $_" -ForegroundColor Red
    Remove-Item $tempDir -Recurse -Force
    Read-Host "Press Enter to exit"
    exit 1
}

# ============================================================================
# Clean up staging folder
# ============================================================================
Remove-Item $tempDir -Recurse -Force

# ============================================================================
# Summary
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Green
Write-Host "*** RELEASE ZIP CREATED SUCCESSFULLY! ***" -ForegroundColor Green
Write-Host "============================================================================" -ForegroundColor Green
Write-Host ""
Write-Host "Output: $zipPath" -ForegroundColor Cyan
Write-Host ""
Write-Host "Press Enter to exit..."
Read-Host
