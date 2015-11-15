using UnityEngine;
using System.Collections;

public class DudeMove : MonoBehaviour {

	public float speed = 0.025f;
	public float JumpForce = 3;

	private float move = 0f;
	public Animator animator;

	private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
	rigid = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (move != 0f) {
			Vector3 current = this.transform.position;
			current.x += move;
			this.transform.position = current;
		}
	}

	void FixedUpdate () {

		if (Globals.isFight ()) {
			float inputMove = Input.GetAxis ("Horizontal");
			move = (inputMove * speed);

			if (inputMove >= 0) {
				animator.SetBool ("Droite", true);
			}
			if (inputMove < 0) {
				animator.SetBool ("Droite", false);
			}

			if (Input.GetKeyDown (KeyCode.Space)) { //&& touche == true : recharge quand au sol?
				rigid.AddForce (new Vector2 (0, JumpForce), ForceMode2D.Impulse);
			}
		}
	}

}
