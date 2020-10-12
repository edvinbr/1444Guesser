using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript : MonoBehaviour
{
	public Text scoreText;

	void Start() {

	}

	void Update() {

	}

	public void setScoreText(int score) {
		scoreText.text = "Score: " + score;
	}
}
