using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class BSPLeaf
{
    public int type;
    public int vislist;
    public Vector3 mins;
    public Vector3 maxs;
    public int lface_index;
    public int num_lfaces;

    public BSPLeaf(int type, int vislist, Vector3 mins, Vector3 maxs, ushort lface_index, ushort num_lfaces)
    {
        this.type = type;
        this.vislist = vislist;
        this.lface_index = (int)lface_index;
        this.num_lfaces = (int)num_lfaces;

        this.mins = SwizVert(mins);
        this.maxs = SwizVert(maxs);
    }

    private Vector3 SwizVert(Vector3 vert)
    {
        vert.Scale(new Vector3(0.03f, 0.03f, 0.03f));
        float tempx = -vert.x;
        float tempy = vert.z;
        float tempz = -vert.y;
        return new Vector3(tempx, tempy, tempz);
    }

    public override string ToString()
    {
        return "Type: " + type.ToString() + " Vislist: " + vislist.ToString() + " Mins/Maxs: " + mins.ToString() + " / " + maxs.ToString();
    }
}
