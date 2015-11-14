using UnityEngine;
using System.Collections;

public class DudeAttack : MonoBehaviour {

	public GameObject poing;
	public float attackDelay = 0.25f;
	private bool attacking = false;
	private float lastAttack;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetButton ("Fire1")) {
			attacking = true;
		} else {
			attacking = false;
		}

		if (attacking && Time.time >= lastAttack + attackDelay) {
			if (poing)
				poing.SetActive (true);

			lastAttack = Time.time;
		} else {
			if( poing )
				poing.SetActive (false );
		}
	}
}
