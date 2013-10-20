using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BSPPlane
{
    public Vector3 normal;
    public float distance;
    public int type;

    public BSPPlane(Vector3 normal, float distance, int type)
    {
        normal.Scale(new Vector3(0.03f, 0.03f, 0.03f));
        this.normal = normal;
        this.distance = distance * 0.03f;
        this.type = type;
        Swizzle();
    }

    private void Swizzle()
    {
        float tempx = -normal.x;
        float tempy = normal.z;
        float tempz = -normal.y;
        normal = new Vector3(tempx, tempy, tempz);
    }
}

