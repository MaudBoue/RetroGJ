using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GalaxieCreate : MonoBehaviour {

	//public int planetsCount = 3;
	public GameObject planet;
	public GameObject textObj;

	private int selected = 0;
	private GameObject lastSelected;
	private string[] labels;

	public AudioClip selectAudio;

	void Awake(){

		labels = new string[Globals.planetsCount+1];
		labels [0] = "Planete Xanthia";
		labels [1] = "Planete Jaggon";
		labels [2] = "Planete Prulule";
		labels [3] = "Planete XOR 154";
		labels [4] = "Planete Zoubzoub";
		labels [5] = "Planete Krelarf";

		/*
		// Generation de la galaxie
		if (Globals.planets == null) {
			
			Bounds cameraBounds = PixelPerfectCamera.OrthographicBounds (Camera.main);
			float xMax = cameraBounds.extents.x - cameraBounds.extents.x * 0.1f;
			Globals.planets = new PlanetInfo[planetsCount];
			
			for (int i = 0; i < this.planetsCount; i++) {
				
				float x = Random.Range (- xMax, xMax);
				float y = Random.Range (- xMax, xMax);
				if( y < 0 && y < -4.5f) y = -4.5f;
				
				if( !CheckPosition(x,y) ){
					i--;
					continue;
				}
				
				int size = Random.Range (1, 3);
				float scale = planet.transform.localScale.x / (size * 2);
				
				PlanetInfo p = new PlanetInfo ();
				p.x = x;
				p.y = y;
				p.scale = scale;
				
				Globals.planets [i] = p;
			}

			AttachTribu();
			System.Array.Sort(Globals.planets, SortPlanets);
		
			PlanetInfo p1 = Globals.planets [0];
			p1.tribuId = 1;

			Globals.currentPlanet = p1;
			Globals.currentPlanetId = 0;
		}
		*/
	}

	/*int SortPlanets(PlanetInfo a, PlanetInfo b){
		if (a.y > b.y) {
			return -1;
		} else if (a.y < b.y) {
			return 1;
		}
		return 0;
	}*/

	// Use this for initialization
	void Start () {

		int i;

		// Placement des planétes
		for (i = 0; i < Globals.planetsCount; i++) {
			PlanetInfo p = Globals.planets[i];

			GameObject planetObject = Instantiate( planet );
			planetObject.name = "p" + i;
			planetObject.transform.position = new Vector3(p.x, p.y, 0);

			float scale = planetObject.transform.localScale.x;
			planetObject.transform.localScale = new Vector3(scale / 2, scale / 2, 0);
		}
	
		selected = Globals.currentPlanetId;
		lastSelected = null;
		OnChangeSelected (selected);
	}

	/*bool CheckPosition(float x, float y){

		if (Globals.planets.Length == 0)
			return true;

		for (int i = 0; i < Globals.planets.Length - 1 ; i++) {
			PlanetInfo p = Globals.planets[i];

			if( p!= null ){
				float dist = Vector2.Distance( new Vector2(x,y), new Vector2(p.x, p.y) );
				if( dist < 0.1f ){
					return false;
				}
			}
		}
		return true;
	}

	void AttachTribu(){
		int p1Index = 0;
		int p2Index = 1;

		PlanetInfo p2 = Globals.planets [p2Index];
		p2.tribuId = 2;
	}*/

	void OnChangeSelected(int index){

		if (lastSelected) {
			//lastSelected.GetComponent<Animator>().SetBool("Highlight", false);
		}

		GameObject planetObject = GameObject.Find ("p" + index);
		//planetObject.GetComponent<Animator> ().SetBool ("Highlight", true);

		lastSelected = planetObject;
		selected = index;
		Globals.currentPlanetId = index;

		textObj.GetComponent<Text> ().text = labels [selected];
	}

	void PlayAudioSelect(){

	}
	
	// Update is called once per frame
	void Update () {

		if( Input.GetKeyUp(KeyCode.RightArrow) ){
			Globals.currentPlanetId = selected;
			Globals.currentPlanet = Globals.planets[selected];

			Application.LoadLevel(Globals.currentPlanet.scene);
		}

		if( Input.GetKeyUp(KeyCode.UpArrow) ){
			if( selected - 1 >= 0 ){
				OnChangeSelected(selected - 1);
			}else{
				OnChangeSelected( Globals.planets.Length - 1 );
			}
			PlayAudioSelect ();
		}

		if( Input.GetKeyUp(KeyCode.DownArrow) ){
			if( selected + 1 < Globals.planets.Length ){
				OnChangeSelected(selected + 1);
			}else{
				OnChangeSelected(0);
			}
			PlayAudioSelect ();
		}
	}
}
