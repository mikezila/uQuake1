using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BSPVertexLump
{
    public Vector3[] verts;

    public BSPVertexLump()
    {
    }
}

public static class VertHelper
{
    public static Vector3 QSwizzle(this Vector3 vert)
    {
        vert.Scale(new Vector3(0.03f, 0.03f, 0.03f));
        float tempx = -vert.x;
        float tempy = vert.z;
        float tempz = -vert.y;
        return new Vector3(tempx, tempy, tempz);
    }
}

