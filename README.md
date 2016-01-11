uQuake1
=======

Load Quake 1 .bsp maps into Unity3D

### I can use this to load Quake maps in Unity?

Yep.  Put the .bsp (not a .zip, not a .pak) into the Resources/Maps folder and then change the map name in the worldspawn object in the scene.  Hit the button in the editor and the magic happens.  Unity seems to drop the ball with saving prefabs for some reason, so if the sceen has missing scripts on it or whatever, just make a new scene, put an empty in it, and then attach the "generate map vis" script on it.  Put this empty at the world origin, and put the name of the map you want to load into the map name field on the script in the inspector.  If you want to fly around the map there's a "mouse cam" script you can put on a camera to zip around using the mouse.  You can also place a character controller if you'd like to walk around, though keep in mind doors and triggers don't work.

### Is this complete?

No, but a lot is already done.

Faces and their textures are reconstructed using only the data in .bsp, no conversion or re/de-compilation is needed.  The only external file used is a binary lump containing the Quake palette, which is the same binary lump used by offical Quake and more traditional engines.

"vis" data is not used in any way.  I had originally meant to use the visible sets data in the .bsp to cull geometry to boost performance, but it proved to be a worthless effort.  On anything more powerful than an Intel HD 4000 you can render even the largest maps wholesale with no slowdown at all.

No entities are recreated or used.

Lightmaps from the .bsp aren't used either.  I'm still looking for some documentation on how they are stored so I can figure out how to recreate them in Unity.  From what I gather it's a bit more complex that pulling the texture data.

### Why?

Fun.  I love the original Quake and I'm enjoying working on a project involving it.  Honestly I'm just making it as a programming exercise involing a game I love.  Any real use it does see will be icing on the cake.

### Does this work with all maps?

It *should*, but there are issues with some custom maps.  I don't fully understand why, but it seems to choke on some maps custom textures.  It was developed to match the spec of BSP v29, but in theory it should also be able to load maps that require increased limits.

### Does this work with Quake2 or Quake3 maps?  Half-Life?

No.  I have a sister project to this one that can load Quake 3 maps in the same way.  It's here: https://github.com/mikezila/uQuake  I'd love to support Half-Life maps, but it's not a high priority.  As for .bsp for higher/newer bsp engines, like Source and beyond, I'm not sure.  I doubt it.  Source maps are kind of complex, and so much of their visual presentation is reliant on image effects and props.  It would be difficult to make a meaninful loader for those maps that wasn't just a Source engine simulator in Unity, which is beyond the scope of what I'm passionate about

### Can I use this for xyz?

Yes, please.  You can use this to do anything you like.  I didn't write many code comments, but if you can make sense of it and use it, please do.  This project is in the public domain.  No permission from me is needed to do anything.

### Can I pay you to make a version of this for XYZ game?

Maybe, but not likely.  Email me with what you want and maybe I can work with you.  This project is really one of passion, so please don't be upset if I turn you down because whatever game you need this ported for just doesn't light my fire.  It's not personal, it's just that I do this for fun, and if the fun isn't there, then the project isn't happening.