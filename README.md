# WillowTreeBLDEnhanced

Support Borderlands GOTY Enhanced Edition for WillowTree from sourceforge [repository](https://sourceforge.net/projects/willowtree/) 

## Getting Started

- You can download the latest realese [Here](https://github.com/Nicolas-Constanty/WillowTreeBLDEnhanced/releases)
- Extract the .zip in the folder of your choice
- Enter to your folder and execute WillTree.exe

:warning: Before using WillowTree you should make a copy of all your save! :warning:
You can find your save Here :
[User]\Documents\my games\borderlands\savedata for Borderlands GOTY
[User]\Documents\my games\Borderlands Game of the Year\Binaries\SaveData

WillowTree# v2.2.2
------------------------
Version 2.2  revisions performed by matt911, da_fileserver, and XanderChaos

WillowTree# was created by XanderChaos
WSG format research by matt911, Turk645, XanderChaos, and NicolasConstanty
X360 class by DJ Shepherd 
Major revisions by JackSchitt, matt911, da_fileserver and NicolasConstanty

WillowTree# is an open source project.  Visit the development site at [willowtree.sourceforge.net](https://sourceforge.net/projects/willowtree/).

-------------------------
Update v2.2.2
- Fixed loading of Borderlands GOTY Enhanced
- Fixed saving of Borderlands GOTY Enhanced
- Refacto of Item lists and Weapon lists with two class Item and Weapon (Items = List(Item) and Weapons = List(Weapon)) 
- Change Unknow2 to Head

-------------------------
Update v2.2.1
- Fixed a bug with Xbox saves in 2.2 not working when you used the Save button (Save As worked fine)
- Changed the UI interface for the Xbox ID a bit so it will show you the ID
- Changing save format now disables the Save button and will force you to use Save As
- Changing format to PS3 now explains that you will need other files to go with the save file

Update v2.2.0
- Savegame conversion has been implemented.  You can now write most saves as PC, PS3, or Xbox 360.
- Fixed inability to load Xbox 360 savegames at all in version 2.1 and 2.1.1 because of missing resource file.
- Window title updates now so you can look at it to know which character you are editing
- Fixed a bug in the item level/quality handling that resulted in Scorpio weapons being corrupted in all 2.1 revisions.

Update v2.1.1:
- Rewrote string handling to work with non-English strings.  A hotfix version 2.1.0.40 broke the PS3/Xbox string handling.  This fixes that.

Update v2.1.0:
- Complete rewrite of the DLC block load/save code
- Reviewed, tested, and revised all the rest of the savegame read/write code.  Should have fixed most remaining load/save issues.
- Fixed bank size corruption when there was only one echo log in a save.
- Locker saves item level, quality, and ammo now.
- Locker names will not change as much when you shut down and reload.
- Clipboard and file import/export  gets validated more so you shouldn't cause a crash with invalid data as easily.
- Lots of fixes to the  import/export/insert functions in the weapon, items, and locker tab of the UI.  Added some slider controls like the weapon and item panel for consistency.

Update v2.0.0:
- Moved the entire project from Multimedia Fusion to C#.
- New, user-friendly GUI.
- Merged Weapons Workshop into WillowTree.
- Skills, Quests, Ammo Pools, Echo Logs, and Locations are now fully editable. You can now add, edit, and remove items from each of these sections.
- More values are available to edit.
- Automatic backups of saves.
- Added the WillowTree Locker to keep track off weapons and items between saves. Weapons/Items can be given custom names, commented on, and rated. 

Update v1.2.2:
- Fixed the PC version file loading issue. 

Update v1.2.1:
- Slightly changed rebuilding code. Hopefully less bugs.
- Removed "Delete Item/Weapon" as it isn't working correctly.
- Added code to adjust important hash block info on the 360 version.
(Only Weapons Workshop was altered) 

Update v1.2.0:
- Added the new "Weapons Workshop" mode. It specificly edits weapons and items. Unlike the normal WillowTree mode, here you can add/remove/duplicate new weapons and items! It also includes a full list of parts (rather than the last version's limited list). Thanks to Turk there is a check on ItemGrades to make sure that you weapon won't revert to level 0.

Update v1.1.1:
- Added support for new DLC quests. 

Update v1.1.0:
- New organized decompiling process.
- Added an editor for the multiple parts of the save. It can quickly edit weapons, items, quests, player info, character colors, name, skills, locations, and invintory info.
- Added weapon and item importing/exporting from files or from the clipboard.
- Built-in parts swapper.
- Number of bugs fixed.
- PC support added. 

Update v1.0.1:
- Fixes a few minor bugs.
- Added PS3 save support.
