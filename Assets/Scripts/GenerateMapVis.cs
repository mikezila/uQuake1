using UnityEngine;
using System.Collections;

public class GenerateMapVis : MonoBehaviour
{
    public string mapName;
    private BSP29map map;
    private int faceCount = 0;
    private GameObject[][] leafRoots;
    public Bounds[] leafBoxes;
    private bool mapReady = false;
    public Plane[] planes;

    void Start()
    {
        map = new BSP29map(mapName);
        GenerateVisArrays();
        GenerateVisObjects();
        GenerateLeafBoxes();
        //GeneratePlanes();
        mapReady = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Bounds:\r\n");
            foreach (Bounds leaf in leafBoxes)
            {
                Debug.Log(leaf.extents.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            int count = 0;
            foreach (Bounds leaf in leafBoxes)
            {
                //Debug.Log(leaf.center.ToString());
                if (leaf.Intersects(GameObject.FindWithTag("Player").collider.bounds))
                    count++;
            }
            Debug.Log("Touching " + count.ToString() + " of " + leafBoxes.Length + " leafs at " + GameObject.FindWithTag("Player").transform.position.ToString());
        }
    }

    void OnDrawGizmosSelected()
    {
        if (mapReady)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < leafBoxes.Length; i++)
            {
                Gizmos.DrawWireCube(leafBoxes[i].center, leafBoxes[i].size);
            }
        }
    }

    void GeneratePlanes()
    {
        planes = new Plane[map.planeLump.planes.Length];
        for (int i = 0; i < planes.Length; i++)
        {
            planes[i] = new Plane(map.planeLump.planes[i].normal, map.planeLump.planes[i].distance);
        }
    }

    void GenerateVisArrays()
    {
        leafRoots = new GameObject[map.leafLump.leafCount][];
        leafBoxes = new Bounds[map.leafLump.leafCount];
        for (int i = 0; i < map.leafLump.leafCount; i++)
        {
            leafRoots[i] = new GameObject[map.leafLump.leafs[i].num_lfaces];
            leafBoxes[i] = new Bounds();
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

    void GenerateLeafBoxes()
    {
        for (int i = 0; i < map.leafLump.leafCount; i++)
        {
            leafBoxes[i].SetMinMax(map.leafLump.leafs[i].mins, map.leafLump.leafs[i].maxs);
            Debug.Log(leafBoxes[i].center.ToString() + " " + leafBoxes[i].size.ToString());
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

        return faceObject;
    }
    #endregion
}