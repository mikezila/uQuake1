using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class GenerateMap : MonoBehaviour
{
    public string mapName;
    private BSP29map map;
    private int faceCount = 0;
    private Stopwatch stopwatch;

    void Start()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        map = new BSP29map(mapName);
        UnityEngine.Debug.Log("Parsed bsp in: " + stopwatch.ElapsedMilliseconds.ToString());
        stopwatch.Reset();
        stopwatch.Start();
        foreach (BSPFace face in map.facesLump.faces)
        {
            GenerateFaceObject(face);
            faceCount++;
			
        }
        UnityEngine.Debug.Log("Created " + faceCount.ToString() + " GameObjects in: " + stopwatch.ElapsedMilliseconds.ToString());
        stopwatch.Stop();
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
            if (map.edgeLump.ledges[face.ledge_index + i] < 0)
            {
                verts[i] = map.vertLump.verts[map.edgeLump.edges[Mathf.Abs(map.edgeLump.ledges[edgestep])].vert1];
            }
            else
            {
                verts[i] = map.vertLump.verts[map.edgeLump.edges[map.edgeLump.ledges[edgestep]].vert2];
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

        // whip up uvs
        float scales = map.miptexLump.textures[map.texinfoLump.texinfo[face.texinfo_id].miptex].width * 0.03f;
        float scalet = map.miptexLump.textures[map.texinfoLump.texinfo[face.texinfo_id].miptex].height * 0.03f;
        Vector2[] uvs = new Vector2[face.num_ledges];
        for (int i = 0; i < face.num_ledges; i++)
        {
            uvs[i] = new Vector2((Vector3.Dot(verts[i], map.texinfoLump.texinfo[face.texinfo_id].vec3s) + map.texinfoLump.texinfo[face.texinfo_id].offs) / scales, (Vector3.Dot(verts[i], map.texinfoLump.texinfo[face.texinfo_id].vec3t) + map.texinfoLump.texinfo[face.texinfo_id].offt) / scalet);
        }

        faceMesh.vertices = verts;
        faceMesh.triangles = tris;
        faceMesh.uv = uvs;
        faceMesh.RecalculateNormals();
        faceObject.AddComponent<MeshFilter>();
        faceObject.GetComponent<MeshFilter>().mesh = faceMesh;
        faceObject.AddComponent<MeshRenderer>();
        faceObject.renderer.material.mainTexture = map.miptexLump.textures[map.texinfoLump.texinfo[face.texinfo_id].miptex];
        faceObject.AddComponent<MeshCollider>();
        string texName = faceObject.renderer.material.mainTexture.name;
        if (texName == "trigger" || texName == "skip")
        {
            faceObject.collider.enabled = false;
            faceObject.renderer.enabled = false;
        }
        faceObject.isStatic = true;
    }
}
