﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateChunk : MonoBehaviour {

	public GameObject DirtTile;
	public GameObject GrasTile;
	public GameObject StoneTile;

	public int width;
	public float heightMultiplayer;
	public int heihgtAddition;
	public float smoothness;

	[HideInInspector]
	public float seed;

	public GameObject tileCoal;
	public GameObject tileDiamond;
	public GameObject tileGold;
	public GameObject tileIron;

	public float chanceCoal;
	public float chanceDiamond;
	public float chanceGold;
	public float chanceIron;



	void Start () {
		Generate ();

	}

	//Generate main Blocks from stone dirt and grass
	public void Generate ()
	{
		
		for (int i = 1; i <= width; i++) {
			int h = Mathf.RoundToInt ((Mathf.PerlinNoise (0f, i/smoothness)*heightMultiplayer)+heihgtAddition);

			//Debug.Log ("h"+h);

			for (int j = 0; j < h; j++) {
				GameObject selectedTile;
				if (j < h - 20) {		// level of stones 
					selectedTile = StoneTile;
				} else if (j < h - 2) {  //thokness of grass
					selectedTile = DirtTile; 
				} else {
					selectedTile = GrasTile;
				}

				GameObject newTile = Instantiate (selectedTile, Vector2.zero, Quaternion.identity) as GameObject;
				newTile.transform.parent = this.gameObject.transform;
				newTile.transform.localPosition = new Vector2 (i, j);





			}
		}
		Populate ();
	}
	// filling resources 

	public void Populate () {
		foreach (GameObject t in GameObject.FindGameObjectsWithTag("TileStone")) {
			if (t.transform.parent == this.gameObject.transform) {
				float r = Random.Range (0f, 100f);
				GameObject selectedTile = null;

				if (r < chanceDiamond) {
					selectedTile = tileDiamond;
				} else if (r < chanceGold) {
					selectedTile = tileGold;
				} else if (r < chanceIron) {
					selectedTile = tileIron;
				} else if (r < chanceCoal) {
					selectedTile = tileCoal;
				}
				if (selectedTile != null) {
					GameObject newResourceTile = Instantiate (selectedTile, t.transform.position, Quaternion.identity) as GameObject;
					Destroy (t);
				}
			}
		}
}

}