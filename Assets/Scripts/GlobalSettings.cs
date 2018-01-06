using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalSettings : MonoBehaviour {

	public static int numberOfOperators = 2;
	public static QuestionGenerator.Level level = QuestionGenerator.Level.ADD;
	public static int largestNum = 100;
	public static int numOfQuestions = 10;

	public void updateLargestNum(float newValue) {
		largestNum = (int)Math.Round(newValue);
	}

	public void updateNumberOfOperators(int numOfOperators) {
		numberOfOperators = numOfOperators;
	}

	public void updateLevel(QuestionGenerator.Level newLevel) {
		level = newLevel;
	}
}
