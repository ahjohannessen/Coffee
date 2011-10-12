@echo off
cd /d %~dp0

set _=%CD%
set n=%_%/node.exe
set c=%_%/bin/coffee

::%n% >>output.log 2>&1 %c% %*
%n% %c% %*