using UnityEngine;
using System.Collections;

public class PlayMoving : MonoBehaviour {

	private float speed = 0.1f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.left * speed * Time.deltaTime);

		if (transform.position.x <= -1.0f) {
			transform.Translate(1.99f, 0, 0);
		}
	}
}
