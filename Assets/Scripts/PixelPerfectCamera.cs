﻿using UnityEngine;
using System.Collections;

public class PixelPerfectCamera : MonoBehaviour {

	public float textureSize = 100f;
	float unitsPerPixel;

	// Use this for initialization
	void Start () {
		Screen.SetResolution (256, 256, false);
		unitsPerPixel = 1f / textureSize;
		Camera.main.orthographicSize = (Screen.height / 2f) * unitsPerPixel;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
