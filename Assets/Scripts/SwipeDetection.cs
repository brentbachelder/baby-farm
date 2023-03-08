using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
	public CameraMovement camMove;
	public float MAX_SWIPE_TIME = 0.5f;

	// Factor of the screen width that we consider a swipe
	// 0.17 works well for portrait mode 16:9 phone
	public float MIN_SWIPE_DISTANCE = 0.17f;

	public static bool swipedRight = false;
	public static bool swipedLeft = false;


	public bool debugWithArrowKeys = true;

	Vector2 startPos;
	float startTime;

	public void Update()
	{
		swipedRight = false;
		swipedLeft = false;

		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				startPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);
				startTime = Time.time;
			}
			if(t.phase == TouchPhase.Ended)
			{
				if(Time.time - startTime > MAX_SWIPE_TIME) // press too long
					return;

				Vector2 endPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);

				Vector2 swipe = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

				if(swipe.magnitude < MIN_SWIPE_DISTANCE) // Too short swipe
					return;

				if(Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
				{ // Horizontal swipe
					if(swipe.x > 0)
					{
						swipedRight = true;
					}
					else
					{
						swipedLeft = true;
					}
				}
			}
		}

		if(debugWithArrowKeys)
		{
			swipedRight = swipedRight || Input.GetKeyDown(KeyCode.RightArrow);
			swipedLeft = swipedLeft || Input.GetKeyDown(KeyCode.LeftArrow);
		}

		if(swipedRight) camMove.ChangeTarget(-1);
		if(swipedLeft) camMove.ChangeTarget(1);
	}
}
