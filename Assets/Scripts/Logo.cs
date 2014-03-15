using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour {

	private int timeLength = 10;
	//public int nextLevel;
	private float myTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) { // If click left mouse botton, we enter to next scene directly
			Application.LoadLevel(1);
		}

		myTime += Time.deltaTime;
		if (myTime > timeLength) {
			Application.LoadLevel(1);
		}
	}
}