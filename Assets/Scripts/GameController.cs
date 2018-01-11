using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{

	public int questionCloneDelay = 3;
	public GameObject questionBallPrefab;
	public TextMeshProUGUI answerTxt;
	public TextMeshProUGUI scoreTxt;
	private long answer;
	public List<GameObject> questionBalls = new List<GameObject> ();

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

	public void inverseSign() {
		answer = answer * -1;
		refreshAnswer ();
	}

	public void go() {

	}

	public void hint() {
	}
	/*** End of Keyboard control ***/

	public void generateQuestionBall ()
	{
		Debug.Log ("Generating a new question");
		Vector3 startPoint = new Vector3 (UnityEngine.Random.Range (-300f, 400f), -1000f, 7);
		Debug.Log ("Starting point x " + startPoint.x + ", y " + startPoint.y);
		GameObject go = Instantiate (questionBallPrefab, startPoint, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
		go.transform.SetParent (GameObject.FindGameObjectWithTag ("Canvas").transform, false);
		go.transform.localPosition = startPoint;

	}

	private bool allowGeneratingQuestion = true;

	// Use this for initialization
	void Start ()
	{


	}

	private IEnumerator enableQuestionGeneration ()
	{
		yield return new WaitForSeconds (questionCloneDelay);
		allowGeneratingQuestion = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (allowGeneratingQuestion) {
			allowGeneratingQuestion = false;
			generateQuestionBall ();
			StartCoroutine (enableQuestionGeneration ());
		}
	}
}
