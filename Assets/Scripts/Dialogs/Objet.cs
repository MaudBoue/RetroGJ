using System;

public class Objet{

	public string name = "Du vide";
	public string type = "useless";
	public int score = 0;
	public int scoreState = 0;

	public Objet (string type, int score = 0, int scoreState = 0){
		this.type = type;
		this.score = score;
		this.scoreState = scoreState;
	}

}

