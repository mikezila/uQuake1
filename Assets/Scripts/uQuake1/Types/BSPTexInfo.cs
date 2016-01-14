using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BSPTexInfo
{
    public Vector3 vec3s;
    public Vector3 vec3t;
    public float offs;
    public float offt;
    public int miptex;
    public int flags;

    public BSPTexInfo(Vector3 vs, float os, Vector3 vt, float ot, int miptex, int flags)
    {
        this.vec3s = vs;
        this.vec3t = vt;
        this.miptex = miptex;
        this.flags = flags;
        this.offs = os * 0.03f;
        this.offt = ot * 0.03f;

        Swizzle();
    }

    public override string ToString()
    {
        return "Vec3T: " + vec3t.ToString() + " OffT: " + offt.ToString() + " Vec3S: " + vec3s.ToString() + " OffS: " + offs.ToString() + " Miptex: " + miptex.ToString() + " Flags: " + flags.ToString();
    }

    // Quake and Unity use differing XYZ systems and scales.
    // Here we convert Quake-style ZYX to Unity-style XYZ
    // Call it Swizzlin'.
    private void Swizzle()
    {

        float tempx = -vec3s.x;
        float tempy = vec3s.z;
        float tempz = -vec3s.y;

        vec3s = new Vector3(tempx, tempy, tempz);

        tempx = -vec3t.x;
        tempy = vec3t.z;
        tempz = -vec3t.y;

        vec3t = new Vector3(tempx, tempy, tempz);
    }
}

