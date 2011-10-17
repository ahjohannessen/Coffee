@echo off
cd /d %~dp0

set _=%CD%
set n=%_%/node.exe
set c=%_%/bin/coffee

%n% >>%3 2>&1 %c% %1 %2 