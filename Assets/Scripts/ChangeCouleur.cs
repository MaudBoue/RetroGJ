using UnityEngine;
using System.Collections;

public class ChangeCouleur : MonoBehaviour {

	private SpriteRenderer rend;
	public string color;

	public SpriteRenderer Parent;

	// Use this for initialization
	void Start () {
		rend = this.GetComponent<SpriteRenderer> ();
		if (Parent) {
			rend.color = Parent.color;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
