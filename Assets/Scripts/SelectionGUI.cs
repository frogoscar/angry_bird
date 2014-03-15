using UnityEngine;
using System.Collections;

public class SelectionGUI : MonoBehaviour {

	public GUISkin mySkin;

	private Rect backButtonPosition = new Rect(40, 500, 100, 100);

	private Rect level1ButtonPosition = new Rect(125, 60, 150, 150);
	private Rect level2ButtonPosition = new Rect(325, 60, 150, 150);
	private Rect level3ButtonPosition = new Rect(525, 60, 150, 150);

	private Rect level4ButtonPosition = new Rect(125, 260, 150, 150);
	private Rect level5ButtonPosition = new Rect(325, 260, 150, 150);
	private Rect level6ButtonPosition = new Rect(525, 260, 150, 150);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		GUI.skin = mySkin;

		if (GUI.Button(level1ButtonPosition, "", GUI.skin.GetStyle("Level1Button"))) {
			Application.LoadLevel(3); // Go to Level 1
		}

		GUI.Button(level2ButtonPosition, "", GUI.skin.GetStyle("Level2Button"));
		GUI.Button(level3ButtonPosition, "", GUI.skin.GetStyle("Level3Button"));
		GUI.Button(level4ButtonPosition, "", GUI.skin.GetStyle("Level4Button"));
		GUI.Button(level5ButtonPosition, "", GUI.skin.GetStyle("Level5Button"));
		GUI.Button(level6ButtonPosition, "", GUI.skin.GetStyle("Level6Button"));

		if (GUI.Button(backButtonPosition, "", GUI.skin.GetStyle("BackButton"))) {
			Application.LoadLevel(1); // Go to Start
		}
	}
}
