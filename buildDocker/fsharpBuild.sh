#!/bin/sh -euv

cd source
cp -R /code/.nuget/ .
cp -R /code/packages/ .
mono packages/FAKE/tools/FAKE.exe build.fsx
cd ..
cp source/build/output/* /binaries/
