using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BSPMipTexture
{
    public string name;
    public int width;
    public int height;
    public int offset;

    public int PixelCount { get { return width * height; } }

    public BSPMipTexture(char[] name, int width, int height, int offset)
    {
        this.name = new string(name);
        this.width = width;
        this.height = height;
        this.offset = offset;
    }
}

