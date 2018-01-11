using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

	/****
	 * 0 - Main Menui
	 * 1 - Training mode
	 * 2 - Play mode
	 ****/
	private readonly int MENU_MODE = 0;
	private readonly int TRAIN_MODE = 1;
	private readonly int PLAY_MODE = 2;

	public void PlayGame ()
	{
		SceneManager.LoadScene (PLAY_MODE);
	}

	public void Training ()
	{
		SceneManager.LoadScene (TRAIN_MODE);
	}

	public void toMainMenu ()
	{
		SceneManager.LoadScene (MENU_MODE);
	}

	public void QuitGame ()
	{
		Application.Quit ();
	}
		
}
