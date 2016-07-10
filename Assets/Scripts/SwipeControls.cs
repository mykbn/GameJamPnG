using UnityEngine;
using System.Collections;

public class SwipeControls : MonoBehaviour {

	public delegate void OnSwipe(DIRECTIONS direction);
	public static OnSwipe Swipe;

	public float minSwipeDistY;
	
	public float minSwipeDistX;
	
	private Vector2 firstPressPos;

	private Vector2 secondPressPos;

	private Vector3 currentSwipe;
	
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

		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				//save began touch 2d point
				firstPressPos = new Vector2(t.position.x,t.position.y);
			}
			if(t.phase == TouchPhase.Ended)
			{
				//save ended touch 2d point
				secondPressPos = new Vector2(t.position.x,t.position.y);
				
				//create vector from the two points
				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
				
				//normalize the 2d vector
				currentSwipe.Normalize();
				
				//swipe upwards
				if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
				{
					SwipeControl(DIRECTIONS.UP);
					Debug.Log("up swipe");
				}
				//swipe down
				if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
				{
					SwipeControl(DIRECTIONS.DOWN);
					Debug.Log("down swipe");
				}
				//swipe left
				if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				{
					SwipeControl(DIRECTIONS.LEFT);
					Debug.Log("left swipe");
				}
				//swipe right
				if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				{
					SwipeControl(DIRECTIONS.RIGHT);
					Debug.Log("right swipe");
				}
			}
		}
	}

	public void SwipeControl(DIRECTIONS direction){
		if(Swipe != null){
			Swipe(direction);
			if(!GameManager.Instance.isGameStarted && !GameManager.Instance.isGameOver){
				GameManager.Instance.StartGame();
			}
//			if (PlayerPrefs.HasKey ("isFirstPlay")) {
//				if (PlayerPrefs.GetInt ("isFirstPlay") == 1) {
//					UserInterface.Instance.ShowInstructions(false);
//				}
//			} else {
			UserInterface.Instance.ShowInstructions(false);
//			}
		}
	}
}
