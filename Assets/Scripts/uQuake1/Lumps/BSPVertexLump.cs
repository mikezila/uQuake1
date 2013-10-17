using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BSPVertexLump
{
    public List<Vector3> verts = new List<Vector3>();

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
        verts.Add(new Vector3(tempx, tempy, tempz));
    }
}

