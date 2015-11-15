using UnityEngine;
using System.Collections;

public class WeakPoint : MonoBehaviour {
	private float damage;

	public Ennemy parent;
	//private DudeLifeDeath player; // là où est la force du joueur

	// Use this for initialization
	void Start () {
		damage = parent.Damage; //player.damage
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player")
			parent.LoseLife (damage);
		
	}

}
