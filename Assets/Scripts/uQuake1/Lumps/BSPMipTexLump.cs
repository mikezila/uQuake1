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
        Debug.Log("Textures:\r\n");
        for (int i = 0; i < texture_headers.Length; i++)
        {
            Debug.Log("H/W: " + texture_headers[i].height.ToString() + "/" + texture_headers[i].width.ToString() + " Offset: " + texture_headers[i].offset.ToString() + " Name: " + texture_headers[i].name);
        }
    }
}

