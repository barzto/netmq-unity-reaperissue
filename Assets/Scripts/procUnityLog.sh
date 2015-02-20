#!/bin/sh
mv ../../Game.log .
grep '^[^ ]' Game.log > Game.log.stripped

