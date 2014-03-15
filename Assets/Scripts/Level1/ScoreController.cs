using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

	public GUISkin mySkin; // For MenuButton, ReloadButton and NextLevelButton

	private Rect backButtonPosition = new Rect(25, 5, 100, 100);

	public Texture2D passWindow;
	public Texture2D failWindow;

	private bool showPassWindow = false;
	private bool showFailWindow = false;
	private Rect myWindow = new Rect(400 - 220, 300 - 150, 450, 360);
	private Rect menuButtonPosition = new Rect(40.52f, 230.1f, 108, 108);
	private Rect reloadButtonPosition = new Rect(164.6f, 230.1f, 115, 115);
	private Rect nextLevelButtonPosition = new Rect(290.63f, 230.1f, 112, 112);

	public static int myScore;

	public AudioClip gameCompleted;  // Birds shount for wining
	public AudioClip gameFailed;     // Pigs grunt for losing

	public GameObject digit10000;
	private GameObject myDisplayBird0;
	private GameObject myDisplayBird2;
	private int indexBird0 = 1; // Make sure display 10000 points only once for bird0 (the left (not used bird) bird0)
	private int indexBird2 = 1; // Make sure display 10000 points only once for bird2 (the left (not used bird) bird2)

	public GameObject digit;
	public Texture2D score;
	public Texture2D highScore;

	private GameObject myDigit1;  // For displaying HighScore
	private GameObject myDigit2;  // For displaying CurrentScore

	// Use this for initialization
	void Start () {
		myDigit1 = (GameObject)Instantiate(digit, new Vector3(6.2f, 5.35f, -0.1f), Quaternion.identity);

		DigitDisplay myScript1 = myDigit1.transform.GetComponent<DigitDisplay>(); // Get the instance of class of DigitDisplay.cs for HighScore

		// PlayerPrefs : Stores and accesses player preferences between game sessions, similar to SharedPreferences in Android
		if (PlayerPrefs.HasKey("HighScore")) {
			myScript1.myStringScore = PlayerPrefs.GetInt("HighScore").ToString();
		}
		else {
			myScript1.myStringScore = "";
		}

		myDigit2 = (GameObject)Instantiate(digit, new Vector3(6.2f, 4.85f, -0.1f), Quaternion.identity);
	}

	void OnGUI () {
		GUI.skin = mySkin;

		GUI.Label(new Rect(305 + 320, 35, 70, 54), score);
		GUI.Label(new Rect(270 + 320, 0, 180, 57), highScore);

		if (GUI.Button(backButtonPosition, "", GUI.skin.GetStyle("BackButton"))) {
			TraceLine.birdsAllUsed = false;
			myScore = 0; // Re-initiate the current score
			Application.LoadLevel(2); // Go to Selection
		}

		if (showPassWindow) {
			myWindow = GUI.Window(0, myWindow, DoMyPassWindow, "");
		}
		if (showFailWindow) {
			myWindow = GUI.Window(0, myWindow, DoMyFailWindow, "");
		}
	}

	private void DoMyPassWindow (int windowID) { // The window shown when wining
		GUI.DrawTexture(new Rect(0, 0, 450, 302), passWindow);
		
		if (GUI.Button(menuButtonPosition, "", GUI.skin.GetStyle("MenuButton"))) {
			TraceLine.birdsAllUsed = false;
			myScore = 0; // Re-initiate the current score
			Application.LoadLevel(1);
		}
		if (GUI.Button(reloadButtonPosition, "", GUI.skin.GetStyle("ReloadButton"))) {
			showPassWindow = false;
			TraceLine.birdsAllUsed = false;
			myScore = 0; // Re-initiate the current score
			Application.LoadLevel(3);
		}
		if (GUI.Button(nextLevelButtonPosition, "", GUI.skin.GetStyle("NextLevelButton"))) {
			//Application.LoadLevel(4);
		}
		//GUI.BringWindowToBack(0);
	}

	private void DoMyFailWindow (int windowID) { // The window shown when losing
		GUI.DrawTexture(new Rect(0, 0, 450, 302), failWindow);
		
		if (GUI.Button(menuButtonPosition, "", GUI.skin.GetStyle("MenuButton"))) {
			TraceLine.birdsAllUsed = false;
			myScore = 0; // Re-initiate the current score
			Application.LoadLevel(1);
		}
		if (GUI.Button(reloadButtonPosition, "", GUI.skin.GetStyle("ReloadButton"))) {
			TraceLine.birdsAllUsed = false;
			myScore = 0; // Re-initiate the current score
			showFailWindow = false;
			Application.LoadLevel(3);
		}
		if (GUI.Button(nextLevelButtonPosition, "", GUI.skin.GetStyle("NextLevelButton"))) {
			//Application.LoadLevel(4);
		}
		//GUI.BringWindowToBack(0);
	}

	// Update is called once per frame
	void Update () {
		DigitDisplay myScript2 = myDigit2.transform.GetComponent<DigitDisplay>(); // Get the instance of class of DigitDisplay.cs for CurrentScore

		if (myScore == 0) { // Current score
			myScript2.myStringScore = "";
		}
		else {
			myScript2.myStringScore = myScore.ToString();
		}

		GameObject pigObject = GameObject.FindWithTag("pig"); // Try to find if there are still any pigs

		if (pigObject == null) { // Pigs have all been defeated, we win

			GameObject bird0Object = GameObject.FindWithTag("bird0"); // Try to find if there is still bird0 left (not used)
			GameObject bird2Object = GameObject.FindWithTag("bird1"); // Try to find if there is still bird2 left (not used)

			if (bird0Object != null) {
				if (myDisplayBird0 == null && indexBird0 == 1) {
					ScoreController.myScore += 10000; // My CurrentScore should be added with 10000
					myDisplayBird0 = (GameObject)Instantiate(digit10000, bird0Object.transform.position, Quaternion.identity); // Display the 10000 points

					Destroy(myDisplayBird0, 1); // Display for one second
					
					indexBird0++; // Only display once
				}
			}

			if (bird2Object != null) {
				if (myDisplayBird2 == null && indexBird2 == 1) {
					ScoreController.myScore += 10000; // My CurrentScore should be added with 10000
					myDisplayBird2 = (GameObject)Instantiate(digit10000, bird2Object.transform.position, Quaternion.identity); // Display the 10000 points

					Destroy(myDisplayBird2, 1); // Display for one second
					
					indexBird2++; // Only display once
				}
			}

			if (PlayerPrefs.GetInt("HighScore") < myScore) { // If my score is better the HighScore
				PlayerPrefs.SetInt("HighScore", myScore);
				PlayerPrefs.Save();
			}

			StartCoroutine(PlaySound(gameCompleted));

			StartCoroutine(WaitForTimes(2));

			showPassWindow = true; // Show the wining window

			//PlayerPrefs.DeleteKey("HighScore");
		}
		else if (TraceLine.birdsAllUsed) { // Birds have all been used up, but there are still pigs, we lose

			StartCoroutine(PlaySound(gameFailed));

			StartCoroutine(WaitForTimes(2));

			showFailWindow = true; // Show the losing window
		}
	}

	IEnumerator PlaySound(AudioClip soundName) {
		if (!audio.isPlaying) {
			audio.clip = soundName;
			audio.Play();
			yield return new WaitForSeconds(audio.clip.length);   // Yield usage : http://msdn.microsoft.com/en-us/library/9k7k7cf0.aspx
		}
	}

	IEnumerator WaitForTimes(float seconds) {
		yield return new WaitForSeconds(seconds);   // Yield usage : http://msdn.microsoft.com/en-us/library/9k7k7cf0.aspx
	}
}
