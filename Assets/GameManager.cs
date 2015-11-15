using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public bool fight = false;
	public static GameManager instance = null;

	// Use this for initialization
	void Awake () {
		if (instance == null){
			instance = this;
		}else if (instance != this)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);
	}


	void Start () {
		fight = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
