#!/bin/sh

node=`which node`
coffee=`which coffee`

$node &> $3 $coffee $1 $2
