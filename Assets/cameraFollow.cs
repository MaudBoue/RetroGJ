using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	public Transform target;
	public float DampTime = 0.2f;
	private Vector3 velocity = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (target) {
			
			Vector3 destination = new Vector3 (target.position.x, target.position.y, -10);
			transform.position = Vector3.SmoothDamp (transform.position, destination, ref velocity, DampTime);
		}
	}
}
