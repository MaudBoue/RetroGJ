using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Ennemy parent;
	private float damage;
	private DudeLifeDeath player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<DudeLifeDeath>();
		damage = parent.Damage;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player")
			player.LoseLife (damage);
		
	}

}
