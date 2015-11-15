using UnityEngine;
using System.Collections;

public class DudeAttack : MonoBehaviour {

	public float timeBetween2Attacks;
	
	private float NextAttaque;
	private GameObject player;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		NextAttaque = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown(KeyCode.C)) {
			anim.SetBool("Attaque",true);
			NextAttaque=Time.time+timeBetween2Attacks;
			Debug.Log ("sfdsfe");
			StartCoroutine(attackFalseLaser ());
		}
	}

	IEnumerator attackFalseLaser(){
		yield return new WaitForSeconds(1);
		anim.SetBool ("Attaque", false);
	}
}
