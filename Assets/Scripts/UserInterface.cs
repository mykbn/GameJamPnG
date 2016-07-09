using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour {

	[SerializeField] private TweenScale tsLogo;
	[SerializeField] private AnimationCurve normalAnimCurve;

	public void OnClickTap(){
		Debug.Log("TAPPED");
		if(Player.Instance != null){
			Player.Instance.MovePlayer();
		}
	}


	public void Start(){
		Player.Instance.HidePlayer(true);
		LightController.Instance.TurnOffLight();
		StartCoroutine(IntroLogo());
	}

	IEnumerator IntroLogo(){
		yield return new WaitForSeconds (0.5f);
		tsLogo.ResetToBeginning();
		tsLogo.Play();
		NGUITools.SetActive(tsLogo.gameObject, true);
		yield return new WaitForSeconds (3f);
		tsLogo.animationCurve = normalAnimCurve;
		tsLogo.duration = 0.3f;
		tsLogo.enabled = true;
		tsLogo.PlayReverse();
		yield return new WaitForSeconds(1f);
		Player.Instance.HidePlayer(false);
		LightController.Instance.GradualLightChange(8f);
		yield break;
	}

}
