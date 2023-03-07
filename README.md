# Hive-demo
A simple demo of a c# color bot for osrs.

# Plugins
Hive use kinput for remote input to the game.
You can find kinput here https://github.com/Kasi-R/KInput
Download the files from the plugin folder and place them within the plugin folder of hive.

# Functions
The demo includes the very basic function required for simple task like powermining.  
Some of the function included:  
•Get closest object based on color to the player(this is closest based on screen coordinates, so can be wonky sometimes).  
•Drop inventory.  
•Check the last inventory slot to see if inventory is full.  
•Check for a xp drop.  
•Pixel searching for all of one color in a rectangle, or 1 single pixel if a color, both with a tolorance.  
•Framework for multi client support.  

# Usage
•Get the pid of the rl client and replace both 12584 with your pid on line 18 in IRON_PM.cs.  
•In remoteIO.cs change the file directory with the one for your plugin folder, pointing to kinputctrl.dll.(going to change this as soon as I can)  
•Client must be in fixed mode.  
•Xp drops changed to cyan and set on fastest.

# Scripts
Included script is a very simple power miner.  
Simple start next to some iron rocks and it will power mine till you exit.  
