using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

	public static LightController Instance;

	public delegate void OnFinishLightChange();
	public static OnFinishLightChange onFinishLightTransition;

	private Light lightObj;

	// Use this for initialization
	void Awake () {
		if (Instance == null) {
			Instance = this; 
		} else {
			Instance = null;
			Instance = this;
		}
		lightObj = this.GetComponentInChildren<Light>();
		onFinishLightTransition = null;
	}



	// Update is called once per frame
	void Update () {
		if (Player.Instance && UserInterface.Instance) {
			if(UserInterface.Instance.isIntroDone){
				float percent = Player.Instance.lightVal/100f;
				lightObj.range = (percent) * 30f; // 30f being the full value
				if(percent != 0 && lightObj.range < 4f){
					lightObj.range = 4f;

				}
			}
		}
	}

	public void TurnOffLight(){
		lightObj.range = 0f;
	}

	public void TurnOnLight(){
		lightObj.range = 30f;
	}

	public void GradualLightChange(float target){
		StartCoroutine(ChangeLightGradually(target));
	}

	IEnumerator ChangeLightGradually(float target){
		while(lightObj.range != target) {
			if(lightObj.range > target){
				//Gradually Decrease Light Intensity
				lightObj.range -= 0.2f;
			}else if(lightObj.intensity < target){
				//Gradually Increase Light Intensity
				lightObj.range += 0.2f;
			}
			yield return new WaitForEndOfFrame();
		}

		if(onFinishLightTransition != null){
			onFinishLightTransition();
//			StopAllCoroutines();
		}
		yield break;
	}
}
