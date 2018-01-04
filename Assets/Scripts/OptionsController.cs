using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OptionsController : MonoBehaviour {

	public TextMeshProUGUI currentLargestNum;
	public TextMeshProUGUI currentNumOperators;

	public void updateLargestNum(float newValue) {
		GlobalSettings.largestNum = (int)Math.Round (newValue);
		currentLargestNum.text = GlobalSettings.largestNum.ToString ();
	}

	public void updateNumOperators(float newValue) {
		GlobalSettings.numberOfOperators = (int)Math.Round (newValue);
		currentNumOperators.text = GlobalSettings.numberOfOperators.ToString();
	}

	public void updateLevel(String level) {
		GlobalSettings.level = (QuestionGenerator.Level)Enum.Parse(typeof(QuestionGenerator.Level), level, true);
	}



}
