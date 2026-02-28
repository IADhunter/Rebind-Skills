@echo off
title RebindSkills - Build y Deploy
color 0A

echo ============================================
echo   RebindSkills - Build y Deploy
echo ============================================
echo.

:: Compilar el proyecto
echo [1/2] Compilando...
dotnet build "D:\Documentos\RW mods\TemplateMod-master\src\rebindskills.csproj" -c Release --nologo -v quiet

IF %ERRORLEVEL% NEQ 0 (
    color 0C
    echo.
    echo [ERROR] La compilacion fallo. Revisa los errores arriba.
    pause
    exit /b 1
)

echo [OK] Compilacion exitosa.
echo.

:: Copiar la DLL a BepInEx/plugins
echo [2/2] Copiando DLL a plugins...
copy /Y "D:\Documentos\RW mods\TemplateMod-master\mod\newest\plugins\RebindSkills.dll" "D:\programas\Steam\steamapps\common\Rain World\BepInEx\plugins\RebindSkills.dll"

IF %ERRORLEVEL% NEQ 0 (
    color 0C
    echo.
    echo [ERROR] No se pudo copiar la DLL. Verifica que la ruta de plugins existe.
    pause
    exit /b 1
)

echo [OK] DLL copiada correctamente.
echo.
color 0A
echo ============================================
echo   Listo. Abre Rain World y prueba el mod.
echo ============================================
pause
