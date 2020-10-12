using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Random;

public class GameLoop : MonoBehaviour
{

	private List<(int, int, int)> countryColors;
	private List<string> countries;
	private MapHandler mapHandler;
	private CountryTextScript countryTextScript;
	private ScoreTextScript scoreTextScript;

	private List<string> countriesLeft;
	private int countryToFindIndex;
	private string countryToFind;

	private static System.Random rng = new System.Random();

	private int score;
	private int time;
	private int guesses;

	void Start()
	{
		GameObject thecsvp = GameObject.Find("CSVParser");
		CSVParser csvpScript = thecsvp.GetComponent<CSVParser>();
		countryColors = csvpScript.colorsOnly;
		countries = csvpScript.countriesOnly;

		GameObject map = GameObject.Find("colorOutlined");
		mapHandler = map.GetComponent<MapHandler>();

		GameObject textA = GameObject.Find("CountryText");
		countryTextScript = textA.GetComponent<CountryTextScript>();
		GameObject textB = GameObject.Find("ScoreText");
		scoreTextScript = textB.GetComponent<ScoreTextScript>();

		setup();

	}

	void Update() {
		if (Input.GetKey("r")) {
			setup();
		}

		if (Input.GetMouseButtonUp(0)) {

			(int idxOfClicked, Color pixelColor) = mapHandler.getClickedCountry();
			//print("idx of clicked " + idxOfClicked);
			print("Country clicked " + countries[idxOfClicked]);
			if (idxOfClicked >= 0) {

				if (idxOfClicked == countryToFindIndex) {
					score += Mathf.Max(0, 20 * (5 - guesses));
					scoreTextScript.setScoreText(score);
					mapHandler.paintCountry(idxOfClicked, pixelColor);
					setNextToFind();
				}
				else if (countriesLeft.Contains(countries[idxOfClicked])) {
					guesses++;
				}
			}
		}
	}

	private void setup() {
		countriesLeft = new List<string>(countries);
		countriesLeft.Remove("land");
		countriesLeft.Remove("water");
		setNextToFind();

		score = 0;
		time = 0;
		guesses = 0;

		scoreTextScript.setScoreText(score);

	}

	private void setNextToFind() {
		if (countriesLeft.Count > 0) {
			int tempi = rng.Next(countriesLeft.Count);
			countryToFind = countriesLeft[tempi];
			countriesLeft.Remove(countryToFind);
			print(countriesLeft.Count + " countries left!");
			countryToFindIndex = countries.IndexOf(countryToFind);
			countryTextScript.setCountryText(countryToFind);
		}
		else {
			setup(); // Make game end, display score etc.
		}

	}

}
