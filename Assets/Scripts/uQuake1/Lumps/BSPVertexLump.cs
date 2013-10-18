using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BSPVertexLump
{
    public Vector3[] verts;
    private int vert_count = 0;

    public BSPVertexLump()
    {
    }

    public void PrintInfo()
    {
        foreach (Vector3 vert in verts)
        {
            Debug.Log(vert.ToString());
        }
    }

    public void AddVert(Vector3 vert)
    {
        vert.Scale(new Vector3(0.03f, 0.03f, 0.03f));
        float tempx = -vert.x;
        float tempy = vert.z;
        float tempz = -vert.y;
        verts[vert_count]=(new Vector3(tempx, tempy, tempz));
        vert_count++;
    }
}

