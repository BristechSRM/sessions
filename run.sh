cd build/output/
if test "$OS" = "Windows_NT"
then
    ./Sessions.exe
else
    mono Sessions.exe
fi
