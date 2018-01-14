using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalSettings : MonoBehaviour {

	public static int numberOfOperators = 2;
//	public static QuestionGenerator.Level level = QuestionGenerator.Level.ADD;
	public static int largestNum = 30;
	public static int numOfQuestions = 10;
	public static int questionsInterval = 3;

	public static bool hasAdd = true;
	public static bool hasSub = false;
	public static bool hasMul = false;
	public static bool hasDiv = false;

	public static bool isDead = false;

	public void updateQuestionInterval(int newInterval) {
		questionsInterval = newInterval;
	}

	public void updateLargestNum(float newValue) {
		largestNum = (int)Math.Round(newValue);
	}

	public void updateNumberOfOperators(int numOfOperators) {
		numberOfOperators = numOfOperators;
	}
		

	public void updateAdd(bool newValue) {
		hasAdd = newValue;
	}

	public void updateSub(bool newValue) {
		hasSub = newValue;
	}

	public void updateMul(bool newValue) {
		hasMul = newValue;
	}

	public void updateDiv(bool newValue) {
		hasDiv = newValue;
	}


}
