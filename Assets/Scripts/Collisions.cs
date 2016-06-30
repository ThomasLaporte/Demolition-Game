using UnityEngine;

public class Collisions : MonoBehaviour {

    private bool goalValid = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCollisionEnter2D(Collision2D coll)
    {
		MainScript mainScript = GameObject.Find ("Main Camera").GetComponent<MainScript> ();

        Debug.Log(this.gameObject.tag);
        Debug.Log(coll.gameObject.tag);
        // Si l'oiseau touche le sol après avoir été envoyé
//        if (this.gameObject.tag == "Bird" && coll.gameObject.tag == "Ground")
//        {
//            GameObject.Find("Main Camera").GetComponent<MainScript>().destroyBird();
//
//        }

        // Si l'objectif a été touché
        if (this.gameObject.tag == "Goal" && coll.gameObject.tag == "Construction" && !goalValid)
        {
            goalValid = true;
            // On change la position du texte affichant le nom bre de points gagnés
			mainScript.txtMessage.text = " + 3000 pts !";
			mainScript.txtMessage.transform.position = this.gameObject.transform.position;
            Debug.Log("GAGNE");

            GameObject.Find("Main Camera").GetComponent<MainScript>().Score = GameObject.Find("Main Camera").GetComponent<MainScript>().Score + 3000;
        }


		if (this.gameObject.name == mainScript.lstBirds[mainScript.getIdBird].name  && (coll.gameObject.tag == "Construction" || coll.gameObject.tag == "Ground"))
			mainScript.startTime = Time.time;
    }

}
