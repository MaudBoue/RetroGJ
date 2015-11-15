using UnityEngine;
using System.Collections;

public class IABlob : MonoBehaviour {

	public float distanceDetection;
	public float timeBetween2Attacks;

	private float NextAttaque;
	private GameObject player;
	private Animator anim;

	private Vector2 RealPosition;


	private GameManager gameManagerScript;

	// Use this for initialization
	void Start () {
		gameManagerScript = GameObject.Find("gameManager").GetComponent<GameManager>();

		player = GameObject.FindGameObjectWithTag ("Player");
		anim = this.GetComponent<Animator> ();
		RealPosition = new Vector2 (transform.position.x,transform.position.y+ 1.5f);
		NextAttaque = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	

		anim.SetBool ("Attaque", false);

		if (Vector2.Distance (RealPosition, player.transform.position) < distanceDetection && Time.time >= NextAttaque) {
			anim.SetBool("Attaque",true);
			NextAttaque=Time.time+timeBetween2Attacks;
			//Debug.Log ("sfdsfe");
		}


		anim.SetBool("gmFight",gameManagerScript.fight );

	}

	public void OnDrawGizmos ()
	{	Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere (RealPosition, distanceDetection);
	}

}
