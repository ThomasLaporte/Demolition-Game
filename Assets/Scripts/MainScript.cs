﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {

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
	private Vector3 initialCamPos; // Position initiale de la caméra 
	private Vector3 initialBirdPos;

	public bool followBird = false;

	void Start()
	{
		initialCamPos = GetComponent<Camera>().transform.position;
		initialBirdPos = lstBirds [0].transform.position;
	}

	void Update () {

		txtScore.text = "Score : " + score; 

		// Si l'oiseau n'a pas touché le sol, il est automatiquement détruit au bout de 3 secondes
		elapsedTime = Time.time - startTime;
		//Vector3 posBird = lstBirds [idBird].transform.position;
		Vector3 posBird = initialBirdPos;

		if(idBird <= lstBirds.Count -1) 
		{posBird= lstBirds [idBird].transform.position;}
		if (startTime != 0f && currentBird != null && System.Math.Round (elapsedTime) == 5) {
			if (lstBirds [idBird].transform.position == posBird) {
				destroyBird ();
				followBird = false;
			}
		} 
		else {
			Vector3 posCamFollowBird = GetComponent<Camera>().transform.position; 
			if(followBird)
			{
				// La camera suit la position de l'oiseau qui est jeté
				posCamFollowBird.x = lstBirds[idBird -1].transform.position.x; 
				GetComponent<Camera>().transform.position = posCamFollowBird; 
			}
			else
			{
				GetComponent<Camera>().transform.position = initialCamPos;
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
		startTime = Time.time;
		//Debug.Log(Input.mousePosition.x - GameObject.Find("Slingshot").transform.position.x);
		currentBird = lstBirds[idBird];
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		// Récupération du rigidbody de l'oiseau courant pour lui appliquer une force
		Rigidbody2D rb = lstBirds[idBird].GetComponent<Rigidbody2D>();

		Vector3 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
		Vector3 realMousePosition = new Vector3 (posMouse.x, posMouse.y, 0f);

//		Debug.DrawLine(realMousePosition, catapult.transform.position, Color.red, 40000000000000);

		Vector3 lance = catapult.transform.position - lstBirds [idBird].transform.position;//realMousePosition;

		rb.velocity  = lance * 12f;
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
		txtMessage.transform.position = currentBird.transform.position;
		txtMessage.text = "+ 100pts";

		score += 100; // Incrementation du score

		Destroy(currentBird);

		if (idBird <= lstBirds.Count - 1)
		{
			lstBirds[idBird].transform.position = initialBirdPos; //new Vector2(-6.5f, -3f); // Positionnement de l'oiseau dans le lance-pierre
			Rigidbody2D rb = lstBirds[idBird].GetComponent<Rigidbody2D>();

			rb.Sleep(); // Permet d'éviter à l'oiseau de tomber une fois qu'on a changé sa position
			clicActive = true;
		}
		startTime = 0f;
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
