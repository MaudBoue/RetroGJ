using UnityEngine;
using System.Collections;

public class PixelPerfectSprite : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float x = Mathf.Round ( this.transform.position.x * 100) / 100;
		float y = Mathf.Round ( this.transform.position.y * 100) / 100;

		Vector3 current = this.transform.position;
		current.x = x;
		current.y = y;

		this.transform.position = current;
	}
}
