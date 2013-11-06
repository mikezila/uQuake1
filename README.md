uQuake1
=======

Load Quake 1 .bsp maps into Unity3D

### I can use this to load Quake maps in Unity?

Yep.  Put the .bsp (not a .zip, not a .pak) into the Resources/Maps folder and then change the map name in the worldspawn object in the scene.  Hit play and the magic happens.  Unity seems to drop the ball with saving prefabs for some reason, so if the sceen has missing scripts on it or whatever, just make a new scene, put an empty in it, and then attach the "generate map vis" script on it.  Put this empty at the world origin, and put the name of the map you want to load into the map name field on the script in the inspector.  If you want to fly around the map there's a "mouse cam" script you can put on a camera to zip around using the mouse.  You can also place a character controller if you'd like to walk around, though keep in mind doors and triggers don't work.

### Is this complete?

No, but a lot is already done.

Faces and their textures are reconstructed at run-time using only the data in .bsp, no conversion or re/de-compilation is needed.  The only external file used is a binary lump containing the Quake palette, which is the same binary lump used by offical Quake and more traditional engines.

"vis" data is used, so only visible objects are rendered.  Vis support is still a little buggy, and incomplete.  Currently models other than the world itself (such as doors and moving switches) will be missing (or rather their renderer's are turned off) because I am currently only walking the bsp tree for the world itself, when each of these sub-models has it's own to walk, and then a visible set to render.  This will be simple to fix, though.

Lightmaps aren't parsed or used yet.  Entities aren't parsed or used either, but I'd like to at least place the player spawn.  I think recreating the whole engine with switches and monsters and guns is out of my reach, but we'll see.

### Why?

Fun.  I love the original Quake and I'm enjoying working on a project involving it.  Once I get lightmaps working, with Unity's awesome image effects it may be a cool way to take screens of upcoming singleplayer maps.  It's possible it'll see use as a map development tool.  It'd be easy to make visual markers for things like leaves, and it makes it easy to examine your pvs, so it may perhaps help people who develop map compilation tools examine their results more visually.  Honestly I'm just making it as a programming exercise involing a game I love.  Any real use it does see will be icing on the cake.

### Does this work with all maps?

It should, but there are issues with some.  There are issues parsing the textures of some maps, and I'm not sure why.  Currently only classic, good-ol BSP v29 maps are supported, but support for BSP2 will be pretty trivial to add once I have some other parts cleaned up.

### Does this work with Quake2 or Quake3 maps?  Half-Life?

No.  I have a sister project to this one that can load Quake 3 maps in the same way.  It's here: https://github.com/mikezila/uQuake  I'd love to support Half-Life maps, but it's not a high priority.  As for .bsp for higher/newer bsp engines, like Source and beyond, I'm not sure.  I doubt it.  Source maps are kind of complex, and so much of their visual presentation is reliant on image effects and props.  It would be difficult to make a meaninful loader for those maps that wasn't just a Source engine simulator in Unity.  Which actually sound kind of fun, but I think Quake is much softer target.

### Can I use this for xyz?

Yes, please.  You can use this to do anything you like.  I didn't write many code comments, and I admit that some parts of it are a little write-only, as even I'm a little mystified by some of it already (mainly the pvs/vis system, it's a mess) but the classes to read the bsp's data are pretty self explanatory, and should be pretty simple to re-use in a different project.  But yes you're free to take this code and do whatever you want with it.  I don't need any credit or anything, just have fun.  If you do use it please tell me, only because I'd love to see what you make with it.
