using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateMap : MonoBehaviour
{
    public Material replacementTexture;
    public string mapName;
    private BSP29map map;
    private int faceCount = 0;

    void Start()
    {
        map = new BSP29map(mapName);

        foreach (BSPFace face in map.faces.faces)
        {
            GenerateFaceObject(face);
            faceCount++;
        }
    }

    void GenerateFaceObject(BSPFace face)
    {
        GameObject faceObject = new GameObject("BSPface "+faceCount.ToString());
        Mesh faceMesh = new Mesh();
        faceMesh.name = "BSPmesh";
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        List<int> vertIndexes = new List<int>();
        List<int> ledges = new List<int>();

        Debug.Log("Face " + faceCount.ToString() + " :\r\n");
        Debug.Log("Should have: " + face.num_ledges.ToString() + " edges/verts");
        Debug.Log("Ledges:\r\n");
        for (int i = 0; i < face.num_ledges; i++)
        {
            ledges.Add(face.ledge_index + i);
            Debug.Log(ledges[i]);
        }

        foreach (int ledge in ledges)
        {
            int index = (int)map.edges.ledges[ledge];

            if (index < 0)
            {
                vertIndexes.Add(map.edges.edges[Mathf.Abs(index)].vert2);
            }
            else
            {
                vertIndexes.Add(map.edges.edges[index].vert1);
            }
        }

        Debug.Log("VertsIndexs:\r\n");
        foreach (int index in vertIndexes)
        {
            verts.Add(map.verts.verts[index]);
            Debug.Log(index.ToString());
        }
        Debug.Log("Verts:\r\n");
        foreach (Vector3 vert in verts)
        {
            Debug.Log(vert.ToString());
        }

        // whip up tris
        for (int i = 1; i < (verts.Count - 1); i++)
        {
            tris.Add(0);
            tris.Add(i);
            tris.Add(i + 1);
        }

        // make some uvs to debug with
        for (int i = 0; i < verts.Count; i++)
        {
            uvs.Add(new Vector2(0.0f,0.0f));
        }

        faceMesh.vertices = verts.ToArray();
        faceMesh.triangles = tris.ToArray();
        faceMesh.uv = uvs.ToArray();
        faceMesh.RecalculateNormals();
        faceMesh.RecalculateBounds();
        faceMesh.Optimize();
        faceObject.AddComponent<MeshFilter>();
        faceObject.GetComponent<MeshFilter>().mesh = faceMesh;
        faceObject.AddComponent<MeshRenderer>();
        faceObject.renderer.material = replacementTexture;
    }
}
