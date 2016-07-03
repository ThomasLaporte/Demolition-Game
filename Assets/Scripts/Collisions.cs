using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour {

    private bool goalValid = false;
	public bool hit = false;

    public void OnCollisionEnter2D(Collision2D coll)
    {
		MainScript mainScript = GameObject.Find ("Main Camera").GetComponent<MainScript> ();

        // Si l'objectif a été touché
        if (this.gameObject.tag == "Goal" && coll.gameObject.tag == "Construction" && !goalValid)
        {
            goalValid = true;
            // On change la position du texte affichant le nombre de points gagnés
            GameObject.Find("Main Camera").GetComponent<MainScript>().Score = GameObject.Find("Main Camera").GetComponent<MainScript>().Score + 1000;
			foreach (GameObject ss in mainScript.lstBirds) {
				mainScript.Score += 300;
			}
			mainScript.Screens.GetComponent<ScreensScript> ().endLevelScreen.SetActive (true);
			int totalScore = mainScript.Score + 100;
			mainScript.Screens.GetComponent<ScreensScript> ().message.text = "Vous avez terminé le niveau avec " + totalScore +" points ! ";

        }

		if (this.gameObject.name == mainScript.lstBirds [mainScript.getIdBird].name && (coll.gameObject.tag == "Construction" || coll.gameObject.tag == "Ground") && hit == false) {
			mainScript.startTime = Time.time;
			hit = true;
		}
    }

}
