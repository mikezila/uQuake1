using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class BSPFaceLump
{
    public List<BSPFace> faces = new List<BSPFace>();

    public BSPFaceLump()
    {
    }

    public void PrintInfo()
    {
        Debug.Log("Faces:\r\n");
        foreach (BSPFace face in faces)
        {
            Debug.Log(face.ToString());
        }
    }
}

