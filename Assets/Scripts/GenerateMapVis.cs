using UnityEngine;
using System.Collections;

public class GenerateMapVis : MonoBehaviour
{
    public string mapName;
    private BSP29map map;
    private int faceCount = 0;
    private GameObject[][] leafRoots;
    private Transform player;

    void Start()
    {
        map = new BSP29map(mapName);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GenerateVisArrays();
        GenerateVisObjects();
    }

    void Update()
    {
        RenderPVS(WalkBSP());

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("In Leaf: " + WalkBSP().ToString() + " - " + map.leafLump.leafs[WalkBSP()].ToString());
        }
    }

    // This uses the bsp lookup method to find the leaf
    // the camera is in, and returns it.
    private int WalkBSP()
    {
        int child = BSPlookup(0);
        while (child >= 0)
        {
            child = BSPlookup(child);
        }

        child = -(child + 1);
        return child;
    }

    private void RenderPVS(int leaf)
    {
        //Debug.Log("Rendering PVS for Leaf: " + leaf.ToString());
        for (int i = 0; i < leafRoots.Length; i++)
        {
            foreach (GameObject go in leafRoots[i])
            {
                go.renderer.enabled = false;
            }
        }

        if (leaf != 0)
        {
            //bool[] PVS = GetCVS(map.leafLump.leafs[leaf].vislist);
        }


        foreach (GameObject go in leafRoots[leaf])
        {
            go.renderer.enabled = true;
        }

    }

    private bool[] GetCVS(int index)
    {
        bool[] CVS = new bool[map.leafLump.leafCount];
        for (int i = 0; i < CVS.Length; i++)
        {
            CVS[i] = false;
        }

        for (int i = 0; i < CVS.Length; i++)
        {
            if (map.visLump.pvs[index + i] == 0)
            {
                CVS[i] = true;
            }
            else
            {
                i += map.visLump.pvs[index + i];
                CVS[i] = true;
            }
        }

        return CVS;
    }

    // Walks the BSP tree, call this recursivly until you get a negative number
    private int BSPlookup(int node)
    {
        int child;
        if (!map.planeLump.planes[map.nodeLump.nodes[node].planeNum].plane.GetSide(player.position))
        {
            child = map.nodeLump.nodes[node].children[0];
        }
        else
        {
            child = map.nodeLump.nodes[node].children[1];
        }
        return child;
    }

    void GenerateVisArrays()
    {
        leafRoots = new GameObject[map.leafLump.leafCount][];
        for (int i = 0; i < map.leafLump.leafCount; i++)
        {
            leafRoots[i] = new GameObject[map.leafLump.leafs[i].num_lfaces];
        }
    }

    void GenerateVisObjects()
    {
        for (int i = 0; i < map.leafLump.leafCount; i++)
        {
            for (int j = 0; j < map.leafLump.leafs[i].num_lfaces; j++)
            {
                leafRoots[i][j] = GenerateFaceObject(map.facesLump.faces[map.markSurfacesLump.markSurfaces[map.leafLump.leafs[i].lface_index + j]]);
                faceCount++;
            }
        }
    }

    #region Face Object Generation

    GameObject GenerateFaceObject(BSPFace face)
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
        faceObject.isStatic = true;
        //faceObject.renderer.enabled = false;

        return faceObject;
    }
    #endregion
}