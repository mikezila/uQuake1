using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;


public class BSPHeader
{
    public class HeaderEntry
    {
        public int offset;
        public int length;

        public HeaderEntry(int offset, int length)
        {
            this.offset = offset;
            this.length = length;
        }

        public override string ToString()
        {
            return "Offset: " + offset + " Length: " + length + "\r\n";
        }
    }

    public List<HeaderEntry> directory = new List<HeaderEntry>();
    public uint version;

    public BSPHeader(BinaryReader map)
    {
        map.BaseStream.Seek(0, SeekOrigin.Begin);
        version = map.ReadUInt32();
        Debug.Log("BSP Version: "+version.ToString());

        for (int i = 0; i < 15; i++)
        {
            directory.Add(new HeaderEntry(map.ReadInt32(), map.ReadInt32()));
        }
    }

    public void PrintInfo()
    {
        for (int i = 0; i < 15; i++)
        {
            Debug.Log("Lump " + i.ToString() + " " + directory[i].ToString());
        }
    }
}

