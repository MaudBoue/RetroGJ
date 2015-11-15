using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	public Transform target;
	public float DampTime = 0.2f;
	private Vector3 velocity = Vector3.zero;

	public GameObject BoundDroite;
	public GameObject BoundGauche;

	private float CameraSize = 6.4f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log (BoundGauche.transform.position.x + CameraSize);

		if (target) {
			if (target.position.x > (BoundGauche.transform.position.x+CameraSize) && target.position.x < (BoundDroite.transform.position.x-CameraSize) )
			{
			Vector3 destination = new Vector3 (target.position.x, 0, -10);
			transform.position = Vector3.SmoothDamp (transform.position, destination, ref velocity, DampTime);
			}
		}
	}
}
