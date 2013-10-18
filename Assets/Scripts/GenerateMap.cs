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
        GameObject faceObject = new GameObject("BSPface " + faceCount.ToString());
        faceObject.transform.parent = gameObject.transform;
        Mesh faceMesh = new Mesh();
        faceMesh.name = "BSPmesh";

        // grab our verts
        Vector3[] verts = new Vector3[face.num_ledges];
        int edgestep = face.ledge_index;
        for (int i = 0; i < face.num_ledges; i++)
        {
            if (map.edges.ledges[face.ledge_index + i] < 0)
            {
                verts[i] = map.verts.verts[map.edges.edges[Mathf.Abs(map.edges.ledges[edgestep])].vert1];
            }
            else
            {
                verts[i] = map.verts.verts[map.edges.edges[map.edges.ledges[edgestep]].vert2];
            }
            edgestep++;
        }

        // whip up tris
        int[] tris = new int[(face.num_ledges - 2) * 3];
        int tristep = 1;
        for (int i = 1; i < verts.Length - 1; i++)
        {
            tris[tristep - 1] = 0;
            tris[tristep] = i;
            tris[tristep + 1] = i + 1;
            tristep += 3;
        }

        float scales = map.miptex.textures[map.texinfo.texinfo[face.texinfo_id].miptex].width * 0.03f;
        float scalet = map.miptex.textures[map.texinfo.texinfo[face.texinfo_id].miptex].height * 0.03f;
        // whip up uvs
        Vector2[] uvs = new Vector2[face.num_ledges];
        for (int i = 0; i < face.num_ledges; i++)
        {
            //uvs[i] = new Vector2(Vector3.Dot(verts[i], map.texinfo.texinfo[face.texinfo_id].vec3s) + map.texinfo.texinfo[face.texinfo_id].offs, Vector3.Dot(verts[i], map.texinfo.texinfo[face.texinfo_id].vec3t) + map.texinfo.texinfo[face.texinfo_id].offt);
            uvs[i] = new Vector2((Vector3.Dot(verts[i], map.texinfo.texinfo[face.texinfo_id].vec3s) + map.texinfo.texinfo[face.texinfo_id].offs) / scales, (Vector3.Dot(verts[i], map.texinfo.texinfo[face.texinfo_id].vec3t) + map.texinfo.texinfo[face.texinfo_id].offt) / scalet);
        }

        faceMesh.vertices = verts;
        faceMesh.triangles = tris;
        faceMesh.uv = uvs;
        faceMesh.RecalculateNormals();
        faceObject.AddComponent<MeshFilter>();
        faceObject.GetComponent<MeshFilter>().mesh = faceMesh;
        faceObject.AddComponent<MeshRenderer>();
        faceObject.renderer.material.mainTexture = map.miptex.textures[map.texinfo.texinfo[face.texinfo_id].miptex];
        faceObject.AddComponent<MeshCollider>();
        faceObject.isStatic = true;
    }
}
