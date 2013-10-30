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
    public Plane plane;

    public BSPPlane(Vector3 normal, float distance, int type)
    {
        this.normal = -normal; // Invert the normal because of reasons.  Really I'm not sure why, Unity/Quake's vector3 systems don't get along.
        this.distance = distance * 0.03f;
        this.type = type;
        Swizzle();
        this.plane = new Plane(this.normal, this.distance);
    }

    public override string ToString()
    {
        return "Normal: " + plane.normal.ToString() + " D: " + plane.distance.ToString();
    }

    private void Swizzle()
    {
        float tempx = -normal.x;
        float tempy = normal.z;
        float tempz = -normal.y;
        normal = new Vector3(tempx, tempy, tempz);
        normal.Scale(new Vector3(0.03f, 0.03f, 0.03f));
    }
}

