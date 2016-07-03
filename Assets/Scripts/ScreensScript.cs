using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreensScript : MonoBehaviour {

	public GameObject lostScreen;
	public GameObject breakScreen;
	public GameObject endLevelScreen;
	public Text message;

	public void leaveGame()
	{
		Application.Quit ();
	}

	public void nextLevel()
	{

		// On charge le niveau 2 
		if (SceneManager.GetActiveScene ().name.Equals ("Level1")) {
			SceneManager.LoadScene ("Level2");
		} else {
			if (SceneManager.GetActiveScene ().name.Equals ("Level2")) {
				SceneManager.LoadScene ("Level3");
			}
		}
	}

	public void restartGame()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void continueCurrentLevel()
	{
		breakScreen.SetActive (false);
	}

	public void breakInGame()
	{
		breakScreen.SetActive (!breakScreen.activeSelf);
	}
}
