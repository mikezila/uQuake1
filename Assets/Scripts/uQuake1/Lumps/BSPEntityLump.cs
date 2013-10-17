using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class BSPEntityLump
{
    public string rawEntities;

    public BSPEntityLump(char[] ents)
    {
        this.rawEntities = new string(ents);
    }
}

