using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Inventory : MonoBehaviour {

	public Text inventoryText, selectionText;

	int[] counts = new int[7];
	int selectedTile;
	public void Add (int tileType, int count) {
		counts [tileType] += count;
	}

	string[] names = new string[] {"Травка", "Земелька" , "Камушки" , "Уголек" , "Железяки" , "Золото" , "Брильянты" };

		public GameObject[] tiles = new GameObject[7];
	// Update is called once per frame
	void Update () {
		inventoryText.text = "Трава: " + counts [0] + "\nЗемля: " + counts [1] + "\nКамень: " + counts [2] + "\nУголь: " + counts [3] + "\nЖелезо: " + counts [4] + "\nЗолото: " + counts [5] + "\nБрилики: " + counts [6];
	
	
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			selectedTile++;
			if (selectedTile < 0) {
				selectedTile = counts.Length - 1;
			}
			if (selectedTile > counts.Length -1) {
				selectedTile = 0;
			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			selectedTile--;
			if (selectedTile < 0) {
			selectedTile = counts.Length - 1;
				}
				if (selectedTile > counts.Length -1) {
					selectedTile = 0;
				}
		}

		selectionText.text = "Материал анлим: " + names [selectedTile];



		if (Input.GetMouseButtonDown (1)) {
			Vector3 mousepos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector3 placepos = new Vector3 (Mathf.Round(mousepos.x), Mathf.Round(mousepos.y), 0f);

			if (Physics2D.OverlapCircleAll (placepos, 0.25f).Length == 0) {
			GameObject newTile = Instantiate (tiles [selectedTile], placepos, Quaternion.identity) as GameObject;
	
			}
				}
	 
	}
}
