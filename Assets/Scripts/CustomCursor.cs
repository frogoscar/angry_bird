using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour {

	public Texture2D myCursor;
	public Texture2D myClickCursor;
	public float width;
	public float height;

	private bool showClickCursor = false;

	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) { // Click the mouse
			showClickCursor = true;
		}
		else {
			showClickCursor = false;
		}

	}

	void OnGUI () {
		if (showClickCursor) {
			GUI.DrawTexture(new Rect(Input.mousePosition.x - width / 2.0f, Screen.height - Input.mousePosition.y - height / 2.0f, width, height), myClickCursor);
		}
		else {
			GUI.DrawTexture(new Rect(Input.mousePosition.x - width / 2.0f, Screen.height - Input.mousePosition.y - height / 2.0f, width, height), myCursor);
		}

		GUI.depth = -1;
	}
}
