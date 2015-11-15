using UnityEngine;
using System.Collections;

public class DudeLifeDeath : MonoBehaviour {

	public float MaxLife = 100;
	public float Life;
	// Use this for initialization
	void Start () {
		if (MaxLife!=null) {
			Life = MaxLife;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoseLife(float damage){
		Life -= damage;
		Debug.Log(Life);
		if (Life <= 0) {
			Dead();
		}
	}

	private void Dead(){
		//playSound death
		Application.LoadLevel("GameOver");
	}

}
