using UnityEngine;
using System.Collections;

public class BastonCotroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyUp(KeyCode.Space) ){
			Application.LoadLevel("Galaxie");
		}
	}
}
