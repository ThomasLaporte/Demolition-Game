using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragAndDrop : MonoBehaviour {
	private List<GameObject> lstBirds = new List<GameObject> ();
//
//
	//private int idBird = GameObject.Find ("Main Camera").GetComponent<MainScript>().getIdBird;

	// Use this for initialization
	void Start () {
		lstBirds = GameObject.Find ("Main Camera").GetComponent<MainScript>().lstBirds;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown()
	{

	}
	
	void OnMouseDrag() {
		Vector3 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 realMousePosition = new Vector3 (posMouse.x, posMouse.y, 0f);
		Vector3 posCatapulte = new Vector3(GameObject.Find ("Main Camera").GetComponent<MainScript> ().catapult.transform.position.x,GameObject.Find ("Main Camera").GetComponent<MainScript> ().catapult.transform.position.y, 0f);

		//lstBirds [0].transform.position = Input.mousePosition;
		Vector3 testt = posCatapulte;
		Vector2 test = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		if (Mathf.Round(Vector3.Distance (realMousePosition, posCatapulte)) < 12.5f) {
			testt = realMousePosition;
			lstBirds [0].transform.position = test;
		} else
			lstBirds [0].transform.position = posCatapulte;
	}
	
	void OnMouseUp()
	{
		GameObject.Find ("Main Camera").GetComponent<MainScript> ().followBird = true;
		GameObject.Find ("Main Camera").GetComponent<MainScript> ().addForceOnBirds ();

	}


}

