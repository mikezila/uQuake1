uQuake1
=======

Load Quake 1 .bsp maps into Unity3D

### I can use this to load Quake maps in Unity?

Yep.  Put the .bsp (not a .zip, not a .pak) into the Resources/Maps folder and then change the map name in the worldspawn object in the scene.  Hit play and the magic happens.

### Is this complete?

Kind of.  Lightmaps aren't loaded yet, and you can't actually play.  You just fly around in the map.

### So...why then?

Fun.  I love the original Quake am I'm enjoying working on a project involing it.  Once I get lightmaps working, with Unity's awesome image effects it may be a cool way to take screens of upcoming singleplayer maps.  It also has tools to visualize where leafs are, so it can help optimze performance as well if you load it up and notice a ton of leafs being generated in an area due to odd geometry.  Honestly I'm just making it as a programming exercise involing a game I love.

### Does this work with all maps?

It should, but there are issues with some.  It looks like maps that use weird textures might not work, and I'm not sure why.  Still looking into it.  Most modern maps work.  It takes a bit to start up on huge maps if you're running it inside the editor, so be patient.  Even massive maps load very quickly if you compile/build and run the build instead.  Performance isn't that great on huge maps, but again be patient, I'm working on it.

### Does this work with Quake2 or Quake3 maps?  Half-Life?

No.  I have a sister project to this one that can load Quake 3 maps in the same way.  It's here: https://github.com/mikezila/uQuake  I'd love to support Half-Life maps, but it's not a high priority.
