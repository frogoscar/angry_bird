using UnityEngine;
using System.Collections;

public class BirdMoving : MonoBehaviour {

	private bool isGrounded = false; // The bird is on the ground
	private float randomNumber;

	public float seconds;

	// Use this for initialization
	void Start () {
			InvokeRepeating("Move", 2, seconds); // Execute the function Move from the the 2th second, and once every "seconds" seconds
	}

	private void Move () {
		if (!gameObject.rigidbody.isKinematic) {
			transform.rigidbody.velocity = new Vector3(0, 2.0f, 0);
		}
		isGrounded = false;
		randomNumber = Random.Range(0, 1.0f);
	}

	void OnCollisionEnter (Collision collision) { // When the bird meets the ground
		isGrounded = true;
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isGrounded) { // Only Rotate when the bird is not on the ground
			if (randomNumber > 0.5f) { // 50% possibility
				transform.Rotate(Vector3.back * Time.deltaTime * 400);
			}
			else {
				transform.Rotate(Vector3.forward * Time.deltaTime * 400);
			}
		}

		if (Slingshot.isJump) { // The next bird should jump onto the slingshot
			StartCoroutine(BirdJump());
		}
	}

	// When the precedent bird has been shotted, the next (if any) should jump onto the slingshot automatically
	IEnumerator BirdJump (){
		yield return new WaitForSeconds(1.0f);

		if (gameObject.name == "bird1" && Slingshot.birdNumber == 0) { // bird0 has been shotted, we should prepare bird1
			transform.animation.Play(); // Play the animation of jump of bird1
			transform.gameObject.collider.isTrigger = true; // If not do this, the bird will fall down

			Slingshot.isJump = false; // Jump action has been done, reset the isJump flag

			yield return new WaitForSeconds(1.0f);

			Slingshot.myBird.SetActive(true); // bird0
			Slingshot.myBird.transform.animation.Stop();

			Destroy(gameObject);
		}
		else if (gameObject.name == "bird2" && Slingshot.birdNumber == 1) { // bird0 has been shotted, we should prepare bird1
			transform.animation.Play(); // Play the animation of jump of bird1
			transform.gameObject.collider.isTrigger = true; // If not do this, the bird will fall down
			
			Slingshot.isJump = false; // Jump action has been done, reset the isJump flag
			
			yield return new WaitForSeconds(1.0f);
			
			Slingshot.myBird.SetActive(true); // bird0
			Slingshot.myBird.transform.animation.Stop();
			
			Destroy(gameObject);
		}
	}
}
