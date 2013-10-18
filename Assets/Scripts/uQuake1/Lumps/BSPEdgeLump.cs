using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class BSPEdgeLump
{
    public BSPEdge[] edges;
    public int[] ledges;

    public BSPEdgeLump()
    {
    }

    public void PrintInfo()
    {
        Debug.Log("Edges:\r\n");
        foreach (BSPEdge edge in edges)
        {
            Debug.Log(edge.ToString());
        }

        Debug.Log("Ledges:\r\n");
        foreach (short ledge in ledges)
        {
            Debug.Log(ledge.ToString());
        }
    }
}

