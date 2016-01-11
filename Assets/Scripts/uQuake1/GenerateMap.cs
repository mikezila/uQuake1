using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;

public class GenerateMap : MonoBehaviour
{
    public string mapName;
    private BSP29map map;

    void Start()
    {

    }

    void Update()
    {

    }

    void PopulateLevel()
    {
        map = new BSP29map(mapName);
        GenerateMapObjects();
    }

    void GenerateMapObjects()
    {
        foreach (BSPFace face in map.facesLump.faces)
            GenerateFaceObject(face);
    }


    #region Editor Widgets

#if UNITY_EDITOR
    [CustomEditor(typeof(GenerateMap))]
    class GenerateMapInEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GenerateMap script = (GenerateMap)target;
            if (GUILayout.Button("Generate"))
                script.PopulateLevel();

            if (GUILayout.Button("Clear"))
            {
                var children = new List<GameObject>();
                foreach (Transform child in script.gameObject.transform) children.Add(child.gameObject);
                children.ForEach(child => DestroyImmediate(child));
            }
        }
    } 
#endif

    #endregion


    #region Face Object Generation

    GameObject GenerateFaceObject(BSPFace face)
    {
        GameObject faceObject = new GameObject("BSPface");
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

        // We make a material and then use shared material to work around a leak in the editor
        Material mat = new Material(Shader.Find("Diffuse"));
        mat.mainTexture = map.miptexLump.textures[map.texinfoLump.texinfo[face.texinfo_id].miptex];
        faceObject.GetComponent<Renderer>().sharedMaterial = mat;

        // Turn off the renderer if the face is part of a trigger brush
        string texName = map.miptexLump.textures[map.texinfoLump.texinfo[face.texinfo_id].miptex].name;
        if (texName == "trigger")
        {
            faceObject.GetComponent<Renderer>().enabled = false;
        }

        faceObject.AddComponent<MeshCollider>();
        faceObject.isStatic = true;

        return faceObject;
    }
#endregion
}