using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BSPMipTexLump
{
    public int tex_count;
    public int[] tex_offsets;
    public BSPMipTexture[] texture_headers;
    public Texture2D[] textures;
    private int loaded_tex = 0;

    public BSPMipTexLump(int tex_count)
    {
        this.tex_count = tex_count;
        tex_offsets = new int[tex_count];
        texture_headers = new BSPMipTexture[tex_count];
        textures = new Texture2D[tex_count];
    }

    public void LoadOffset(int offset)
    {
        tex_offsets[loaded_tex] = offset;
        loaded_tex++;
    }

    public void PrintInfo()
    {
        Debug.Log("TexCount: " + tex_count.ToString() + "/" + loaded_tex.ToString() + " Offsets:\r\n");
        foreach (int i in tex_offsets)
        {
            Debug.Log(i.ToString());
        }

        Debug.Log("Textures:\r\n");
        foreach (BSPMipTexture tex in texture_headers)
        {
            Debug.Log("H/W: " + tex.height.ToString() + "/" + tex.width.ToString() + " Name: " + tex.name);
        }
    }
}

