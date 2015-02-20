#!/bin/sh
./build.sh
echo Press key
mono helloworld.exe > standalone.log
grep '^[^ ]' standalone.log > standalone.log.stripped

