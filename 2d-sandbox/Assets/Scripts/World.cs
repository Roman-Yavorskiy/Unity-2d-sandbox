using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	public float chunkWith = 50;
	public float chunkHeight = 50;
	public static World currentWorld;

	public void Awake(){
		currentWorld = this;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
