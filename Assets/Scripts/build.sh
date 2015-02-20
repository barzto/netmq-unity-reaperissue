#!/bin/sh
BASEREFS="-r:System.ServiceModel -r:lib/AsyncIO.dll"
GMCS=gmcs
#$GMCS -out:lib/NetMQ.dll -target:library $BASEREFS $(find log netmq -name '*.cs') 
$GMCS -out:helloworld.exe $BASEREFS $(find helloworld log netmq -name '*.cs')
