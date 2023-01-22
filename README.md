# IL 2 Career Toolset

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/2f639adcc2c64220acea31837f5e5d80)](https://www.codacy.com/gh/XanatosX/IL2CarrerReviver/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=XanatosX/IL2CarrerReviver&amp;utm_campaign=Badge_Grade)

**This readme is still WIP I did want to get nighly build's up ASAP**

This tool will provide you different commands to performe on your save file.
One of the major features is to revive a dead pilot by resetting the carreer to the day before
death.

## Warning

This tool will change, delete and move data in your save file around, which can corrupt it.
This can lead to unwanted behaviors of the game or make the save useless.
Those errors could appear later and may be invisible directly after usage.
Command's which change your save file will present you a warning you need to comfirm before the
command will run.

## Command list

You need to open a cmd or powershell to run the commands, to do so press windows key and search for
powershell or cmd. Navigate to the folder contaning the exe of the toolset by `cd {Folder name you extracted the zip to}`. 
Use the commands listed below to use the tool

### Settings

#### Set Log level

`IL2CareerToolset.exe settings loglevel {loglevel}`

The following log levels are allowed:

- Trace
- Debug
- Information
- Warning
- Error
- Critical

On default the app will use `warning`. The logfile is getting saved in `%appdata%\IL2CareerToolset`.
Just enter the string into the navigation bar in your explorer to get there.

#### Automatic search for steam savegame

`IL2CareerToolset.exe settings auto`

This command will try to automatically detect your database file, this will only work if you use a steam installation.

**Keep in mind that this command will scan all your discs and folders to find the game, no data will be uploaded.**

#### Enter Savegame manually

`IL2CareerToolset.exe settings manuell`

This command will allow you to set the game folder yourself.

### Pilots

#### Get Pilots

`IL2CareerToolset.exe save game pilot [Arguments] [Flags]`

This command will list all the pilots of all your save games. there are some possibilities for filtering

##### Arguments

| Position | Required |              Description            |
| -------- | -------- | ----------------------------------- |
|    1     | OPTIONAL | The name of the pilot to search for |

##### Flags

|     Flag     |       Description       |
| ------------ | ----------------------- |
| -p|--player  | Only show player pilots |

#### Revive Pilots

`IL2CareerToolset.exe save game pilot [Flags]`

Revive a pilot from your save game, please create a backup first. Keep in mind that this action could destroy your save.
The command will ask you which pilot to revive as you run it. It will ask you to confirm the revive and give you an overview 
if it was successful.

##### Flags

|      Flag     |         Description         |
| ------------- | --------------------------- |
| -i|--ironman  | Include iron man characters |

### Backup

#### List Backup's

`IL2CareerToolset.exe save backup list`

Get a table with all the backups created for your game.

#### Create Save Backup

`IL2CareerToolset.exe save backup create [Arguments]`

Create a new backup for your game and store it to the backup folder `%appdata%\IL2CareerToolset\backups`.

##### Arguments

| Position | Required |             Description          |
| -------- | -------- | -------------------------------- |
|    1     | OPTIONAL | The name of the backup to create |

#### Delete Backup

`IL2CareerToolset.exe save backup delete [Arguments] [Flags]`

Delete a single backup or all of them, if no arguments provided the program will ask for a backup to delete.


##### Arguments

| Position | Required |             Description          |
| -------- | -------- | -------------------------------- |
|    1     | OPTIONAL | The guid of the backup to delete |

> :information_source: To get the guid for a backup use the `list` command first

##### Flags

|    Flag  |             Description             |
| -------- | ----------------------------------- |
| -a|--all | Delete all the backups for the game |

#### Rename Backup's

`IL2CareerToolset.exe save backup rename [Arguments]`

Rename a backup. If no arguments provided the program will ask you for a backup to rename and the new name to use

##### Arguments

| Position | Required |              Description           |
| -------- | -------- | ---------------------------------- |
|    1     | OPTIONAL | The guid of the backup to rename   |
|    2     | OPTIONAL | The new name to use for the backup |

> :information_source: To get the guid for a backup use the `list` command first

#### Restore Backup

`IL2CareerToolset.exe save backup restore [Arguments]`

Restore a backup. If no argument provided the program will show a list with all the backups you could restore

##### Arguments

| Position | Required |               Description           |
| -------- | -------- | ----------------------------------- |
|    1     | OPTIONAL | The guid of the backup to restore   |

> :information_source: To get the guid for a backup use the `list` command first

# Report Bugs

Go to [issues] to report any bugs you encounter.

# License

Read the [license] file for more information

[license]: LICENSE
[issues]: https://github.com/XanatosX/IL2CarrerReviver/issues