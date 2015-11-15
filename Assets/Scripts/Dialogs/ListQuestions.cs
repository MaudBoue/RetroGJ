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

	void Awake(){
		actions = Globals.getJSON ("actions.json");
		dialogs = Globals.getJSON ("tribu1.json");
	}

	// Use this for initialization
	void Start () {
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
		JSONClass obj = item.AsObject;

		if (obj == null) {
			string str = item;

			// Do
			if( str.Contains("do:") ){
				str = str.Replace("do:", "");
				str = str.Replace("\"", "");
				
				MethodInfo method = this.GetType().GetMethod (str);
				if( method != null ){
					method.Invoke(this, null);
				}
			
			// Action
			}else{
				str = str.Replace("action:", "");
				str = str.Replace("\"", "");

				currentAction = str;
				Parse (dialogs.AsObject);
				UpdateList ();
				OnChange (0);
			}

		} else {
			if( currentAction != "dialogs" ){
				// Update de la liste avec les éléments enfants
				Parse (obj);
				UpdateList ();
				OnChange (0);
			}else{
				if( !inReponse ){

					JSONArray reponses = currentListValues[selected]["reponses"].AsArray;
					int att = currentListValues[selected]["attitude"].AsInt;

					// Atitude négative
					if( att != 0){

						if( Globals.currentPlanet != null && Globals.currentPlanet.tribuAttitude == 0)
							Globals.currentPlanet.tribuAttitude = att;
					
					// Attitude neutre
					}else{
						if( Random.Range(1, 10) == 1 ){
							Globals.currentPlanet.tribuAttitude = -1;
						}
					}

					int randRep = Random.Range(0, reponses.Count);

					reponseObject.SetActive(true);
					GameObject reponseText = GameObject.Find ("Reponse");
					Text t = reponseText.GetComponent<Text>();
					t.text = reponses[randRep];

					inReponse = true;
				}else{
					reponseObject.SetActive(false);

					inReponse = false;
				}
			}
		}

		// Reset de la selection
		selected = 0;
		realSelected = 0;
		skip = 0;

		// 
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

}
