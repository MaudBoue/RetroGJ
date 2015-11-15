using UnityEngine;
using System.Collections;

public class UIGameOver : MonoBehaviour {

	private SoundManagerS SoundM;
	private GameManager gameManagerScript;
	// Use this for initialization
	void Start () {
		SoundM = GameObject.Find ("SoundManager").GetComponent<SoundManagerS> ();
		gameManagerScript = GameObject.Find("gameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewGame(){
		SoundM.ChangeMusique (SoundM.musiquePlanete);
		SoundM.OnPlanete = true;
		gameManagerScript.fight = false;
		Application.LoadLevel ("Load");
	}

	public void quitter(){
		Application.Quit();
	}


}
