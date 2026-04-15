# AGENTS Guide for VideoInfo

This file captures the essential rules and context for agents working on this VideoInfo repository. 

## Project Scope
- Purpose: Command line application to test and exercise the video libraries used in DisplayMagician separately so that bugs can be discovered and fixed outside of DisplayMagician. Tests NVAPIWrapper, ADLXWrapper and IGCLWrapper, three c# video library wrappers that wrap the underlying C/C++ libraries provided by their respective manufacturers.
- Structure: 
    - Root `VideoInfo/` project,
    - NVAPIWrapper DLL in `VideoInfo/DLL` that allows access to the NVIDIA NvAPI DLL installed as part of NVIDIA driver,
    - ADLXWrapper DLL in `VideoInfo/DLL` that allows access to the AMD ADLX DLL installed as part of the AMD driver,
    - IGCLWrapper DLL in `VideoInfo/DLL` that allows access to the Intel IGCL DLL installed as part of the Intel driver,

## Core Development Rules
- ALWAYS MAKE SURE THAT YOU TELL THE USER YOUR PLAN BEFORE YOU MAKE ANY CHANGES TO FILES AND GIVE THE USER A CHANCE TO REVIEW. ONLY MAKE CHANGS ONCE THE USER HAS GIVEN THEIR APPROVAL. The user can tell you to perform multiple steps of a plan if you want to.
- When PLANNING, if you think you will get confused and lose track of where you are in your plan, then please write it down into a PLAN.md document. Keep the PLAN.md updated as you go, and make sure that the information you store in the PLAN.md is very descriptive and detailed, so that if you lose track in the future you can review the PLAN.md and you will know what to do and will do it well. Do not be overly concise as you lose a lot of nuance that will be important.
- DO NOT MAKE THINGS UP. Always check the VideoInfo code in `VideoInfo` if you need more information. If you are unsure then tell the user. The user wants you to only use facts - not conjecture. Tune your temperature to the lowest you can. You must be factual in your answers - DO NOT INVENT OR MAKJE ANYTHING UP. ACCURACY IS THE MOST IMPORTANT THING.
- Write code that tries to be robust and cope with problems getting the information requested, but without causing an exception or a crash. 
- Naming/patterns: Preserve established coding patterns and styles across this project. Ask the user for permission if you need to deviate from those styles.
- If you need to run scripts, note that all development is being done on Windows 11 x64 machines, and within Powershell terminals. You MUST make sure that your scripts will run in a powershell environment on a Windows 11 x64 machine.

## Testing Expectations
- CRITICAL: The tests should be designed to find errors in the VideoInfo library. DO NOT PATCH TESTS SO THAT THEY RUN SUCCESSFULLY TO AVOID UNDERLYING ERRORS IN THE VideoInfo LIBRARY. THE WHOLE POINT OF TESTING IS TO FIND UNDERLYING ERRORS IN THE VideoInfo LIBRARY SO THAT THEY CAN BE FIXED.  
- Suites: xUnit in `VideoInfo.Tests` targeting `net10.0`; hardware-aware and read-only (no tuning changes). Global xUnit parallelization is disabled.

## Versioning
- Version scheme: `VERSION` provides MAJOR.MINOR; PATCH computed from git commit count via `SetVersionFromGit` and `build_NVAPI.ps1`. Update `VERSION` when bumping MAJOR/MINOR.

## Expectations for Agents
- Keep APIs and helpers consistent with existing conventions; avoid breaking established patterns. Consistency is key across the whole codebase. Do not deviate from this consistency without first requesting permission from the user. 
- Respect disposal and pointer ownership rules; ensure safe lifetime handling.
- Maintain optional-feature gating and hardware skip behavior in tests and helpers.
