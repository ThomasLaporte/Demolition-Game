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

	public bool followBird = false;
    // Update is called once per frame
    void Update () {
		


        txtScore.text = "Score : " + score; 

        // Propulser l'oiseau lors du clic sur le bouton gauche
//		if (Input.GetMouseButtonUp(0) && clicActive && idBird<= lstBirds.Count)
//        {
//            clicActive = false;
//
//            startTime = Time.time;
//
//			followBird = true; // La camera suit l'oiseau lance
//
//            addForceOnBirds();
//            
//        }
       
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
			Vector3 temp = GetComponent<Camera>().transform.position; // copy to an auxiliary variable...
			if(followBird)
			{
				// TEst de modification de la position de la camera 

				temp.x = lstBirds[idBird -1].transform.position.x; // modify the component you want in the variable...
				GetComponent<Camera>().transform.position = temp; // and save the modified value 
			}
//			else
//			{
//				temp.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
//				GetComponent<Camera>().transform.position = temp;
//			}
		}
			

    }

    public void addForceOnBirds()
    {

        //Debug.Log(Input.mousePosition.x - GameObject.Find("Slingshot").transform.position.x);
        currentBird = lstBirds[idBird];
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // récupération du rigidbody de la pomme afin de lui appliquer une force dans une direction aléatoire
        Rigidbody2D rb = lstBirds[idBird].GetComponent<Rigidbody2D>();

        // Force appliquée à l'oiseau pour l'envoyer 


		Vector3 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 realMousePosition = new Vector3 (posMouse.x, posMouse.y, 0f);

		//Debug.DrawLine(realMousePosition, catapult.transform.position, Color.red, 40000000000000);

		Vector3 lance = catapult.transform.position - realMousePosition;
	
		rb.velocity  = lance * 8f;
		Debug.Log (Vector3.Distance(realMousePosition, catapult.transform.position));
//		rb.AddForce(lance * 50f, ForceMode2D.Force); // new Vector2(Input.mousePosition.x , Input.mousePosition.y) * 2, ForceMode2D.Force

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
