﻿using UnityEngine;
using System.Collections;

public class DudeMove : MonoBehaviour {

	public float speed = 0.025f;
	private float move = 0f;

	// Use this for initialization
	void Start () {
	
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

		float inputMove = Input.GetAxis ("Horizontal");
		move = (inputMove * speed);

	}
}
