using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class BSPModel
{
    public int numLeafs;
    public int[] nodes;

    public BSPModel(int[] nodes, int numLeafs)
    {
        this.nodes = nodes;
        this.numLeafs = numLeafs;
    }
}

