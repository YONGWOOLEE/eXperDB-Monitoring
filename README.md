# Introduction

eXperDB-Monitoring for PostgreSQL provide real time monitoring for PostgreSQL It enables various types of monitoring such as session, alive, resource, performance, and so on. It supports to monitoring not only Streaming Replication but also HA status. You can use eXperDB-Monitoring for PostgreSQL to quickly find various troubles, analyze troubles, and tune performance of PostgreSQL.


![intro](./images/introduction.png "Introduce eXperDB-Monitoring")
![intro2](./images/introduction2.png "Introduce eXperDB-Monitoring")


## characteristic

### Agentless
   * eXperDB-Monitoring collect by installing only extension module on the target cluster of PostgreSQL server
   * 3-tier : the client has no affect on the target servers.
   * Easy installing and upinstaling.

### Monitoring multi-cluster smultaneously.

### Extensive collection of information

### Maximizing user convenience
   
## System Components

eXperDB-Monitoring for postgreSQL consists of a server for collecting database information, extension on PostgreSQL server, and a control screen as an integrated console.

![consist](./images/consist.png "eXperDB-Monitoring components")

# Features

1) Dash-Board

2) Resource monitoring

3) Transaction monitoring

4) Health-Check by key performance indicator
-	Long running SQL
-	Unused index
-	Last vacuum day
-	Last analyze day
-	Connection failed 
-	Idle in transaction
-	Locked transaction
-	Disk used ratio
-	Swap used ratio
-	CPU wait ratio
-	Current connection
-	Commit rate
- Buffer hit rate
- HA Status

5) Backend process monitoring

6) SQL Plan

7) Object statistics

8) Time-Line-View (Multiple Charts)

9) Alert Event 

10) Control Session(Backend) and Lock

11) HA Status and Streaming Replication monitoring

# License
[![LICENSE](https://img.shields.io/badge/LICENSE-GPLv3-ff69b4.svg)](https://github.com/experdb/eXperDB-Management/blob/master/LICENSE)


# Installation

## Installing eXperDB-Monitoring-Server
### System Requirements
* OS : Linux
* JDK : JDK 1.7 or later
* CPU : At least 4core, recommended 8core
* HDD : 100GB or more

### Installation procedure
1.Preparation
eXperDB-Monitoring for PostgreSQL using PostgreSQL as as repository. so PostgreSQL must be installed on your linux machine before installing eXperDB-Monitoring-Server.
<pre>  
tar zxvf eXperDB_Server_xxx.tar.gz
</pre>
  
2.Run install script
<pre>  
cd eXperDB_Server
./install.sh -h 127.0.0.1 -d postgres -p 5432 -U postgres -W postgres
</pre>

## Installing eXperDB-Monitoring-Extension
### System Requirements
* OS : Linux
* PostgreSQL : Ver 9.1 or later

### Installation procedure
1.Preparation
<pre>
# tar zxvf eXperDB_PGMON_xxx.tar
</pre>
2.Build
<pre>
# tar zxvf eXperDB_PGMON_xxx.tar
# cd eXperDB_PGMON
# make USE_PGXS=1 install
</pre>
3.Create extension.
- Create the database for monitoring.
- Create the role with super privileges for monitoring
- Create extension
<pre>
postgres=# create extension experdb_mon;
</pre>

## Installing eXperDB-Monitoring-Client
### System Requirements
* OS : Windows 7 or later
* .Net Framwork : 4.5.1 or later (already contained in the installation package)
* Resolution : 1920x1080

### Installation procedure
1. Run eXperDB.Monitoring_XXX.exe

### Documentation
[User Manaual](https://github.com/YONGWOOLEE/eXperDB-Monitoring/tree/master/document/eXperDB-Monitoring_User_Manual_v10.4_d3.02.pdf)

# Copyright
Copyright (c) 2016-2019, eXperDB Development Team
All rights reserved.

