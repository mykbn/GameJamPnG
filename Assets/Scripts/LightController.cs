using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

	public static LightController Instance;

	private Light lightObj;

	// Use this for initialization
	void Awake () {
		Instance = this;
		lightObj = this.GetComponentInChildren<Light>();
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
		while(lightObj.intensity != target) {
			if(lightObj.intensity > target){
				//Gradually Decrease Light Intensity
				lightObj.intensity -= 0.2f;
			}else if(lightObj.intensity < target){
				//Gradually Increase Light Intensity
				lightObj.intensity += 0.2f;
			}
			yield return new WaitForEndOfFrame();
		}
		yield break;
	}
}
