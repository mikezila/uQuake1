using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BSPPlaneLump
{
    public BSPPlane[] planes;

    public BSPPlaneLump() { }

    public void PrintInfo()
    {
        foreach (BSPPlane plane in planes)
        {
            Debug.Log(plane.ToString());
        }
    }
}

