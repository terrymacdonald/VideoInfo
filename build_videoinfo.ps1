#
# VideoInfo Build Script (PowerShell)
# Builds the VideoInfo solution using the local .NET SDK
#

# Resolve script directory
$scriptRoot = $PSScriptRoot
if (-not $scriptRoot) {
    $scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
}

Set-Location $scriptRoot
Write-Host "Working directory: $scriptRoot" -ForegroundColor Cyan
Write-Host ""

Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "VideoInfo Rebuild Script" -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

# ============================================================================
# Determine version information from the csproj (fallback to 1.0.0)
# ============================================================================
Write-Host "Determining version number..." -ForegroundColor Yellow

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
    Write-Host ""
    Write-Host "Please ensure .NET 10.0 SDK is installed" -ForegroundColor Yellow
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

    if ($LASTEXITCODE -ne 0) {
        throw "Restore failed with exit code $LASTEXITCODE"
    }

    Write-Host ""
    Write-Host "NuGet packages restored successfully!" -ForegroundColor Green
    Write-Host ""
} catch {
    Write-Host ""
    Write-Host "ERROR: Failed to restore NuGet packages!" -ForegroundColor Red
    Write-Host "Error: $_" -ForegroundColor Yellow
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit 1
}

# ============================================================================
# Clean the solution
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "Cleaning VideoInfo solution..." -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

$configuration = "Debug"
$buildProps = @("/p:Version=$version", "/p:AssemblyVersion=$version", "/p:FileVersion=$version")

try {
    Write-Host "dotnet clean $solutionPath --configuration $configuration $($buildProps -join ' ')" -ForegroundColor Gray
    dotnet clean $solutionPath --configuration $configuration $buildProps

    if ($LASTEXITCODE -ne 0) {
        throw "Clean failed with exit code $LASTEXITCODE"
    }

    Write-Host ""
    Write-Host "Clean completed successfully!" -ForegroundColor Green
    Write-Host ""
} catch {
    Write-Host ""
    Write-Host "ERROR: Clean failed!" -ForegroundColor Red
    Write-Host "Error: $_" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Troubleshooting:" -ForegroundColor Yellow
    Write-Host "  - Ensure all NuGet packages are restored" -ForegroundColor Gray
    Write-Host "  - Verify .NET 10.0 SDK is installed" -ForegroundColor Gray
    Write-Host "  - Check project files for errors" -ForegroundColor Gray
    Write-Host ""
    Read-Host "Press Enter to exit"
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
    Write-Host "dotnet build $solutionPath --configuration $configuration --no-restore $($buildProps -join ' ')" -ForegroundColor Gray
    dotnet build $solutionPath --configuration $configuration --no-restore $buildProps

    if ($LASTEXITCODE -ne 0) {
        throw "Rebuild failed with exit code $LASTEXITCODE"
    }

    Write-Host ""
    Write-Host "Rebuild completed successfully!" -ForegroundColor Green
    Write-Host ""
} catch {
    Write-Host ""
    Write-Host "ERROR: Rebuild failed!" -ForegroundColor Red
    Write-Host "Error: $_" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Troubleshooting:" -ForegroundColor Yellow
    Write-Host "  - Ensure all NuGet packages are restored" -ForegroundColor Gray
    Write-Host "  - Verify .NET 10.0 SDK is installed" -ForegroundColor Gray
    Write-Host "  - Check project files for errors" -ForegroundColor Gray
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit 1
}

# ============================================================================
# Success summary
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Green
Write-Host "*** REBUILD SUCCESSFUL! ***" -ForegroundColor Green
Write-Host "============================================================================" -ForegroundColor Green
Write-Host ""
Write-Host "Projects built:" -ForegroundColor Cyan
Write-Host "  - VideoInfo" -ForegroundColor Green
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Cyan
Write-Host "  - Run from bin\\x64\\Debug or switch configuration in the script if needed" -ForegroundColor Gray
Write-Host ""
Write-Host "Press Enter to exit..."
Read-Host
