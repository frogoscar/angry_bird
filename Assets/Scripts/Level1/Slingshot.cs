using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	public GameObject bird;
	public AudioClip slingStretchedSound;
	public AudioClip flyingSound;
	public Color myColor;  // The color of the sling

	private bool isClicked = false; // The bird has been clicked (chosen) to be shotted
	private GameObject shottedBird; // The bird being shotted
	private Vector3 lastPosition; // The position of the bird when we pull the sling
	private Vector3 direction; // The direction of pulling
	private float magnitude;

	public static bool isDrawing = false; // If we will draw the track line of movement of the bird
	public static bool isJump = false; // If the next bird will jump to the slingshot after the current bird has been shotted
	public static GameObject myBird; // The flying bird
	public static int birdNumber = -1; // The number of the bird: 0, 1, 2 (we have three birds in Level1)

	private LineRenderer lineRenderer; // For drawing the sling
	//private int index = 0;
	private Vector3 position;

	//private float soundRate = 0.0f;

	// Use this for initialization
	void Start () {
		birdNumber = -1;

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material.color = myColor;
		lineRenderer.SetWidth(0.1f, 0.1f);
		lineRenderer.SetVertexCount(3); // We need 3 vertex to define the sling
	}

	// Update is called once per frame
	void Update () {
		bool isMouseDown = Input.GetMouseButton(0); // If the left mouse button is held down

		if (!isClicked) { // The bird has not been chosen (held by the mouse)

			if (isMouseDown) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (collider.Raycast(ray, out hit, 10.0f)) {
					isClicked = true; // The bird has been chosen (held/clicked)
					if (birdNumber <= 1) {
						// new Vector3(transform.position.x, transform.position.y, -0.1f)
						// GetComponent<Collider>()
						shottedBird = (GameObject)Instantiate(bird, new Vector3(transform.position.x, transform.position.y, -0.1f), Quaternion.identity); // Generate a bird
						Physics.IgnoreCollision(shottedBird.collider, gameObject.collider); // Ignore the collision of the shottedBird and that of the slingshot
						shottedBird.rigidbody.isKinematic = true;  // If isKinematic is enabled, Forces, collisions or joints will not affect the rigidbody anymore.

						myBird = GameObject.FindWithTag("bird0"); // The bird at the start position of pulling
						myBird.SetActive(false); // Not visible
					}
				}
			}
		}
		else if (isMouseDown) { // The bird has been chosen and is adjusting the sling
			Vector3 lastDirection = direction;

			lastPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.8f));

			direction = transform.position - lastPosition;
			direction.Normalize();

			magnitude = Mathf.Min(Vector3.Distance(transform.position, lastPosition), 1.8f);
			Vector3 tempPosition = transform.position + direction * (-magnitude);
			shottedBird.transform.position = new Vector3(tempPosition.x, tempPosition.y, -0.1f);

			// Draw the sling
			lineRenderer.enabled = true;
			lineRenderer.SetPosition(0, new Vector3(-4.88f, 0.35f, -0.1f)); // The point of Shooter02
			lineRenderer.SetPosition(1, shottedBird.transform.position); // The point of the position of the pulled bird
			lineRenderer.SetPosition(2, new Vector3(-5.31f, 0.23f, -0.3f)); // The point of Shooter01

			if (direction != lastDirection) { // We play the sound of sling stretched when the direction is different
				StartCoroutine(PlaySound(slingStretchedSound));
			}
		}
		else { // During the shotting of bird
			birdNumber++;
			if (birdNumber <= 2) {
				// Should we add  ---  shottedBird.tag = "bird(Clone)";  ---  here?

				GameObject birdObject2 = GameObject.FindWithTag("bird(Clone)");

				if (birdObject2 != null) {
					Destroy(birdObject2, 1);
				}

				lineRenderer.enabled = false;

				shottedBird.collider.isTrigger = false;
				shottedBird.rigidbody.isKinematic = false;
				shottedBird.rigidbody.velocity = direction * 7.7f * magnitude;
				shottedBird.rigidbody.useGravity = true;

				if (audio.isPlaying) {
					audio.Stop();
				}
				StartCoroutine(PlaySound(flyingSound));

				shottedBird.tag = "bird(Clone)";

				isClicked = false;

				isDrawing = true;

				isJump = true; // The current bird has been shotted out, so the next bird (if there is) should jump onto the slingshot
			}
		}
	}

	IEnumerator PlaySound(AudioClip soundName) {
		if (!audio.isPlaying) {
			audio.clip = soundName;
			audio.Play();
			yield return new WaitForSeconds(audio.clip.length);   // Yield usage : http://msdn.microsoft.com/en-us/library/9k7k7cf0.aspx
		}
	}

	// This function is not that good, not working every time
//	void PlaySound (AudioClip soundName) {
//		if(!audio.isPlaying && Time.time > soundRate){
//			soundRate = Time.time + soundName.length;
//			audio.clip = soundName;
//			audio.Play();
//		}
//	}
}
