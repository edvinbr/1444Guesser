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
	private HintTextScript hintTextScript;

	private List<string> countriesLeft;
	private int countryToFindIndex;
	private string countryToFind;
	private List<int> countriesDone = new List<int>();

	private static System.Random rng = new System.Random();

	private int score;
	private int time;
	private int guesses;

	private float hintTimerAmount = 3.0f;
	private float hintTimer;
	private float blinkTimerAmount = 0.5f;
	private float blinkTimer;
	private int blinkColor = 0;


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
		GameObject textC = GameObject.Find("HintText");
		hintTextScript = textC.GetComponent<HintTextScript>();

		setup();

		hintTimer = hintTimerAmount;
		blinkTimer = blinkTimerAmount;

	}

	void Update() {
		if (Input.GetKeyUp("r")) {
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
					blinkColor = 0;
					guesses = 0;
					mapHandler.paintCountry(idxOfClicked, pixelColor);
					setNextToFind();
					
				}
				else if (countriesLeft.Contains(countries[idxOfClicked])) {
					guesses++;
					hintTimerEnded();
					hintTextScript.movePositionToMouse();
					hintTextScript.setHintText(countries[idxOfClicked]);

					if(guesses >= 5) {
						blinkColor = 1;
					}
					
				}
			}
		}

		hintTimer -= Time.deltaTime;
		blinkTimer -= Time.deltaTime;

		if(hintTimer <= 0.0f) {
			hintTimerEnded();
		}

		if(blinkTimer <= 0.0f) {
			if (blinkColor == 1) {
				mapHandler.paintCountry(countryToFindIndex, Color.red);
				blinkColor = 2;
			}
			else if(blinkColor == 2){
				mapHandler.paintCountry(countryToFindIndex, Color.white);
				blinkColor = 1;
			}
			blinkTimer = blinkTimerAmount;
		}
	}

	private void setup() {

		foreach (int country in countriesDone) {
			mapHandler.paintCountry(country, new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f, 1.0f));
		}
		countriesDone = new List<int>();

		countriesLeft = new List<string>(countries);
		countriesLeft.Remove("land");
		countriesLeft.Remove("water");
		setNextToFind();

		

		score = 0;
		time = 0;
		guesses = 0;
		blinkColor = 0;

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
			countriesDone.Add(countryToFindIndex);
		}
		else {
			setup(); // Make game end, display score etc.
		}

	}

	private void hintTimerEnded() {
		hintTimer = hintTimerAmount;
		hintTextScript.setHintText("");
	}

}
