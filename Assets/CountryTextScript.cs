using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryTextScript : MonoBehaviour
{
	public Text findThisText;

	void Start() {

	}

	void Update() {

	}

	public void setCountryText(string country) {
		findThisText.text = "Click on " + country + "!";
	}

}
