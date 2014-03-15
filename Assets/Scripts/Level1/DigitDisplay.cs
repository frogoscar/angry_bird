using UnityEngine;
using System.Collections;

public class DigitDisplay : MonoBehaviour {

	public string myStringScore;

	public float x;
	public float y;

	public float scale = 1.0f;

	public Color myColor;

	public Texture2D[] myNumber = new Texture2D[10]; // The digits: 0,1,2,3,4,5,6,7,8,9

	private int width = 50;
	private int height = 72;

	void OnGUI () {
		GUI.color = myColor;

		Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

		x = screenPos.x - 70;
		y = Screen.height - screenPos.y - 20;

		if (myStringScore != null) {
			for (int i = 0; i < myStringScore.Length; i++) { // Display the digits of my score
				GUI.DrawTexture(new Rect(x + i * scale * width, y, scale * width, scale * height), myNumber[(int)char.GetNumericValue(myStringScore[i])]);
			}
		}
	}

}
