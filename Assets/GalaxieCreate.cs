using UnityEngine;
using System.Collections;

public class GalaxieCreate : MonoBehaviour {

	public int planetsCount = 3;
	public GameObject planet;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < this.planetsCount; i++) {
			GameObject p = Instantiate( planet );
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
