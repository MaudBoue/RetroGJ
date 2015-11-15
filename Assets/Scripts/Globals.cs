using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Globals : MonoBehaviour {

	public static PlanetInfo[] planets;
	public static PlanetInfo currentPlanet;
	public static int currentPlanetId = 0;

	public static int playerHealth = 100;
	public static ArrayList items = new ArrayList();

	public static JSONNode getJSON(string filename){
		return JSON.Parse(System.IO.File.ReadAllText(filename));
	}

}
