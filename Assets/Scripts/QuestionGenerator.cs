using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using TMPro;
using UnityEngine.SceneManagement;

/**
 * 
 **/
public class QuestionGenerator : MonoBehaviour
{

	public TextMeshProUGUI questionTxt;
	public TextMeshProUGUI answerTxt;
	private Question question;

	private Scores score = new Scores();
	private int answeredQuestions = 0;

	public void restartGame() {
		score = new Scores ();
		answeredQuestions = 0;
		generate ();
	}

	public enum Level
	{
		ADD = 0,
		SUB = 1,
		MUL = 2,
		DIV = 3,
		ADD_SUB = 4,
		ADD_SUB_MUL = 5,
		ALL = 6

	}
	public TextMeshProUGUI historyTxt;
	public int rightAnswer;

	public Stopwatch stopWatch = new Stopwatch ();

	public long answer = 0;

    public void hintAnswer()
    {
        Debug.Log("Used the hint!");
        answerTxt.text = question.answer.ToString();
    }

	//You have to specify all the functions hooks, then it will be called
	void Awake()
	{
		Debug.Log("Awake");
	}

	// called first
	void OnEnable()
	{
		Debug.Log("OnEnable called");
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("OnSceneLoaded: " + scene.name);
		Debug.Log(mode);
		generate ();
	}

	// called third
	void Start()
	{
		Debug.Log("Start");
	}

	// called when the game is terminated
	void OnDisable()
	{
		Debug.Log("OnDisable");
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public void scoreTheQuestion ()
	{
		if (stopWatch != null && stopWatch.IsRunning) {
			stopWatch.Stop ();
			// Get the elapsed time as a TimeSpan value.
			TimeSpan ts = stopWatch.Elapsed;

			// Format and display the TimeSpan value.
			string elapsedTime = String.Format ("{0:00}:{1:00}:{2:00}.{3:00}",
				                     ts.Hours, ts.Minutes, ts.Seconds,
				                     ts.Milliseconds / 10);

			bool answerIsRight = rightAnswer.Equals ((int)answer);
			string isCorrectAnswer = answerIsRight ? "[Correct]" : "[Wrong]";
			historyTxt.text += elapsedTime + " -> " + questionTxt.text + " = " + rightAnswer + "[" + answer.ToString () + "]" +
			isCorrectAnswer + "\n";
		}
	}

	public void onBtnClick (int i)
	{
		answer = answer * 10 + i;
		refreshAnswer ();
	}

	public void clear ()
	{
		answerTxt.text = "?";
		answer = 0;
	}

	private void refreshAnswer ()
	{
		answerTxt.text = answer.ToString (); 
	}


	public void refreshQuestion ()
	{
		this.generate ();
	}

	public Question generate ()
	{
		//Record previous question
		scoreTheQuestion ();
		question = QuestionFactory.generate ();
		questionTxt.text = question.question;
		answerTxt.text = question.answer.ToString ();
		rightAnswer = (int)question.answer;
		clear ();
		stopWatch = new Stopwatch ();
		stopWatch.Start ();

		return question;
	}

	public void inverseSign() {
		answer = answer * -1;
		refreshAnswer ();
	}

}
