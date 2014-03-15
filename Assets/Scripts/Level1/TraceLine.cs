using UnityEngine;
using System.Collections;

public class TraceLine : MonoBehaviour {

	public GameObject particles; // The particle system we created
	public static bool birdsAllUsed = false; // Birds are all used up

	private int lengthOfLineRenderer = 100;
	private LineRenderer lineRenderer;
	private int index = 0; // The index of the points of the line
	private Vector3 position;
	private bool stopDrawing = false; // Stop drawing the line when the bird meets the ground

	private GameObject myParticles; 
	
	// Use this for initialization
	void Start () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		//lineRenderer.material = new Material(Shader.Find("Particles/Additive")); 
		lineRenderer.material = new Material(Shader.Find("Transparent/Diffuse"));
		lineRenderer.SetColors(Color.white, Color.white);
		lineRenderer.SetWidth(0.03f, 0.03f);
		lineRenderer.SetVertexCount(1);
	}

	IEnumerator OnCollisionEnter(Collision collision) { // If the bird meets something, we stop the bird and also stop drawing the traceline
		Slingshot.isDrawing = false; // Stop drawing the traceline
		
		stopDrawing = true;
		if (myParticles == null) { // Make sure that it only be created once
			myParticles = (GameObject)Instantiate(particles, transform.position, Quaternion.identity); // The particle system for displaying the feather of bird when it hits something
		}

		yield return StartCoroutine(WaitForTime(2.0f));

		gameObject.renderer.enabled = false; // Make bird(Clone) invisible

		if (Slingshot.birdNumber == 2) {
			yield return StartCoroutine(WaitForTime(2.0f));
			birdsAllUsed = true;
		}
	}

	// Update is called once per frame
	void Update () {
		if (stopDrawing) {
			transform.gameObject.rigidbody.velocity = new Vector3(0, transform.gameObject.rigidbody.velocity.y, transform.gameObject.rigidbody.velocity.z);
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
		}

		lineRenderer = GetComponent<LineRenderer>();

		position = transform.position;

		if (Slingshot.isDrawing) {
			lineRenderer.SetPosition(index, position);
			if (index >= (lengthOfLineRenderer - 1)) {
				return;
			}
			else {
				index++;
				lineRenderer.SetVertexCount(index+1);
				lineRenderer.SetPosition(index, position);
			}
		}
	}

	IEnumerator WaitForTime(float seconds) {
		yield return new WaitForSeconds(seconds);
	}
}
