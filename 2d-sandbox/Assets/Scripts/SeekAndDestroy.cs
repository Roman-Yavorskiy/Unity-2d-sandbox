using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class SeekAndDestroy : MonoBehaviour {

	public Transform player;
	public Transform bot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player);
		transform.Translate (Vector2.right * Time.deltaTime);
		
	}
}
