using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragAndDrop : MonoBehaviour {
	private List<GameObject> lstBirds = new List<GameObject> ();
	private int birdID;
	private Vector3 posBird = new Vector3(0,0);
	private MainScript mainScript;


	void Start()
	{
		mainScript = GameObject.Find ("Main Camera").GetComponent<MainScript> ();
	}
	// Update is called once per frame
	void Update () {
		lstBirds = mainScript.lstBirds;
		birdID = mainScript.getIdBird;
	}
	
	void OnMouseDrag() {
		

		Vector3 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 realMousePosition = new Vector3 (posMouse.x, posMouse.y, 0f);
		Vector3 posCatapulte = new Vector3(mainScript.catapult.transform.position.x,mainScript.catapult.transform.position.y, 0f);

	
		//lstBirds [0].transform.position = Input.mousePosition;
		Vector3 testt = posCatapulte;
		Vector2 test = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		if (Mathf.Round(Vector3.Distance (realMousePosition, posCatapulte)) < 2f) {
			posBird = realMousePosition;
			testt = realMousePosition;
			lstBirds [birdID].transform.position = test;
			lstBirds [birdID].transform.rotation = new Quaternion(0,0,0,0);
		} else
			lstBirds [birdID].transform.position = posBird;
	}
	
	void OnMouseUp()
	{
		mainScript.followBird = true;
		mainScript.addForceOnBirds ();
	}
}