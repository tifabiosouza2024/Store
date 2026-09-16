@echo off
setlocal enabledelayedexpansion


rem Caminho do arquivo XML
set "arquivo=Directory.Build.props"

rem Ler VERSAO
for /f "tokens=*" %%A in ('findstr "<ProjectVersion>" %arquivo%') do (
    set "linha=%%A"
    rem Extrai o valor entre as tags
    for /f "tokens=2 delims=><" %%B in ("!linha!") do set "VERSAO=%%B"
)

REM Divide em Major.Minor.Patch.Build
for /f "tokens=1-4 delims=." %%a in ("!VERSAO!") do (
    set MAJOR=%%a
    set MINOR=%%b
    set PATCH=%%c
    set BUILD=%%d
)

echo.
echo ==========================================
echo Versao atual: !VERSAO!
echo ==========================================
echo.
echo Escolha o tipo da alteracao:
echo.
echo [ENTER] Apenas Build
echo [1] Patch
echo [2] Minor
echo [3] Major
echo.

set /p OPCAO=Opcao:

REM ============================================================
REM Calcula a nova versão
REM ============================================================

set /a BUILD+=1

if "!OPCAO!"=="3" (
    set /a MAJOR+=1
    set MINOR=0
    set PATCH=0
    set BUILD=0
)

if "!OPCAO!"=="2" (
    set /a MINOR+=1
    set PATCH=0
    set BUILD=0
)

if "!OPCAO!"=="1" (
    set /a PATCH+=1
    set BUILD=0
)


set NOVA_VERSAO=!MAJOR!.!MINOR!.!PATCH!.!BUILD!

echo.
echo Nova versao: !NOVA_VERSAO!
echo.

set TAG=Store_!NOVA_VERSAO!

REM ============================================================
REM Docker Build
REM ============================================================

copy .\FS.Store\Dockerfile .\Dockerfile >nul

echo Criando imagem Docker...

docker build -t devfabiosouza/projetos:!TAG! .

set RESULTADO=%ERRORLEVEL%

del Dockerfile

if NOT "%RESULTADO%"=="0" (
    echo.
    echo ==========================================
    echo BUILD FALHOU.
    echo A versao NAO foi alterada.
    echo ==========================================
    pause
    exit /b 1
)

REM ============================================================
REM Login Docker
REM ============================================================

docker login -u devfabiosouza -p @#2c4h56RL@#

if errorlevel 1 (
    echo Erro no login do Docker.
    pause
    exit /b 1
)

REM ============================================================
REM Push
REM ============================================================

echo.
echo Enviando imagem...

docker push devfabiosouza/projetos:!TAG!

if errorlevel 1 (
    echo.
    echo ==========================================
    echo PUSH FALHOU.
    echo A versao NAO foi alterada.
    echo ==========================================
    pause
    exit /b 1
)

REM ============================================================
REM Atualiza o XML
REM ============================================================

powershell -Command "(Get-Content '%arquivo%') -replace '<ProjectVersion>.*?</ProjectVersion>', '<ProjectVersion>!NOVA_VERSAO!</ProjectVersion>' | Set-Content '%arquivo%'"


echo.
echo ==========================================
echo Versao atualizada para !NOVA_VERSAO!
echo ==========================================

pause