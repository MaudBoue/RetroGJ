using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	public int life = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void takeAttack( int count ) {
		this.life -= count;
		if (this.life <= 0) {
			Destroy(this.gameObject);
		}
	}
}
