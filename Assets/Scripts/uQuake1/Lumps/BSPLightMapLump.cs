using System.Text;
using UnityEngine;

public class BSPLightMapLump
{
    public BSPLightMap[] Maps { get; set; }
    public byte[] RawMaps { get; set; }

    public BSPLightMapLump()
    {
    }

    public void DebugDexture()
    {
        Debug.Log(RawMaps.Length);
        int size = 20;
        Texture2D megatex = new Texture2D(size, size);

        Color[] colors = new Color[size*size];

        for (int i = 0; i < size*size; i++)
        {
            byte temp = RawMaps[i];
            colors[i] = new Color32(temp, temp, temp, 255);
        }
        megatex.SetPixels(colors);

        GameObject debugQuad = GameObject.FindGameObjectWithTag("debugtex");
        debugQuad.GetComponent<MeshRenderer>().material.SetTexture("_LightMap", megatex);
    }
}

