using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chunk : MonoBehaviour {
	public GameObject Block;
	Vector2 overvalue;
	// Use this for initialization
	void Start ()
	{
		overvalue = new Vector2 (Random.value, Random.value);
		for (int x = 0; x < World.currentWorld.chunkWith; x++) {
			for (int y = 0; y < World.currentWorld.chunkHeight; y++) {
				float noiseX = x + overvalue.x;
				float noiseY = y + overvalue.y;
				float noise = Mathf.PerlinNoise (noiseX, noiseY);
				if (noise > 0.4) {
					//for (int x = 0; x < World.currentWorld.chunkWith; x++) {
					//	float noiseX = ((Random)x / 20) + 10;	
					//	for (int y = 0; y < World.currentWorld.chunkHeight; y++) {
					//		float noiseY = ((Random)y / 20) + 10;
					//		float noiseValue = Noise.Generate (noiseX, noiseY);
					//		if (noiseValue < 0.1)
					Instantiate (Block, new Vector2 (x, y), Quaternion.identity);

				}
			}
		}
	}
}