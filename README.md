uQuake1
=======

Load Quake 1 .bsp maps into Unity3D

### I can use this to load Quake maps in Unity?

Yep.  Put the .bsp (not a .zip, not a .pak) into the Resources/Maps folder and then change the map name in the worldspawn object in the scene.  Hit play and the magic happens.

### Is this complete?

Kind of.  Lightmaps aren't loaded yet, and you can't actually play.  You just walk around in the map.  You might have to pause the game as soon as you start it and move your character controller inside the bounds of the map.  Things like triggers and water end up being solid, and doors and things don't open, so you might have to move the controller around to tour a map.  Or just pause the game and fly around in the scene view.

### Does this work with all maps?

It should, but there are issues with some.  It looks like maps that use weird textures might not work, and I'm not sure why.  Still looking into it.  Most modern maps work.  It takes a bit to start up on huge maps, so be patient.  Performance isn't that great on huge maps, but again be patient, I'm working on it.

### Does this work with Quake2 or Quake3 maps?

No.  I have a sister project to this one that can load Quake 3 maps in the same way.  It's here: https://github.com/mikezila/uQuake
