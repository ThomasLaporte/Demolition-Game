using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {
    

    // public GameObject bird;

    public List<GameObject> lstBirds = new List<GameObject>();
    public Text txtMessage, txtScore;
//	public GameObject camera;
	public GameObject catapult;

    public bool clicActive = true;
    private int idBird = 0;

    private int score = 0;

    // Test  time 
    private GameObject currentBird;
    private float startTime = 0f;
    private float elapsedTime;

	private bool followBird = false;
    // Update is called once per frame
    void Update () {
		


        txtScore.text = "Score : " + score; 

        // Propulser l'oiseau lors du clic sur le bouton gauche
		if (Input.GetMouseButtonUp(0) && clicActive && idBird<= lstBirds.Count)
        {
            clicActive = false;

            startTime = Time.time;

			followBird = true; // La camera suit l'oiseau lance

            addForceOnBirds();
            
        }
       
        // Si l'oiseau n'a pas touché le sol, il est automatiquement détruit au bout de 3 secondes
        elapsedTime = Time.time - startTime;
		Vector3 posBird = lstBirds [idBird].transform.position;
		if (startTime != 0f && currentBird != null && System.Math.Round (elapsedTime) == 3) {
			if (lstBirds [idBird].transform.position == posBird) {
				destroyBird ();
				followBird = false;
			}
		} 
		else {
			if(followBird)
			{
				// TEst de modification de la position de la camera 
				Vector3 temp = GetComponent<Camera>().transform.position; // copy to an auxiliary variable...
				temp.x = lstBirds[idBird -1].transform.position.x; // modify the component you want in the variable...
				GetComponent<Camera>().transform.position = temp; // and save the modified value 
			}
		}
			

    }

    void addForceOnBirds()
    {

        //Debug.Log(Input.mousePosition.x - GameObject.Find("Slingshot").transform.position.x);
        currentBird = lstBirds[idBird];
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // récupération du rigidbody de la pomme afin de lui appliquer une force dans une direction aléatoire
        Rigidbody2D rb = lstBirds[idBird].GetComponent<Rigidbody2D>();

        // Force appliquée à l'oiseau pour l'envoyer 
		var test = Vector2.Distance (lstBirds [idBird].transform.position, Input.mousePosition);
		var testt = new Vector2 (lstBirds [idBird].transform.position.x + Input.mousePosition.x, lstBirds [idBird].transform.position.y + Input.mousePosition.y);
		var testttt = new Vector2 (Input.mousePosition.x - lstBirds [idBird].transform.position.x , Input.mousePosition.y - lstBirds [idBird].transform.position.y);

		float x = lstBirds [idBird].transform.position.x - Input.mousePosition.x;
		float y = lstBirds [idBird].transform.position.y - Input.mousePosition.y;



		Vector2 posCatapulte = lstBirds [idBird].transform.position;
		Vector2 posMouse2 = Input.mousePosition;
		Vector3 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//		posMouse.z = 0;
//		gameObject.transform.position = pz;



		Vector2 velocityTest = new Vector2 (lstBirds [idBird].transform.position.x + posMouse.x, lstBirds [idBird].transform.position.y + posMouse.y);
		rb.velocity = new Vector2(10, 10);
        //rb.velocity = new Vector2(Input.mousePosition.x, Input.mousePosition.y)*0.2f;
        //rb.AddForce(new Vector2(Input.mousePosition.x , Input.mousePosition.y) * 2, ForceMode2D.Force);

//        Debug.Log(/*"Diff : " + */GameObject.Find("Slingshot").transform.position.x - v3.x);

        // Incrémentation pour le prochain oiseau
        idBird++;
    }

    /// <summary>
    /// Récupère l'oiseau à instancier
    /// </summary>
    public int getIdBird
    {
        get {return idBird;}
    }

    /// <summary>
    /// Destruction de l'oiseau lancé
    /// </summary>
    public void destroyBird()
    {
        txtScore.text = 
        txtMessage.text = "+ 100pts";
        txtMessage.transform.position = currentBird.transform.position;

        score += 100;

        Destroy(currentBird);

        if (idBird <= lstBirds.Count - 1)
        {
            Debug.Log("L'id du bird  = " + idBird);
            lstBirds[idBird].transform.position = new Vector2(-6.5f, -3f); // Positionnement de l'oiseau dans le lance-pierre
            Rigidbody2D rb = lstBirds[idBird].GetComponent<Rigidbody2D>();

            rb.Sleep(); // Permet d'éviter à l'oiseau de tomber une fois qu'on a changé sa position
            clicActive = true;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }
		
}
