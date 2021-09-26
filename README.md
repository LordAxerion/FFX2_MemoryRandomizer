# FFX2_MemoryRandomizer
A FFX-2 randomizer that randomizes gotten dressspheres and garmentgrids in memory.

## How to run it
- To run the code either compile it yourself or get the published executable.
- Start your game, then start the exe. The program will attach itself to your game process automatically.
- The application will ask you what you want to randomize, you can randomize dressspheres and/or garmentgrids.
- Keep the application running while you play the game.
- The program will automatically create save files in its root location called DresssphereRando.txt. and GGRando.txt
- At the start of the program it will attempt to read that file. If you want to start anew move it somewhere else, rename or delete it. 
- That's it, have fun :)

One more note: The randomizer cannot randomize the intro (at least not yet). You have to finish the intro and once you are on the airship, open the menu and remove all dressspheres from the First Steps garment grid. This will remove the Dressspheres from your list and instead you will have the randomized ones.   
If you randomize the GarmentGrids you will have to remove the FirstStep GG from the girls equipment and replace it with your new one.   
If you randomize both, you might want to remove the DS from FirstStep once you got it, since the old one (Thief, Warrior, Songstress, Gunner) will still be on it, even if you don't have them.

**Please note that the Garment Grid randomizing currently doesn't work as intended, you will get more GGs than you should have**

## Known Bugs
If you open up the Tutorial from Shinra (for example to get Vanguard) you will have your Dresspheres scrambled. As of now I didn't come up with a good idea to fix this.  
- You can either get Vanguard, then start the Mod and let your Dressspheres be randomized. (Since it is only possible after the Intro anyway)  
- Or you just remember to stop the randomizer whenever you open the Tutorial and start it again once you left the menu.  
- If you don't mind that you'll have a few more DS (of the same type) than you should have, you can choose to ignore this bug.  

Sorry for the inconvinience. 

## FAQ
**Will there be a GUI at some point?**
- Actually yes, since we already have a collaborator willing to do that :D

**What does the mod do exactly? Will it mess with my game?**
- The mod is working in the games process memory. It doesn't touch the binary files, or any ressources, which means that if you start a new game without the mod attached, everything will work as intended. 
- If you save a game in which you used the mod you will have the same randomized dressspheres even if the mod isn't running anymore.

**The game said I picked up Dressphere XX, but I got a different one.**
- Yes the game will give you the ones which you should get, for example Festivalist from Brother after talking to him. The Mod will then switch it with something else. Currently it is not possible to see what you got in the pickup text. Once you open your menu you will see that you got a random one.

**One of my dresspheres is what it should be instead of a random one.**
- Note that sometimes the random algorithm shuffles the same dresssphere on the index it was at before. It usually happens only to one.

**What are you currently working on?**
- Fixing the GG randomization which just blew the project up big time :'D
- There is a GUI coming ;)
- To get my mind off of GGs, I will also randomize the abilities

**Something really bugs me! >:(**
- That is fine, open an issue or contact me and I will try to solve your problem. You can also contribute code yourself :)

**Can I contribute?**
- Of course, I made this because I wanted a randomizer for this game, the better it gets, the happier I am.

**Can I copy your code, or sell it?**
- Look in the license file. You can do whatever you want, but being named would be nice.

If you have any further questions feel free to contact me on Discord: Earinor#4167
