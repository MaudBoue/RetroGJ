using UnityEngine;
using System.Collections;

public class PoingAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Enemy" ) {
			//Destroy(collider.gameObject);
			Entity entity = collider.gameObject.GetComponent<Entity>();
			if( entity )
				entity.takeAttack(10);
		}
	}

}
