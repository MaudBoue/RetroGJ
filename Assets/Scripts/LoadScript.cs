﻿using UnityEngine;
using System.Collections;

public class LoadScript : MonoBehaviour {

	public static int zoom = 4;

	// Use this for initialization
	void Awake () {
		Screen.SetResolution (zoom * 128,zoom * 128, false);
		Globals.GenerateGalaxie ();
	}

	void Start(){
		Application.LoadLevel ("BPlaneteBleue");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
