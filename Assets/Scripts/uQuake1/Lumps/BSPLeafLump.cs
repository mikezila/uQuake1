using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BSPLeafLump
{
    public BSPLeaf[] leafs;
    public int numLeafs;

    public BSPLeafLump()
    {
    }

    public void GetLeafInfo(int leaf)
    {
        Debug.Log("Leaf " + leaf.ToString() + " " + leafs[leaf].ToString());
    }

    public void PrintInfo()
    {
        Debug.Log("Leafs:\r\n");
        foreach (BSPLeaf leaf in leafs)
        {
            Debug.Log(leaf.ToString());
        }
    }
}

