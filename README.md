# FFX2_MemoryRandomizer
A FFX-2 randomizer that randomizes gotten dressspheres in memory.

## How to run it
- To run the code either compile it yourself or get the published executable.
- Start your game, then start the exe. The program will attach itself to your game process automatically.
- Keep the application running while you play the game.
- The program will automatically create a save file in its root location called Mapping.txt. 
- At the start of the program it will attempt to read that file. If you want to start anew move it somewhere else, rename or delete it. 
- You can switch multiple save files by just renaming the one you want to use to "Mapping.txt" and the others to something else
- If you load a save make sure to load the save in the game first and then start the program or it could mess up the mapping
- That's it, have fun :)

One more note: The randomizer cannot randomize the intro (at least not yet). You have to finish the intro and once you are on the airship, open the menu and remove all dressspheres from the First Steps garment grid. This will remove the Dressspheres from your list and instead you will have the randomized ones.

## FAQ
Will there be a GUI at some point?
- No, unless you write one yourself ;)

What does the mod do exactly? Will it mess with my game?
- The mod is working in the games process memory. It doesn't touch the binary files, or any ressources, which means that if you start a new game without the mod attached, everything will work as intended. 
- If you save a game in which you used the mod you will have the same randomized dressspheres even if the mod isn't running anymore.

The game said I picked up Dressphere XX, but I got a different one.
- Yes the game will give you the ones which you should get, for example Festivalist from Brother after talking to him. The Mod will then switch it with something else. Currently it is not possible to see what you got in the pickup text.

What are you currently working on?
- Randomizing Garment Grids

Can I contribute?
- Of course, I made this because I wanted a randomizer for this game, the better it gets, the happier I am.

Can I copy your code, or sell it?
- Look in the license file. You can do whatever you want, but being named would be nice of you.

If you have any further questions feel free to contact me on Discord: Earinor#4167
