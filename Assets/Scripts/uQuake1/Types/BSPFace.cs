using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BSPFace
{
    public short plane_id;
    public short side;
    public int ledge_index;
    public short num_ledges;
    public short texinfo_id;
    public char typelight;
    public char baselight;
    public char[] light = new char[2];
    public int lightmap;

    public BSPFace(int ledge_index, short num_ledges, short texinfo_id, byte[] lightstyles, int lightmap)
    {
        this.ledge_index = ledge_index;
        this.num_ledges = num_ledges;
        this.texinfo_id = texinfo_id;
        this.lightmap = lightmap;
    }

    public override string ToString()
    {
        return "EdgeListIndex: " + ledge_index.ToString() + " NumEdges: " + num_ledges.ToString() + " TexinfoIndex: " + texinfo_id.ToString() + "\r\n";
    }
}

