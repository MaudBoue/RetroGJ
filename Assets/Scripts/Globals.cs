﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Globals : MonoBehaviour {

	public static int planetsCount = 6;
	public static int planetsVisited = 0;
	public static PlanetInfo[] planets;
	public static string[] planetsNames;
	public static PlanetInfo currentPlanet;
	public static int currentPlanetId = 0;

	public static int playerHealth = 100;
	public static int faim = 100;
	public static int soif = 100;
	public static ArrayList items = new ArrayList();

	public static bool quest1 = false;
	public static bool quest2 = false;

	public static JSONNode getJSON(string filename){
		return JSON.Parse(System.IO.File.ReadAllText(filename));
	}

	// A chaque nouvelle planete
	public static void CheckState(){

		if (planetsVisited == 0) {
			Globals.faim -= 10;
			Globals.soif -= 15;
		}

		if (faim <= 0) {
			// Mort de faim
		}else if(soif <= 0){
			// Mort de soif
		}

		planetsVisited++;
	}


	public static void GenerateGalaxie(){
		if (planets == null) {
			Bounds cameraBounds = PixelPerfectCamera.OrthographicBounds (Camera.main);
			float xMax = cameraBounds.extents.x - cameraBounds.extents.x * 0.1f;
			Globals.planets = new PlanetInfo[planetsCount];
			
			for (int i = 0; i < planetsCount; i++) {
				
				float x = Random.Range (- xMax, xMax);
				float y = Random.Range (- xMax, xMax);
				if (y < 0 && y < -4.5f)
					y = -4.5f;
				
				if (!CheckPosition (x, y)) {
					i--;
					continue;
				}
				
				int size = Random.Range (1, 3);
				//float scale = planet.transform.localScale.x / (size * 2);
				
				PlanetInfo p = new PlanetInfo ();
				p.x = x;
				p.y = y;
				//p.scale = scale;
				
				Globals.planets [i] = p;
			}
			
			PlanetInfo p2 = Globals.planets [1];
			p2.tribuId = 2;

			System.Array.Sort (Globals.planets, SortPlanets);
			
			PlanetInfo p1 = Globals.planets [0];
			p1.tribuId = 1;
			
			Globals.currentPlanet = p1;
			Globals.currentPlanetId = 0;
		}
	}

	static int SortPlanets(PlanetInfo a, PlanetInfo b){
		if (a.y > b.y) {
			return -1;
		} else if (a.y < b.y) {
			return 1;
		}
		return 0;
	}

	static bool CheckPosition(float x, float y){
		
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
}
