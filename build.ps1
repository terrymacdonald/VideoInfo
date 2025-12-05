#
# VideoInfo Build Script (PowerShell)
# Builds the VideoInfo solution using dotnet CLI
#

# Get the directory where this script is located
$scriptRoot = $PSScriptRoot
if (-not $scriptRoot) {
    $scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
}

# Change to script directory
Set-Location $scriptRoot
Write-Host "Working directory: $scriptRoot" -ForegroundColor Cyan
Write-Host ""

Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "VideoInfo Build Script" -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

# ============================================================================
# Read version from csproj (fallback to 1.0.0)
# ============================================================================
Write-Host "Determining version number..." -ForegroundColor Yellow

$csprojPath = Join-Path $scriptRoot "VideoInfo\VideoInfo.csproj"
$version = "1.0.0"
$assemblyVersion = $null
$fileVersion = $null

if (Test-Path $csprojPath) {
    try {
        [xml]$csprojXml = Get-Content $csprojPath
        $assemblyVersion = $csprojXml.Project.PropertyGroup.AssemblyVersion | Select-Object -First 1
        $fileVersion = $csprojXml.Project.PropertyGroup.FileVersion | Select-Object -First 1
    }
    catch {
        Write-Host "Warning: Unable to read version from csproj: $_" -ForegroundColor Yellow
    }
}

if ($fileVersion) {
    $version = $fileVersion.Trim()
}
elseif ($assemblyVersion) {
    $version = $assemblyVersion.Trim()
}

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
}
catch {
    Write-Host "";
    Write-Host "ERROR: dotnet CLI not found in PATH" -ForegroundColor Red
    Write-Host "";
    Write-Host "Please ensure .NET 8.0 SDK is installed" -ForegroundColor Yellow
    Write-Host "Download from: https://dotnet.microsoft.com/download/dotnet/8.0" -ForegroundColor Cyan
    Write-Host "";
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
    }
    else {
        Write-Host "";
        Write-Host "ERROR: .NET 10.0 SDK not found" -ForegroundColor Red
        Write-Host "";
        Write-Host "Available SDKs:" -ForegroundColor Yellow
        $sdks | ForEach-Object { Write-Host "  $_" -ForegroundColor Gray }
        Write-Host "";
        Write-Host "Please install .NET 10.0 SDK from: https://dotnet.microsoft.com/download/dotnet/10.0" -ForegroundColor Cyan
        Write-Host "";
        exit 1
    }
}
catch {
    Write-Host "";
    Write-Host "ERROR: Failed to check .NET SDK version: $_" -ForegroundColor Red
    Write-Host "";
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
    exit 1
}

try {
    & dotnet restore $solutionPath
    
    if ($LASTEXITCODE -ne 0) {
        throw "Restore failed with exit code $LASTEXITCODE"
    }
    
    Write-Host "";
    Write-Host "NuGet packages restored successfully!" -ForegroundColor Green
    Write-Host "";
}
catch {
    Write-Host "";
    Write-Host "ERROR: Failed to restore NuGet packages!" -ForegroundColor Red
    Write-Host "Error: $_" -ForegroundColor Yellow
    Write-Host "";
    exit 1
}

# ============================================================================
# Clean the solution
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "Cleaning VideoInfo solution..." -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

$configuration = "Release"

try {
    Write-Host "dotnet clean $solutionPath --configuration $configuration /p:Version=$version /p:AssemblyVersion=$version /p:FileVersion=$version"
    dotnet clean $solutionPath --configuration $configuration /p:Version=$version /p:AssemblyVersion=$version /p:FileVersion=$version
    
    if ($LASTEXITCODE -ne 0) {
        throw "Clean failed with exit code $LASTEXITCODE"
    }
    
    Write-Host "";
    Write-Host "Clean completed successfully!" -ForegroundColor Green
    Write-Host "";
}
catch {
    Write-Host "";
    Write-Host "ERROR: Clean failed!" -ForegroundColor Red
    Write-Host "Error: $_" -ForegroundColor Yellow
    Write-Host "";
    exit 1
}

# ============================================================================
# Rebuild the solution
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "Rebuilding VideoInfo solution..." -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

try {
    Write-Host "dotnet build $solutionPath --configuration $configuration --no-restore /p:Version=$version /p:AssemblyVersion=$version /p:FileVersion=$version"
    dotnet build $solutionPath --configuration $configuration --no-restore /p:Version=$version /p:AssemblyVersion=$version /p:FileVersion=$version
    
    if ($LASTEXITCODE -ne 0) {
        throw "Rebuild failed with exit code $LASTEXITCODE"
    }
    
    Write-Host "";
    Write-Host "Rebuild completed successfully!" -ForegroundColor Green
    Write-Host "";
}
catch {
    Write-Host "";
    Write-Host "ERROR: Rebuild failed!" -ForegroundColor Red
    Write-Host "Error: $_" -ForegroundColor Yellow
    Write-Host "";
    exit 1
}

# ============================================================================
# Success summary
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Green
Write-Host "*** BUILD SUCCESSFUL! ***" -ForegroundColor Green
Write-Host "============================================================================" -ForegroundColor Green
Write-Host ""
Write-Host "Projects built:" -ForegroundColor Cyan
Write-Host "  - VideoInfo" -ForegroundColor Green
Write-Host ""
Write-Host "Outputs:" -ForegroundColor Cyan
Write-Host "  - bin\\x64\\Release\\VideoInfo.exe" -ForegroundColor Gray
Write-Host ""; 
