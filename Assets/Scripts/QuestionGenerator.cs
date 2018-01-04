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
	private static readonly System.Random rnd = new System.Random ();

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

	public void updateElapsedTime ()
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
		updateElapsedTime ();

		string questionStr = "";
		List<RandNode> opdNodes = new List<RandNode> ();
		List<RandNode> oprNodes = new List<RandNode> ();

		RandNode rn = getOpd (GlobalSettings.largestNum);
		opdNodes.Add (rn);
		questionStr += rn.number.ToString ();

		for (int i = 0; i < GlobalSettings.numberOfOperators - 1; i++) {
			RandNode opd = getOpd (GlobalSettings.largestNum);
			opdNodes.Add (opd);	
			RandNode opr = getOps (GlobalSettings.level);
			oprNodes.Add (opr);

			questionStr += " " + QTree.getOperatorStr (opr.type) + " " + opd.number.ToString ();
		}

		questionTxt.text = questionStr;
		Debug.Log (questionStr);
		QTree root = QTreeBuilder.build (opdNodes, oprNodes);
		rightAnswer = (int)root.evaluate ();
		question = new Question (questionStr, rightAnswer);


		Debug.Log ("The correct answer is " + question.answer.ToString ());

		answerTxt.text = question.answer.ToString ();
		clear ();
		stopWatch = new Stopwatch ();
		stopWatch.Start ();

		return question;
	}

	//Get a random operand
	private RandNode getOpd (int max)
	{
		RandNode node = new RandNode ();
		node.number = rnd.Next (0, max + 1);
		return node;
	}


	//Get a random operator
	private RandNode getOps (Level level)
	{
		RandNode node = new RandNode ();
		switch (level) {
		case Level.ADD:
			node.type = QTree.Type.Add;
			break;
		case Level.SUB:
			node.type = QTree.Type.Sub;
			break;
		case Level.MUL:
			node.type = QTree.Type.Mul;
			break;
		case Level.DIV:
			node.type = QTree.Type.Div;
			break;
		case Level.ADD_SUB:
			int i = rnd.Next (0, 2);
			node.type = i == 0 ? QTree.Type.Add : QTree.Type.Sub;
			break;
		case Level.ADD_SUB_MUL:
			i = rnd.Next (0, 3);
			node.type = i == 0 ? QTree.Type.Add : i == 1 ? QTree.Type.Sub : QTree.Type.Mul;
			break;
		default:
			i = rnd.Next (0, 4);
			if (i == 0)
				node.type = QTree.Type.Add;
			else if (i == 1)
				node.type = QTree.Type.Sub;
			else if (i == 2)
				node.type = QTree.Type.Mul;
			else
				node.type = QTree.Type.Div;
			break;
	
		}
		return node;
	}

}
