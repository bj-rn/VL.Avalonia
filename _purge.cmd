@echo off
echo Purging 'lib' and 'obj' artifacts...

:: Loop through the current directory and all subdirectories looking for 'lib' and 'obj'
FOR /d /r . %%d IN (lib) DO (
    IF EXIST "%%d" (
        echo Deleting: "%%d"
        rd /s /q "%%d"
    )
)

FOR /d /r . %%d IN (obj) DO (
    IF EXIST "%%d" (
        echo Deleting: "%%d"
        rd /s /q "%%d"
    )
)

echo.
echo Purge complete!
pause