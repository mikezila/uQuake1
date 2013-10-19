using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BSPMarkSurfaces
{
    public int[] markSurfaces;

    public BSPMarkSurfaces()
    {
    }

    public void PrintInfo()
    {
        Debug.Log("MarkSurfaces:\r\n");
        foreach (int msurface in markSurfaces)
        {
            Debug.Log(msurface.ToString());
        }
    }
}
