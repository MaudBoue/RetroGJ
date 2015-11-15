using UnityEngine;
using System.Collections;

public class SoundManagerS : MonoBehaviour {

	public static SoundManagerS instance = null;
	private AudioSource AudioS;

	public AudioClip musiquePlanete;
	public AudioClip musiqueCombat;

	public bool fight = false;
	// Use this for initialization

	void Awake () {
		if (instance == null){
			instance = this;
		}else if (instance != this)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);
	}


	void Start () {
		AudioS = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey(KeyCode.Z)) {
			fight=true;
			AudioS.clip = musiqueCombat;
			AudioS.Play ();
		}
		if (Input.GetKey(KeyCode.A)) {
			fight=false;
			AudioS.clip = musiquePlanete;
			AudioS.Play ();
		}

	}
}
