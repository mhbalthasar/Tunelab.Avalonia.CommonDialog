@echo off
cd %~dp0
%~d0

rmdir /s /q ..\Output
mkdir ..\Output

dotnet publish ..\Tunelab.Avalonia.CommonDialog\Tunelab.CommonDialog.csproj -c Release  -o %temp%\CommonDialogOutput --artifacts-path %temp%\tmp_ui

REM COPY
move %temp%\CommonDialogOutput\Tunelab.CommonDialog.dll ..\Output\
