using UnityEngine;
using System.Collections;

public class AniSprite : MonoBehaviour {
	
	public int columnSize;
	public int rowSize;
	public int colFrameStart; // From which column of the frames we will start the animation
	public int rowFrameStart; // From which row of the frames we will start the animation
	public int totalFrames;
	public int framesPerSecond;
	public float totalTime;
	public bool moveDirection;
	public float timeLength; // The time length of execution of the animatioin


	private float myTime = 0.0f;
	private float myTimeLength = 0.0f;
	private bool isPlay = true;
	private Vector2 scale;
	private Vector2 offset;


	void Update () {

		if (isPlay) {
			isPlay = DoAniSprite(columnSize, rowSize, colFrameStart, rowFrameStart, totalFrames, framesPerSecond, totalTime, moveDirection);
		}

		myTimeLength += Time.deltaTime;

		if (timeLength != 0 && myTimeLength > timeLength) {
			Destroy(gameObject);
		}
	}

	/**
	 * moveDirection: True means Forward animatioin, False means Backward animatioin
	 * 
	 */
	public bool DoAniSprite (int columnSize, int rowSize, int colFrameStart, int rowFrameStart, int totalFrames, int framesPerSecond, float totalTime, bool moveDirection) {

		myTime += Time.deltaTime;

		if (totalTime != 0 && myTime > totalTime) {
			isPlay = false;
			myTime = 0;
			return isPlay;
		}

		int index = ((int)(myTime * framesPerSecond)) % totalFrames;

		int v = index / columnSize;
		int u;

		if (moveDirection) { // Forward
			scale = new Vector2(1.0f / columnSize, 1.0f / rowSize);
			u = index % columnSize;
		}
		else { // Backward
			scale = new Vector2(-1.0f / columnSize, 1.0f / rowSize);
			u = -index % columnSize;
		}

		offset = new Vector2((u + colFrameStart) * scale.x, 1.0f - scale.y - (v + rowFrameStart) * scale.y);

		renderer.material.mainTextureScale = scale;
		renderer.material.mainTextureOffset = offset;

		return true;
	}


//	public void DoAniSprite(int columnSize, int framesPerSecond, bool moveDirection) { // moveDirection : True means Left, False means Right
//		float indexFloat = Time.time * framesPerSecond;
//		int indexInt = (int)indexFloat % columnSize;
//		Vector2 scale;
//		Vector2 offset;
//		
//		if (moveDirection) { // Left
//			scale = new Vector2(1.0f / columnSize, 1);
//			offset = new Vector2(indexInt * scale.x, 0);
//		}
//		else { // Right
//			scale = new Vector2(-1.0f / columnSize, 1);
//			offset = new Vector2(-indexInt * scale.x, 0);
//		}
//		
//		renderer.material.mainTextureScale = scale;
//		renderer.material.mainTextureOffset = offset;
//	}
//
//	public void DoAniFixedSprite(int columnSize, int index, bool moveDirection) { // Only execute one frame of the sequence
//		float x2 = 1.0f / columnSize;
//		float x1 = index * x2;
//
//		if (moveDirection) { // Left
//			renderer.material.mainTextureScale = new Vector2(x2, 1);
//			renderer.material.mainTextureOffset = new Vector2(x1, 0);
//		}
//		else { // Right
//			renderer.material.mainTextureScale = new Vector2(-x2, 1);
//			renderer.material.mainTextureOffset = new Vector2(x1, 0);
//		}
//	}
}
