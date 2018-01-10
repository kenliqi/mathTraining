using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

	public int questionCloneDelay = 3;
	public GameObject questionBallPrefab;
	public List<GameObject> questionBalls = new List<GameObject> ();

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
