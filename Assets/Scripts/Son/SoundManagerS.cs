using UnityEngine;
using System.Collections;

public class SoundManagerS : MonoBehaviour {

	public static SoundManagerS instance = null;

	private GameManager gameManagerScript;

	private AudioSource AudioS;

	public AudioClip musiquePlanete;
	public AudioClip musiqueCombat;
	public AudioClip musiqueDeath;

	public bool fight = false;
	public bool OnPlanete = true;
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
		gameManagerScript = GameObject.Find("gameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (OnPlanete) {
			if (gameManagerScript.fight && AudioS.clip != musiqueCombat) {
				ChangeMusique (musiqueCombat);
			}
			if (gameManagerScript.fight == false && AudioS.clip != musiquePlanete) {
				ChangeMusique (musiquePlanete);
			}

			if (Input.GetKey(KeyCode.Z)) {
				gameManagerScript.fight=true;
				
			}
			if (Input.GetKey(KeyCode.A)) {
				gameManagerScript.fight=false;
			}

		}


	}

	public void ChangeMusique(AudioClip Clip){
		AudioS.clip = Clip;
		AudioS.Play ();
	}
}
