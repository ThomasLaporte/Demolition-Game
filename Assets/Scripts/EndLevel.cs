using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

	public void leaveGame()
	{
		Application.Quit ();
	}

	public void nextLevel()
	{
		Debug.Log ("Nom scene = " + SceneManager.GetActiveScene ().name);

		// On charge le niveau 2 
		if (SceneManager.GetActiveScene ().name.Equals ("Level1")) {
			SceneManager.LoadScene ("Level2");
		}
	}
}
