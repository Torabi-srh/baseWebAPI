﻿#!/bin/bash

cd /root/baseWebAPI
git stash
git stash drop
git pull
docker stop $(docker ps -aqf "name=baseWebAPI")
docker rm -f $(docker ps -aqf "name=baseWebAPI")
docker rmi -f $(docker images -aqf "name=baseWebAPI")
docker build -t baseWebAPI .
docker run -d --network=host --name baseWebAPI baseWebAPI -v /var/run/docker.sock:/var/run/docker.sock