using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{

	public float questionCloneDelay = 3;
	private float speedUp = 1.2f;
	public GameObject questionBallPrefab;
	public TextMeshProUGUI answerTxt;
	public TextMeshProUGUI scoreTxt;
	private long answer;
	private int score = 0;
	public AudioClip ballExplodeSfx;
	private int speedupCnt = 0;

	/*** Keyboard control ***/
	private void refreshAnswer ()
	{
		answerTxt.text = answer.ToString (); 
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

	public void inverseSign ()
	{
		answer = answer * -1;
		refreshAnswer ();
	}

	public int openQuestions ()
	{
		return GameObject.FindGameObjectsWithTag ("QuestionBall").Length;
	}

	public void go ()
	{

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("QuestionBall")) {
			QuestionBall q = obj.GetComponent<QuestionBall> ();
			if (q.question.verify (answer)) {
				playSfx (ballExplodeSfx);
				scoreTxt.text = (++score).ToString ();
				clear ();
				Destroy (obj);
//				StartCoroutine(delayDestroy (obj));
				return;
			}
		}
		badAnswer ();

	}

	public void hint ()
	{
		showAnswer ();
		StartCoroutine (backToQuestion ());
	}


	private IEnumerator backToQuestion ()
	{
		yield return new WaitForSeconds (1);
		showQuestion ();
	}

	private void showAnswer ()
	{
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("QuestionBall")) {
			QuestionBall q = obj.GetComponent<QuestionBall> ();
			foreach (TextMeshProUGUI text in obj.GetComponentsInChildren<TextMeshProUGUI> ())
				text.SetText (q.question.answer.ToString ());
		}
	}

	private void showQuestion ()
	{
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("QuestionBall")) {
			QuestionBall q = obj.GetComponent<QuestionBall> ();
			foreach (TextMeshProUGUI text in obj.GetComponentsInChildren<TextMeshProUGUI> ())
				text.SetText (q.question.question.ToString ());
		}
	}

	private void badAnswer ()
	{
		Debug.Log ("Bad answer");
		clear ();
	}

	private IEnumerator delayDestroy (GameObject gameObj)
	{
		//set background color or sound
		yield return new WaitForSeconds (1);
		Destroy (gameObj);
	}
	/*** End of Keyboard control ***/

	private void nextLevel ()
	{
		questionCloneDelay /= speedUp;
	}

	public void generateQuestionBall ()
	{
//		Debug.Log ("Generating a new question");
		Vector3 startPoint = new Vector3 (UnityEngine.Random.Range (-100f, 700f), 100f, -100);
//		Debug.Log ("Starting point x " + startPoint.x + ", y " + startPoint.y);
		GameObject go = Instantiate (questionBallPrefab, startPoint, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
		go.transform.SetParent (GameObject.FindGameObjectWithTag ("BallArea").transform, false);
		go.transform.localPosition = startPoint;
		go.layer = 0;

	}

	private bool allowGeneratingQuestion = true;

	// Use this for initialization
	void Start ()
	{


	}

	private IEnumerator enableQuestionGeneration ()
	{
		yield return new WaitForSeconds(questionCloneDelay);
		//user speed is faster than we generate the question, we ignore this schedule generation as user's quick action has already generated this question
		if (speedupCnt > 0)
			speedUp--;
		else
			allowGeneratingQuestion = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (allowGeneratingQuestion || openQuestions () <= 0) {
			//User quick answer to spawn a new question
			if (!allowGeneratingQuestion)
				speedupCnt++;
			allowGeneratingQuestion = false;
			generateQuestionBall ();
			StartCoroutine (enableQuestionGeneration ());
		}
	}

	void playSfx (AudioClip _sfx)
	{
		Debug.Log ("Ball Exploded!");
		GetComponent<AudioSource> ().clip = _sfx;
		if (!GetComponent<AudioSource> ().isPlaying)
			GetComponent<AudioSource> ().Play ();
	}
}
