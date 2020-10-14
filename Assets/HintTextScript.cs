using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintTextScript : MonoBehaviour
{
	public Text hintText;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public void setHintText(string country) {
		hintText.text = country;
	}

	public void movePositionToMouse() {
		hintText.transform.position = Input.mousePosition;
	}

}
