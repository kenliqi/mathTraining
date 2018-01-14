using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OptionsController : MonoBehaviour {

	public TextMeshProUGUI currentLargestNum;
	public TextMeshProUGUI currentNumOperators;
	public Toggle addToggle;
	public Toggle subToggle;
	public Toggle mulToggle;
	public Toggle divToggle;

	public void Start() {
		Debug.Log ("Start the menu");
		currentLargestNum.text = GlobalSettings.largestNum.ToString ();
		currentNumOperators.text = GlobalSettings.numberOfOperators.ToString();
		addToggle.isOn = GlobalSettings.hasAdd;
		subToggle.isOn = GlobalSettings.hasSub;
		mulToggle.isOn = GlobalSettings.hasMul;
		divToggle.isOn = GlobalSettings.hasDiv;
	}

	public void updateLargestNum(float newValue) {
		GlobalSettings.largestNum = (int)Math.Round (newValue);
		currentLargestNum.text = GlobalSettings.largestNum.ToString ();
	}

	public void updateNumOperators(float newValue) {
		GlobalSettings.numberOfOperators = (int)Math.Round (newValue);
		currentNumOperators.text = GlobalSettings.numberOfOperators.ToString();
	}
		

}
