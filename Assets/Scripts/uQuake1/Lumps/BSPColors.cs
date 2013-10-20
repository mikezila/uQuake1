using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

public class BSPColors
{
    public Color32[] colors = new Color32[256];
    private BinaryReader colorLump;
    public Texture2D debugTex;

    public BSPColors()
    {
        colorLump = new BinaryReader(File.Open("Assets/Resources/id1/palette.lmp", FileMode.Open));
        RipColors();
        colorLump.BaseStream.Dispose();
        //DebugTex();
    }

    private void RipColors()
    {
        for (int i = 0; i < 256; i++)
        {
            colors[i] = new Color32(colorLump.ReadByte(), colorLump.ReadByte(), colorLump.ReadByte(), (byte)0.0f);
        }
    }

    private void DebugTex()
    {
        debugTex = new Texture2D(16, 16);
        debugTex.SetPixels32(colors);
        debugTex.Apply();
    }
}

