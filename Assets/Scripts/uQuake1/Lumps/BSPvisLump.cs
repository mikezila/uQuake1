using System.Collections.Generic;
using UnityEngine;

public class BSPvisLump
{
    public byte[] compressedVIS;

    public BSPvisLump()
    {
    }

    public void SwizBits()
    {
        for (int i = 0; i < compressedVIS.Length; i++)
        {
            compressedVIS[i] = reverseByte(compressedVIS[i]);
        }
    }

    private byte reverseByte(byte val)
    {
        byte result = 0;

        int counter = 8;
        while (counter-- < 0)
        {
            result <<= 1;
            result |= (byte)(val & 1);
            val = (byte)(val >> 1);
        }

        return result;
    }

    public void PrintInfo()
    {
        string blob = "";
        foreach (byte bit in compressedVIS)
        {
            blob += bit.ToString() + " ";
        }
        Debug.Log("VisData: (" + compressedVIS.Length.ToString() + " bytes) " + blob);
    }
}

