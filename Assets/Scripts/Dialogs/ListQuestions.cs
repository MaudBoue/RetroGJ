using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON;
using System.Text.RegularExpressions;
using System.Reflection;

public class ListQuestions : MonoBehaviour {

	public Color normalColor = new Color(0.6f,0.6f,0.6f);
	public Color selectedColor = new Color(1f,1f,1f);

	public GameObject reponseObject;
	private bool inReponse = false;

	private int selected = 0;
	private int realSelected = 0;
	private int skip = 0;
	private GameObject lastSelection;

	private int maxItems = 4;
	private string[] currentList;
	private JSONNode[] currentListValues;

	private string currentCol;

	private string currentAction = "actions";
	private JSONNode actions;
	private JSONNode dialogs;

	private int countQuestion = 0;

	//private ArrayList allObjets;
	private ArrayList objets;
	private string currentInv;

	private Dictionary<string, Objet> allObjets = new Dictionary<string, Objet>();

	void Awake(){
		actions = Globals.getJSON ("actions.json");

		allObjets.Add ("Fruit bizarre", new Objet("eat", 25));
		allObjets.Add ("Capsule de soin", new Objet("eat", 75));

		allObjets.Add ("Bidon de kerozene", new Objet("drink", 50));
		allObjets.Add ("Bave de limace", new Objet("drink", 25));
		allObjets.Add ("Flasque de bave", new Objet("drink", 50));
	
		allObjets.Add ("Bidon de kerozene vide", new Objet("useless", 0));
		allObjets.Add ("Idole galactique", new Objet("useless", 0));
		allObjets.Add ("Bibelot informe", new Objet("useless", 0));
		allObjets.Add ("Crane usage", new Objet("useless", 0));
		allObjets.Add ("Globe occulaire", new Objet("useless", 0));

		foreach(KeyValuePair<string, Objet> item in allObjets){
			item.Value.name = item.Key;
		}
	}

	// Use this for initialization
	void Start () {

		objets = new ArrayList ();

		Debug.Log (Globals.currentPlanetId);

		if (Globals.currentPlanet.tribuId > 0) {
			dialogs = Globals.getJSON ("tribu" + Globals.currentPlanet.tribuId + ".json");
		} else {
			// Random de rencontre
			if( Random.Range(1,5) == 1 ){
				// Monters @todo
			}

			//if( Random.Range (1,3) == 1 ){
				int randObjCount = Random.Range(1, 3);
				for(int i = 0; i < randObjCount; i++){
					// Objets dans la scene
					int objIndex = Random.Range(0, allObjets.Count);
					//objets.Add( allObjets[objIndex] );
					
					int iobj = 0;
					foreach(KeyValuePair<string, Objet> item in allObjets){
						if( iobj == objIndex ){
							objets.Add( item.Value.name );
						}
						iobj++;
					}
				}
			//}
		}

		Parse (actions.AsObject);
		UpdateList ();
		OnChange (0);
	}

	// Update is called once per frame
	void Update () {
		if( Input.GetKeyUp(KeyCode.DownArrow) ){
			OnChange (1);
		}
		if( Input.GetKeyUp(KeyCode.UpArrow) ){
			OnChange (-1);
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			OnSelectItem ();
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			OnBack ();
		}
	}

	void Parse(JSONClass list){
		currentList = new string[list.Count];
		currentListValues = new JSONNode[list.Count];

		int i = 0;
		foreach ( KeyValuePair<string, JSONNode> item in list) {

			if( (item.Key == "Dire" || item.Key == "Donner") && Globals.currentPlanet.tribuId == 0){
				continue;
			}

			if( objets.Count == 0 ){
				if( item.Key == "Prendre" ){
					continue;
				}

				if( Globals.currentPlanet.tribuId == null && item.Key == "Manipuler" ){
					continue;
				}
			}

			currentList[i] = item.Key;
			currentListValues[i] = item.Value;
			i++;
		}

		selected = realSelected = skip = 0;
	}

	public void ChangeSelection(){
		GameObject text = GameObject.Find ("Text" + realSelected);

		if (lastSelection != null) {
			Text t = lastSelection.GetComponent<Text>();
			t.color = normalColor;
		}

		if (text != null) {
			Text t = text.GetComponent<Text>();
			t.color = selectedColor;
			lastSelection = text;
		}
	}

	void UpdateList(){

		int i;
		for (i = 0; i < currentList.Length; i++) {
			GameObject textObject = GameObject.Find ("Text" + i );
			if( textObject != null ){
				Text text = textObject.GetComponent<Text>();

				string value = currentList[skip + i];
				if( value != null ){
					text.text = value;
				}else{
					text.text = "";
				}

				text.color = normalColor;
			}
		}

		if( i < maxItems ){
			for(; i < maxItems; i++){
				GameObject textObject = GameObject.Find ("Text" + i );
				if( textObject != null ){
					Text text = textObject.GetComponent<Text>();
					text.text = "";

					if( i == 0 ){
						text.text = " - Rien - ";
					}

					text.color = normalColor;
				}
			}
		}
	}

	void OnChange(int change){

		if (selected + change < 0) {
			change = 0;
		} else if (selected + change >= currentList.Length) {
			change = 0;
		}

		if (change != 0) {
			if (realSelected + change >= maxItems) {
				skip++;
			}else if (realSelected + change < 0) {
				skip--;
			}else{
				realSelected += change;
			}
			selected += change;
			UpdateList();
		}

		ChangeSelection ();
	}

	void OnSelectItem(){

		JSONNode item = currentListValues [selected];
		JSONClass obj = null;

		if(item != null) obj = item.AsObject;

		bool noReset = false;

		if (obj == null) {
			string str = item;

			// Do
			if( str != null && str.Contains("do:") ){
				str = str.Replace("do:", "");
				str = str.Replace("\"", "");
				
				MethodInfo method = this.GetType().GetMethod (str);
				if( method != null ){
					method.Invoke(this, null);
				}
			
			// Action
			}else if( str != null && str.Contains("action:")){
				str = str.Replace("action:", "");
				str = str.Replace("\"", "");

				currentAction = str;
				Parse (dialogs["questions"].AsObject);
				UpdateList ();
				OnChange (0);

			// Inventaire
			}else{

				switch(currentInv){

					// Prendre un objet au sol
					case "take":

					Globals.items.Add( currentList[selected] );

					for(int i = 0; i < objets.Count; i++){
						if( objets[i] == currentList[selected] ){
							objets.RemoveAt(i);
							break;
						}
					}
					
					take ();

					break;

					// Boire
					case "drink":
					case "eat":

					for(int i = 0; i < Globals.items.Count; i++){
						string itemName = (string)Globals.items[i];
						Objet def = allObjets[itemName];

						if( def.name == currentList[selected] ){
							Globals.playerHealth += def.score;
							Globals.items.RemoveAt(i);
							break;
						}
					}

					MethodInfo method = this.GetType().GetMethod( currentInv );
					method.Invoke(this, null);

					break;

					// Donner
					case "give":



					break;
				}

			}

		} else {

			if( currentAction != "dialogs" ){
				// Update de la liste avec les éléments enfants
				Parse (obj);
				UpdateList ();
				OnChange (0);

			}else{

				// Affiche la réponse
				if( !inReponse ){

					countQuestion++;

					JSONArray reponses = currentListValues[selected]["reponses"].AsArray;
					JSONArray aggrs = currentListValues[selected]["aggr"].AsArray;
					int att = currentListValues[selected]["attitude"].AsInt;

					ArrayList reponsesFinal = new ArrayList();

					if( true /*Globals.currentPlanet.tribuAttitude < 0*/ ){
						int count = 0;
						for(int i = 0; i < aggrs.Count; i++){
							int aggrId = aggrs[i].AsInt;

							JSONArray reps = dialogs["aggr"][aggrId-1].AsArray;

							for(int j = 0; j < reps.Count; j++){
								reponsesFinal.Add(reps[j] + "");
								count++;
							}
						}
					}else{
						for(int i = 0; i < reponses.Count; i++){
							reponsesFinal.Add(reponses[i] + "");
						}
					}

					// Atitude négative
					if( att != 0){

						if( Globals.currentPlanet != null && Globals.currentPlanet.tribuAttitude == 0){
							//Globals.currentPlanet.tribuAttitude = att;
							Debug.Log ("Agressif");
						}
					
					// Attitude neutre
					}else{
						if( Random.Range(1, 10) == 1 ){
							//Globals.currentPlanet.tribuAttitude = -1;
							Debug.Log ("Top neutre y'en a marre");
						}

						if( countQuestion == 0 && att == -2){
							// Fuite
							Debug.Log ("Fuite");
						}
					}

					Random.seed = (int)(Time.time * 10000);
					int randRep = Random.Range(0, reponsesFinal.Count);

					reponseObject.SetActive(true);
					GameObject reponseText = GameObject.Find ("Reponse");
					Text t = reponseText.GetComponent<Text>();

					t.text = (string)reponsesFinal.ToArray()[randRep];

					inReponse = true;
					noReset = true;

				// Fin de réponse
				}else{
					reponseObject.SetActive(false);
					inReponse = false;
					noReset = true;
				}
			}
		}

		// Reset de la selection
		if (!noReset) {
			selected = 0;
			realSelected = 0;
			skip = 0;
		}
	}

	void OnBack(){
		switch(currentAction){
		default:
		case "dialogs":
			currentAction = "actions";
			break;
		}

		Parse (actions.AsObject);
		UpdateList ();
		OnChange (0);
	}

	public void leave(){
		// Carte de la galaxie
		Application.LoadLevel("Galaxie");
	}

	public void take(){
		currentInv = "take";

		if (objets.Count > 0)
			ParseGround ();
		else
			Parse (actions.AsObject);

		ResetList ();
	}

	public void give(){
		currentInv = "give";
		ParseInv ();
		ResetList ();
	}

	public void sleep(){
		//Globals.playerHealth = 100;
	}

	public void eat(){
		currentInv = "eat";
		ParseInv ();
		ResetList ();
	}

	public void drink(){
		currentInv = "drink";
		ParseInv ();
		ResetList ();
	}

	void ParseInv(){
		currentList = new string[Globals.items.Count];
		currentListValues = new JSONNode[Globals.items.Count];
		
		for(int i = 0; i < Globals.items.Count; i++){

			string item = (string)Globals.items[i];

			if( currentInv == "eat" || currentInv == "drink" ){
				Objet def = allObjets[item];
				if( def.type != currentInv ) continue;
			}

			currentList[i] = item;
			currentListValues[i] = null;
		}
	}

	void ParseGround(){
		currentList = new string[objets.Count];
		currentListValues = new JSONNode[objets.Count];

		for(int i = 0; i < objets.Count; i++){
			currentList[i] = objets[i].ToString();
			currentListValues[i] = null;
		}
	}

	void ResetList(){
		selected = 0;
		realSelected = 0;
		skip = 0;
		
		UpdateList ();
		OnChange (0);
	}

}
