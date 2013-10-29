using System;
using UnityEngine;

	public class BSPNodeLump
	{
		public BSPNode[] nodes;
	
		public BSPNodeLump ()
		{
		}

        public void PrintInfo(){
            foreach (BSPNode node in nodes)
            {
                Debug.Log(node.ToString());
            }
        }
	}

