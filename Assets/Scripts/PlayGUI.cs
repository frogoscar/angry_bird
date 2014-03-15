using UnityEngine;
using System.Collections;

public class PlayGUI : MonoBehaviour {

	public GUISkin mySkin; // For HomeButton and PlayButton
	public Texture2D quitWindow;
	public Texture2D okButton;
	public Texture2D closeButton;

	private Rect okButtonPosition = new Rect(28.52f, 93.1f, 115, 115);
	private Rect closeButtonPosition = new Rect(280.63f, 93.1f, 115, 115);

	private Rect myWindow = new Rect(400 - 250, 300 - 60, 431, 215);
	private bool showWindow = false;

	private bool isSound1Button = false;
	private bool isSound2Button = true;
	private AudioSource sound;

	void OnGUI () {
		GUI.skin = mySkin;

		if (showWindow) {
			myWindow = GUI.Window(0, myWindow, DoMyWindow, "");
		}
		else {
			if (GUI.Button(new Rect(Screen.width / 2.0f - 200, Screen.height / 2.0f - 100, 442 * 0.8f, 283 * 0.8f), "", GUI.skin.GetStyle("PlayButton"))) {
				Application.LoadLevel(2); // Go to selection
			}
			if (GUI.Button(new Rect(Screen.width - 120, Screen.height - 120, 110, 110), "", GUI.skin.GetStyle("HomeButton"))) {
				showWindow = true;
			}
		}

		// For sound control : Mute or not
		if (isSound1Button) {
			if (GUI.Button(new Rect(15, Screen.height - 115, 105, 115), "", "AudioOffButton")) {
				AudioListener.pause = false;
				isSound1Button = false;
				isSound2Button = true;
			}
		}
		
		if (isSound2Button) {
			if (GUI.Button(new Rect(15, Screen.height - 115, 105, 115), "", "AudioButton")) {
				AudioListener.pause = true;
				isSound1Button = true;
				isSound2Button = false;
			}
		}
	}

	private void DoMyWindow (int windowID) {
		GUI.DrawTexture(new Rect(0, 0, 431, 215), quitWindow);

		if (GUI.Button(closeButtonPosition, closeButton)) {
			showWindow = false;
		}
		if (GUI.Button(okButtonPosition, okButton)) {
			Application.Quit();
		}

		//GUI.BringWindowToBack(0);
	}
}
