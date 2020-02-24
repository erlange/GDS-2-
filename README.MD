
![GDS2](./gdscs/images/gds2Logo2.jpg "GDS2")

GDS-2 is a web based tool meant to facilitate data query on governance issues and public services in Indonesia.

##### Table of Contents

* [**Overview**](#overview)
* [**Requirements**](#requirements)
* [**Installation**](#installation)
  * [Database Installation](#database-installation)
  * [Source Code](#Source-Code)
* [**Analysis Modes**](#basic-usage)
  * [Single Variable](#single-variable)
  * [Scatter Plot](#scatter-plot)
  * [Score Maker](#score-maker)

## Overview
![GDS2](./docimg/GDS-00.png "GDS2")

## Requirements
1. .NET Framework 2.0 or newer.  Yes, this project is ancient. 
2. For development use Visual Studio 2010 or newer.
3. Microsoft SQL Server 2000 Developer Version or higher.
4. Windows XP or newer with IIS installed.

## Installation
1. Download the published release version here.
2. Copy to your local file directory and make it a web application

### Database Installation
The GDS2 database size is huge and Github doesn't allow big file to be hosted in its repositories, so I stored the database in DropBox instead.

1. Download the backup file [from DropBox here](https://www.dropbox.com/s/qz1qdorm3k89uwe/gds.zip) and extract the zip file.
2. Restore it to your SQL Server

```
RESTORE DATABASE gds 
  FROM DISK = 'C:\YourLocalDirectory\gds.bak' 
```

### Source Code
* Download the [ZIP file](https://github.com/erlange/GDS-2-/archive/master.zip) or clone this repository:
```
mkdir [your-directory]
cd [your-directory]
git clone https://github.com/erlange/GDS-2-.git
```

You can then open the `.sln` and build the solution file with Visual Studio.


## Analysis Modes

![GDS2](./docimg/GDS-01.png "3 Analysis Modes of GDS2")

GDS2 provides 3 types of query which enable you to further analyse the data.

### Single Variable
![GDS2](./docimg/GDS-01-00.png "Single Variable Selection")

At the very basic this single variable analysis allows you to display the survey result for selected region from selected dataset.

![GDS2](./docimg/GDS-01-01.png "Result")

Results can then be displayed in chart and compared with other comparators such as geographical or socio-economic characteristics


### Scatter Plot

![GDS2](./docimg/GDS-02-01.png "2 Variables Selection")

Use this to determine correlation between two continuous (numeric) variables. 

![GDS2](./docimg/GDS-02-02.png "Scatter Plot")

Results will then be displayed in scatter-type chart with a trend line.


### Score Maker

![GDS2](./docimg/GDS-03-01.png "Score Maker Variables Selection")

Use this to score each region for up to five custom-weighted continuous (numeric) variables.

![GDS2](./docimg/GDS-03-02.png "Score Maker Variables Selection")
