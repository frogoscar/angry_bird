using UnityEngine;
using System.Collections;

public class WoodController : MonoBehaviour {

	public GameObject digit100;

	private GameObject myDisplay;
	private int index = 1; // Make sure display 100 points only once
	private float y;

	// Use this for initialization
	void Start () {
		y = transform.position.y; // Initial position of Y of the wood
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(transform.position.y - y) > 0.2f) { // If the wood has been knocked down or moved
			if (myDisplay == null && index == 1) {
				ScoreController.myScore += 100; // My CurrentScore should be added with 100
				myDisplay = (GameObject)Instantiate(digit100, transform.position, Quaternion.identity); // Display the 100 points

				Destroy(myDisplay, 1); // Display for one second

				index++; // Only display once
			}

		}
	}
}
