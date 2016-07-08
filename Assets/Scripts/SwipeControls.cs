using UnityEngine;
using System.Collections;

public class SwipeControls : MonoBehaviour {

	public delegate void OnSwipe(DIRECTIONS direction);
	public static OnSwipe Swipe;

	public float minSwipeDistY;
	
	public float minSwipeDistX;
	
	private Vector2 startPos;
	
	void Update()
	{
		if(Input.GetKey("up")){
			SwipeControl(DIRECTIONS.UP);

		}
		if(Input.GetKey("down")){
			SwipeControl(DIRECTIONS.DOWN);

		}
		if(Input.GetKey("right")){
			SwipeControl(DIRECTIONS.RIGHT);

		}
		if(Input.GetKey("left")){
			SwipeControl(DIRECTIONS.LEFT);

		}

		//#if UNITY_ANDROID
		if (Input.touchCount > 0) 
			
		{
			
			Touch touch = Input.touches[0];
			
			switch (touch.phase){
				
			case TouchPhase.Began:
				startPos = touch.position;
				break;
			case TouchPhase.Ended:
				float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
				if (swipeDistVertical > minSwipeDistY) 
				{
					float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
					if (swipeValue > 0){//up
						SwipeControl(DIRECTIONS.UP);
					}
												
					else if (swipeValue < 0){//down
						SwipeControl(DIRECTIONS.DOWN);
					}
								
				}

				float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
				if (swipeDistHorizontal > minSwipeDistX) 
					
				{
					float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
					
					if (swipeValue > 0){//right
						SwipeControl(DIRECTIONS.RIGHT);
					}
						
					else if (swipeValue < 0){//left
						SwipeControl(DIRECTIONS.LEFT);
					}
							
				}
				break;
			}
		}
	}

	public void SwipeControl(DIRECTIONS direction){
		if(Swipe != null){
			Swipe(direction);
		}
	}
}
