# Mp3 Join Utility

Utility for joining mp3 files from subdirectories into one file

Notes:

- Files and directories should be named correctly for correct order of mp3 parts during joining
- Work on Linux/MacOS not tested
- For Windows reused AOT (CoreRT) <https://github.com/dotnet/corert/tree/master/samples/WebApi>

## How it works

### Step 1

- RootDir
  - SubDir_01
    - 01_01_File.mp3
    - 01_02_File.mp3
  - SubDir_02
    - 02_01_File.mp3
    - 02_02_File.mp3
    - 02_03_File.mp3

### Step 2

- RootDir
  - SubDir_01.mp3
  - SubDir_01
    - 01_01_File.mp_
    - 01_02_File.mp_
  - SubDir_01.mp3
  - SubDir_02
    - 02_01_File.mp_
    - 02_02_File.mp_
    - 02_03_File.mp_

### Step 3

- RootDir
- RootDir.mp3
  - SubDir_01.mp_
  - SubDir_01
    - 01_01_File.mp_
    - 01_02_File.mp_
  - SubDir_01.mp_
  - SubDir_02
    - 02_01_File.mp_
    - 02_02_File.mp_
    - 02_03_File.mp_

## How to build

NOTES: Catalog of RID - <https://docs.microsoft.com/en-us/dotnet/core/rid-catalog>

- build
- publish for platform (dotnet publish -r \<RID\> -c \<Configuration\>)
- run

Example for Windows x64:

```shell
dotnet build
dotnet publish -r win-x64 -c release
```

## How to run

- run 'Mp3join "\<path\>"'
- get result in root directory
- remove original files

Example for Windows x64:

```shell
Mp3join "path"
```