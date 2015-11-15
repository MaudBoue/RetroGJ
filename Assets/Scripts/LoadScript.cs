using UnityEngine;
using System.Collections;

public class LoadScript : MonoBehaviour {

	public static int zoom = 4;

	// Use this for initialization
	void Start () {
		Screen.SetResolution (zoom * 128,zoom * 128, false);
		Application.LoadLevel ("Baston");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
