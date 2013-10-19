using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BSPvis
{
    public int[] pvs;

    public BSPvis()
    {
    }

    public void PrintInfo()
    {
        Debug.Log("Vis Data:\r\n");
        string blob = "";
        foreach (int i in pvs)
        {
            blob += i.ToString();
        }
        Debug.Log(blob);
    }
}

