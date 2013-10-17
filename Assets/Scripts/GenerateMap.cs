using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateMap : MonoBehaviour
{
    public Material replacementTexture;
    public string mapName;
    private BSP29map map;

    void Start()
    {
        map = new BSP29map(mapName);

        foreach (BSPFace face in map.faces.faces)
        {
            GenerateFaceObject(face);
        }
    }

    void GenerateFaceObject(BSPFace face)
    {
        GameObject faceObject = new GameObject("BSPface");
        Mesh faceMesh = new Mesh();
        faceMesh.name = "BSPmesh";
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();

        // Find and grab verts
        int edge_step = face.ledge_index;
        for (int i = 0; i < face.num_ledges; i++)
        {
            if (map.edges.ledges[edge_step] > 0)
            {
                verts.Add(map.verts.verts[map.edges.edges[map.edges.ledges[edge_step]].vert1]);
            }
            else
            {
                verts.Add(map.verts.verts[map.edges.edges[Mathf.Abs(map.edges.ledges[edge_step])].vert2]);
            }
            edge_step++;
        }

        // whip up tris
        for (int i = 1; i < (verts.Count - 1); i++)
        {
            tris.Add(0);
            tris.Add(i);
            tris.Add(i + 1);
        }

        faceMesh.vertices = verts.ToArray();
        faceMesh.triangles = tris.ToArray();
        faceMesh.RecalculateNormals();
        faceMesh.RecalculateBounds();
        faceMesh.Optimize();
        faceObject.AddComponent<MeshFilter>();
        faceObject.GetComponent<MeshFilter>().mesh = faceMesh;
        faceObject.AddComponent<MeshRenderer>();
        faceObject.renderer.material = replacementTexture;
    }
}
