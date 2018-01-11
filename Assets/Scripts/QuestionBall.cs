using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionBall : MonoBehaviour {

	private float speed;							//movement speed (the faster, the harder)
	private float destroyThreshold = 1000f;			//if position is passed this value, the game is over.
	private static float gravity = 10f;

	public Question question = QuestionFactory.generate();

	public void showQuestion() {
		updateText (question.question.ToString());	
	}

	public void showAnswer() {
		updateText (question.answer.ToString());
	}

	private void fitText(string text) {
		float scale = text.Length / 3.0f;
		transform.localScale = new Vector3 (transform.localScale.x * scale, transform.localScale.y * scale, transform.localScale.z);
	}

	private void updateText(string newText) {
		TextMeshProUGUI[] text = transform.GetComponentsInChildren<TextMeshProUGUI> ();
//		Debug.Log ("Question on the Ball" + newText);
//		Debug.Log ("The child text component is null? " + text.Length);
		text[0].text = newText;
		fitText (newText);
	}

	void Start() {
		//set a random speed for each enemyball
		speed = Random.Range(6f, 20f);
		transform.name = "QuestionBall";
		showQuestion ();
//		Debug.Log ("Start droping the ball");
	}

	void Update() {
		//move the enemyball down

//		RectTransform rectTransform = this.GetComponent<RectTransform> ();
		Transform rectTransform = transform;
		speed = speed + gravity * Time.deltaTime;
		rectTransform.position += new Vector3(0, Time.deltaTime * speed, 0);

		//Debug.Log("New position " + rectTransform.position);
		//check for possible gameover
		if (rectTransform.position.y > destroyThreshold) {
//			GameController.gameOver = true;
			Destroy(gameObject);
		}
	}
}
