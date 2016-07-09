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
	
	}

	public void TurnOffLight(){
		lightObj.intensity = 0f;
	}

	public void TurnOnLight(){
		lightObj.intensity = 8f;
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
