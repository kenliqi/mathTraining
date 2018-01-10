using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour {
	
	public void PlayGame() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 2);
	}

	public void Training() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void toMainMenu() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
	}

	public void GameOption() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void QuitGame() {
		Application.Quit ();
	}
		
}
