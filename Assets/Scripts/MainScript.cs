using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour {

	public List<GameObject> lstBirds = new List<GameObject>();
	public Text txtMessage, txtScore, txtHighScore;
	public GameObject posBirdInSlingshot;
	public GameObject Screens;

	public bool clicActive = true;
	private int idBird = 0;

	private int score = 0;

	private GameObject currentBird;
	public float startTime = 0f;
	private float elapsedTime;

	private Vector3 initialCamPos; // Position initiale de la caméra 

	public bool followBird = false;
	private bool showScore = false;
	private bool clicYellowBird = false;

	void Start()
	{
		initialCamPos = GetComponent<Camera>().transform.position;
		if (PlayerPrefs.GetString (SceneManager.GetActiveScene ().name) != "") {
			txtHighScore.text = "Meilleur Score : " + PlayerPrefs.GetString (SceneManager.GetActiveScene ().name);
		} else {
			txtHighScore.text = "Meilleur Score : 0";
		}
	}

	void Update () {
		
		txtScore.text = "Score : " + score; 

		// Si l'oiseau n'a pas touché le sol, il est automatiquement détruit au bout de 3 secondes
		elapsedTime = Time.time - startTime;
		//Vector3 posBird = lstBirds [idBird].transform.position;
		Vector3 posBird = posBirdInSlingshot.transform.position;

		if (idBird <= lstBirds.Count - 1 && lstBirds[idBird] != null) {
			posBird = lstBirds [idBird].transform.position;
		} 

		// Au bout de 3 secondes après avoir touché le sol, on affiche le nombre de points gagnés à l'utilisateur
		if (startTime != 0f && System.Math.Round (elapsedTime) == 3 && !showScore && currentBird!= null) {
			txtMessage.gameObject.SetActive(true);
			txtMessage.transform.position = new Vector3(currentBird.transform.position.x, currentBird.transform.position.y +0.5f, txtMessage.transform.position.z);
			txtMessage.text = "+ 100pts";

			score += 100; // Incrementation du score
			showScore = true;
		}
			
		// Si l'oiseau a touché le sol ou un obstacle depuis plus de 4 secondes OU Si sa position < -10 -->  on le détruit
		if ((startTime != 0f && currentBird != null && System.Math.Round (elapsedTime) == 4) || (currentBird != null && currentBird.transform.position.y < -20)) {
			destroyBird ();
			followBird = false;
			showScore = false;

			idBird++;
		} 
		else {
			Vector3 posCamFollowBird = GetComponent<Camera>().transform.position; 
			if(followBird)
			{
				// La camera suit la position de l'oiseau qui est jeté
				posCamFollowBird.x = lstBirds[idBird].transform.position.x; 
				GetComponent<Camera>().transform.position = posCamFollowBird; 
			}
			else
			{
				// Quand l'oiseau est détruit, on replace la caméra à sa place initiale
				GetComponent<Camera>().transform.position = initialCamPos;
			}
		}

		// Si l'oiseau actuel est l'oiseau jaune, on lui applique une force en x lors du clic
		if (Input.GetMouseButtonDown (0) && idBird < lstBirds.Count && lstBirds [idBird].name.Contains("yellow") && lstBirds [idBird].transform.position.x > posBirdInSlingshot.transform.position.x  && clicYellowBird == true) {
			Rigidbody2D rb = lstBirds[idBird].GetComponent<Rigidbody2D>();
			rb.AddForce (Vector2.right * 30, ForceMode2D.Impulse);
			clicYellowBird = false;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Screens.GetComponent<ScreensScript> ().breakInGame ();
		}
			
	}

	public void addForceOnBirds()
	{
		clicYellowBird = true;
		currentBird = lstBirds[idBird];
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		// Récupération du rigidbody de l'oiseau courant pour lui appliquer une force
		Rigidbody2D rb = lstBirds[idBird].GetComponent<Rigidbody2D>();
		rb.isKinematic = false;

		Vector3 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
		Vector3 realMousePosition = new Vector3 (posMouse.x, posMouse.y, 0f);

		// On détermine la direction du lance en fonction de la position de l'oiseau pendant le drag and drop et la catapulte
		Vector3 lance = posBirdInSlingshot.transform.position - lstBirds [idBird].transform.position;

		// On applique une velocitée à l'oiseau dans la direction définie
		rb.velocity  = lance * 13.5f;
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
		// Suppression de l'oiseau en court
		Destroy(currentBird);

		if (idBird < lstBirds.Count - 1) {
			lstBirds [idBird + 1].transform.position = posBirdInSlingshot.transform.position;
			Rigidbody2D rb = lstBirds [idBird + 1].GetComponent<Rigidbody2D> ();

			clicActive = true;
		} else {
			if (PlayerPrefs.GetString (SceneManager.GetActiveScene ().name) != "") {
				if (Score > int.Parse (PlayerPrefs.GetString (SceneManager.GetActiveScene ().name))) {
					PlayerPrefs.SetString (SceneManager.GetActiveScene ().name, Score.ToString ());
				}
			} else {
				PlayerPrefs.SetString (SceneManager.GetActiveScene ().name, Score.ToString ());
			}
			Screens.GetComponent<ScreensScript> ().lostScreen.SetActive (true);
		}
		startTime = 0f;
		txtMessage.gameObject.SetActive(false);
	}

	public int Score
	{
		get{return score;}
		set{score = value;}
	}
}
