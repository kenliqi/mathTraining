using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {
	public AudioClip deadAudio;

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
}
