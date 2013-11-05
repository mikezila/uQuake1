using UnityEngine;

public class BSPModelLump
{
    public BSPModel[] models;

    public BSPModelLump() { }

    public void PrintInfo()
    {
        Debug.Log("Models:\r\n");
        foreach (BSPModel model in models)
        {
            Debug.Log("Model - Leafs: " + model.numLeafs.ToString() + " Nodes: " + model.nodes[0] + ", " + model.nodes[1] + ", " + model.nodes[2] + ", " + model.nodes[3]);
        }
    }
}

