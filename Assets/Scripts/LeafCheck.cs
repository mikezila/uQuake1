using UnityEngine;
using System.Collections;

public class LeafCheck : MonoBehaviour {
	
	private GenerateMapVis map;
	
	// Use this for initialization
	void Start () {
		map = GameObject.FindWithTag("worldspawn").GetComponent("GenerateMapVis") as GenerateMapVis;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){
			int count = 0;
			foreach (Bounds leaf in map.leafBoxes){
				if (leaf.Contains(gameObject.transform.position))
					count++;
			}
			Debug.Log("Touching "+count.ToString()+" leafs.");
		}
	}
}
