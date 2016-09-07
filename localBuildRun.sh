#!/bin/bash -euv
echo -e '\033]2;'Sessions'\007'
./setup.sh
./build.sh
./run.sh
