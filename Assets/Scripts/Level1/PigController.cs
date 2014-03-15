using UnityEngine;
using System.Collections;

public class PigController : MonoBehaviour {

	public GameObject digit10000;

	private GameObject myDisplay;
	private int index = 1; // Make sure display 10000 points only once
	private float y;

	public AudioClip minionWhat;

	// Use this for initialization
	void Start () {
		y = transform.position.y; // Initial position of Y of the pig
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(transform.position.y - y) > 0.3f) { // If the pig has been knocked down or moved
			if (myDisplay == null && index == 1) {
				ScoreController.myScore += 10000; // My CurrentScore should be added with 10000
				myDisplay = (GameObject)Instantiate(digit10000, transform.position, Quaternion.identity); // Display the 10000 points
				StartCoroutine(PlaySound(minionWhat));
				Destroy(myDisplay, 2); // Display for one second
				
				index++; // Only display once
			}
			Destroy(gameObject, 1f);
		}
	}

	IEnumerator PlaySound(AudioClip soundName) {
		if (!audio.isPlaying) {
			audio.clip = soundName;
			audio.Play();
			yield return new WaitForSeconds(audio.clip.length);   // Yield usage : http://msdn.microsoft.com/en-us/library/9k7k7cf0.aspx
		}
	}
}
