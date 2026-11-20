@echo off
setlocal
for %%P in (VL.Avalonia VL.Avalonia.Custom VL.Avalonia.Skia VL.Avalonia.Stride) do (
    for %%D in (lib src\obj src\bin\Release) do (
        call :remove "%~dp0%%P\%%D" || exit /b 1
    )
)
for %%D in (obj bin\Release) do (
    call :remove "%~dp0VL.Avalonia.CodeGen\%%D" || exit /b 1
)
echo Purge complete!
exit /b 0

:remove
if not exist "%~1\" exit /b 0
echo Deleting: "%~1"
rd /s /q "%~1"
if exist "%~1\" (
    echo Failed to remove "%~1" 1>&2
    exit /b 1
)
exit /b 0