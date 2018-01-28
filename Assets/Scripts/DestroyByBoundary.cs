using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {
	public AudioClip deadAudio;

	void Start() {
		//BoxCollider collider = GetComponent<BoxCollider>();
		//MeshRenderer renderer = GetComponent<MeshRenderer>();
		//collider.center = GetComponent<Renderer>().bounds.center;
		//collider.size = GetComponent<Renderer>().bounds.size;
		
	}
	void OnTriggerExit(Collider other)
	{
		playSfx (deadAudio);
		GlobalSettings.isDead = true;
		// Destroy everything that leaves the trigger
		Destroy(other.gameObject);
	}

	void playSfx (AudioClip _sfx)
	{
		Debug.Log ("Ball Exploded!");
		GetComponent<AudioSource> ().clip = _sfx;
		if (!GetComponent<AudioSource> ().isPlaying)
			GetComponent<AudioSource> ().Play ();
	}

	void showResult() {
		GameObject canvas = GameObject.FindGameObjectWithTag ("BallArea");
		GameObject gameOverCanvas = GameObject.FindGameObjectWithTag ("GameOver");
		canvas.SetActive (false);
		gameOverCanvas.SetActive (true);
	}
}
