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
            Debug.Log("Model: " + model.numLeafs.ToString() + "\r\n");
        }
    }
}

