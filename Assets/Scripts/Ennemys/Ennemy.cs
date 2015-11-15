using UnityEngine;
using System.Collections;

public class Ennemy : MonoBehaviour {

	public float MaxLife;
	private float Life;
	public float Damage;

	private GameManager gameManagerScript;

	// Use this for initialization
	void Start () {
		Life = MaxLife;
	}
	
	// Update is called once per frame
	void Update () {
	gameManagerScript = GameObject.Find("gameManager").GetComponent<GameManager>();
	}

	public void LoseLife (float damage){
		Life -= damage;
		Debug.Log(Life);
		if (Life <= 0) {
			Dead();
		}
	}
	
	private void Dead(){
		//playSound death
		gameManagerScript.fight = false;
		Destroy (this.gameObject);
	}

}
