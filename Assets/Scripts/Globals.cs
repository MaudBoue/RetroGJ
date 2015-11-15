using UnityEngine;
using System.Collections;
using SimpleJSON;

public class Globals : MonoBehaviour {

	public static PlanetInfo[] planets;
	public static PlanetInfo currentPlanet;

	public static JSONNode getJSON(string filename){
		return JSON.Parse(System.IO.File.ReadAllText(filename));
	}

}
