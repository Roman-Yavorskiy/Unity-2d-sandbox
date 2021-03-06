﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateChunks : MonoBehaviour {

	public GameObject chunk;
	int chunkWidth;
	public int numChunks;
	float seed;

	void Start () {
		chunkWidth = chunk.GetComponent<GenerateChunk> ().width;
		seed = Random.Range (-10000f, 10000f);
		Generate ();

	}


	public void Generate ()	{
		int lastX = -chunkWidth;
		for (int i = 1; i < numChunks; i++) {
			GameObject newChunk = Instantiate (chunk, new Vector2 (lastX + chunkWidth, 0f), Quaternion.identity) as GameObject;
			newChunk.GetComponent<GenerateChunk> ().seed = seed;
			lastX += chunkWidth;
			}
		}
	}


