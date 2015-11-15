using UnityEngine;
using System.Collections;

public class UIGameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewGame(){
		Application.LoadLevel ("BPlaneteBleue");
	}

	public void quitter(){
		Application.Quit();
	}


}
