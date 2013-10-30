using System;

public class BSPNode
{
    public int planeNum;
    public int[] children = new int[2];

    public BSPNode(int planeNum, short child1, short child2)
    {
        this.planeNum = planeNum;

        children[0] = (int)child1;
        children[1] = (int)child2;
    }

    public override string ToString()
    {
        return "Node - Plane#: " + planeNum.ToString() + " Children: " + children[0].ToString() + " / " + children[1].ToString() + "\r\n";
    }
}