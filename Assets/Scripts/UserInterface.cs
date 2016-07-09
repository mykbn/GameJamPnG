using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour {

	[SerializeField] private TweenScale tsLogo;
	[SerializeField] private AnimationCurve normalAnimCurve;
	[SerializeField] private GameObject goIngameUI;
	[SerializeField] private GameObject goPauseMenu;

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

	public void ShowInGameUI(){
		NGUITools.SetActive (goIngameUI, true);
		FadeChildren(goIngameUI, true);
	}

	public void PauseGame(){
		NGUITools.SetActive(goPauseMenu, true);
		Time.timeScale = 0f;
		FadeChildren(goPauseMenu, true);
	}

	public void UnPauseGame(){
		NGUITools.SetActive(goPauseMenu, false);
		Time.timeScale = 1f;
		FadeChildren(goPauseMenu, false);
	}

	private void FadeChildren(GameObject goParent, bool fadeIn){
		TweenAlpha[] tweens = goParent.GetComponentsInChildren<TweenAlpha>();
		for (int i = 0; i < tweens.Length; i++) {
			tweens[i].enabled = true;
			if(fadeIn){
				tweens[i].ResetToBeginning();
				tweens[i].Play();
			}else{
				tweens[i].PlayReverse();
			}
		}
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
		yield return new WaitForSeconds(0.5f);
		ShowInGameUI();
		yield break;
	}


}
