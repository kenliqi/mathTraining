using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scores : MonoBehaviour {
	public List<Question> questions = new List<Question>();
	public List<Score> answers = new List<Score>();

	public void scoreQuestion(TimeSpan time, Question newQuestion, long answer) {
		questions.Add (newQuestion);
		if (answer.Equals ("?")) {
			//user didn't answer
			answers.Add (ScoreFactory.NoAnswer ());
		} else {
			answers.Add(ScoreFactory.Answer(time, newQuestion.verify(answer)));
		}
	}

	public double totalScore() {
		int N = questions.Count;
		int corrected = 0;
		int wrong = 0;
		int passed = 0;
		TimeSpan totalTime = new TimeSpan ();
		foreach (Score s in answers) {
			if (s.noAnswer)
				passed++;
			else if (s.correctAns)
				corrected++;
			else
				wrong++;

			totalTime += s.time;
		}
		double finalScore = corrected / answers.Count;
		Debug.Log ("You got " + finalScore + "(" + corrected + "/" + answers.Count + ") in " + totalTime);
		return finalScore;
	}
}
