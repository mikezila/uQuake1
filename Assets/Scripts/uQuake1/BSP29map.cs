using UnityEngine;
using System.Collections;
using System.IO;

public class BSP29map
{
    private BinaryReader BSPfile;
    public BSPHeader header;

    public BSPEntityLump entities;
    public BSPFaceLump faces;
    public BSPEdgeLump edges;
    public BSPVertexLump verts;

    public BSP29map(string filename)
    {
        BSPfile = new BinaryReader(File.Open("Assets/Resources/Maps/" + filename, FileMode.Open));
        header = new BSPHeader(BSPfile);

        ReadEntities();
        ReadFaces();
        ReadEdges();
        ReadVerts();

        //faces.PrintInfo();
        //edges.PrintInfo();
        //verts.PrintInfo();
    }

    private void ReadVerts()
    {
        verts = new BSPVertexLump();
        BSPfile.BaseStream.Seek(header.directory[3].offset, SeekOrigin.Begin);
        int numVerts = header.directory[3].length / 12;
        for (int i = 0; i < numVerts; i++)
        {
            verts.AddVert(new Vector3(BSPfile.ReadSingle(), BSPfile.ReadSingle(), BSPfile.ReadSingle()));
        }
    }

    private void ReadEntities()
    {
        BSPfile.BaseStream.Seek(header.directory[0].offset, SeekOrigin.Begin);
        entities = new BSPEntityLump(BSPfile.ReadChars(header.directory[0].length));
    }

    private void ReadEdges()
    {
        edges = new BSPEdgeLump();
        BSPfile.BaseStream.Seek(header.directory[12].offset, SeekOrigin.Begin);
        int numEdges = header.directory[12].length / 4;
        for (int i = 0; i < numEdges; i++)
        {
            edges.edges.Add(new BSPEdge(BSPfile.ReadUInt16(), BSPfile.ReadUInt16()));
        }

        BSPfile.BaseStream.Seek(header.directory[13].offset, SeekOrigin.Begin);
        int numLedges = header.directory[13].length / 2;
        for (int i = 0; i < numLedges; i++)
        {
            edges.ledges.Add(BSPfile.ReadInt16());
        }
    }

    private void ReadFaces()
    {
        faces = new BSPFaceLump();
        BSPfile.BaseStream.Seek(header.directory[7].offset, SeekOrigin.Begin);
        int numFaces = header.directory[7].length / 28;

        // I do seeking inside the loop because I only want to rip the data I care about,
        // and the seek skips over things I won't need, like plane information.
        for (int i = 0; i < numFaces; i++)
        {
            BSPfile.BaseStream.Seek(4, SeekOrigin.Current);
            faces.faces.Add(new BSPFace(BSPfile.ReadInt32(), BSPfile.ReadInt16(), BSPfile.ReadInt16()));
            BSPfile.BaseStream.Seek(8, SeekOrigin.Current);
        }
    }

}
