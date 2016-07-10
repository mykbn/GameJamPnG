using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour {

	public static UserInterface Instance;

	[SerializeField] private TweenScale tsLogo;
	[SerializeField] private AnimationCurve normalAnimCurve;
	[SerializeField] private GameObject goIngameUI;
	[SerializeField] private GameObject goPauseMenu;
	[SerializeField] private UIProgressBar progBarLight;
	[SerializeField] private UIProgressBar progBarFood;
	[SerializeField] private UILabel lblScore;
	[SerializeField] private UILabel lblFoodCount;
	[SerializeField] private UILabel lblFuelCount;
	[SerializeField] private GameObject goGameOver;
	[SerializeField] private GameObject goGameOverRetry;
	[SerializeField] private UILabel lblFinalScore;
	[SerializeField] private UILabel lblHighScore;
	[SerializeField] private GameObject goInstructions;
	[SerializeField] private UILabel lblGameOver;

	public bool isIntroDone = false;
	public string strGameOverMessage = "";

	private const string highScorePrefsKey = "player_highscore";

	void Awake(){
		if (Instance == null) {
			Instance = this; 
		} else {
			Instance = null;
			Instance = this;
		}
	}

	public void OnClickTap(){
		Debug.Log("TAPPED");
		if(Player.Instance != null){
			Player.Instance.MovePlayer();
		}
	}


	public void Start(){
		Debug.Log("START!");
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

	public void ShowGameOver(){
		Time.timeScale = 0f;
		AudioController.Instance.StopBGM(BGM.DUNGEON);
		AudioController.Instance.PlaySFX(SFX.DEATH);
		GameManager.Instance.isGameOver = true;
		NGUITools.SetActive(goGameOver, true);
		LightController.onFinishLightTransition += AnimateGameOver;
		LightController.Instance.TurnOffLight();
		AnimateGameOver();
	}

	public void ShowGameOverRetry(){
		lblGameOver.text = strGameOverMessage;
		NGUITools.SetActive(goGameOverRetry, true);
		lblFinalScore.text = "You lasted for: " + FormatTime(Player.Instance.score);
		lblGameOver.text = strGameOverMessage;
		SaveHighScore(Player.Instance.score);
	}

	public void Retry(){
		Debug.Log("RETRY!");
		GameManager.Instance.Retry();
	}

	public void ShowInstructions(bool state){
		NGUITools.SetActive(goInstructions, true);
		if (state) {
			goInstructions.GetComponent<TweenScale>().ResetToBeginning();
			goInstructions.GetComponent<TweenScale>().enabled = true;
			goInstructions.GetComponent<TweenScale>().PlayForward();
		} else {
			goInstructions.GetComponent<TweenScale>().PlayReverse();
//			PlayerPrefs.SetInt ("isFirstPlay", 0);
		}

	}

	public string FormatTime(float fltTime){
		return string.Format("{0}:{1:00}", (int)fltTime / 60, (int)fltTime % 60);
	}

	private void SaveHighScore(float score){
		if (PlayerPrefs.HasKey (highScorePrefsKey)) {
			float highScore = PlayerPrefs.GetFloat(highScorePrefsKey);
			if (highScore < score) {
				PlayerPrefs.SetFloat (highScorePrefsKey, score);
			}
		} else {
			PlayerPrefs.SetFloat (highScorePrefsKey, score);
		}

		lblHighScore.text = "High Score: " + FormatTime(PlayerPrefs.GetFloat(highScorePrefsKey));

	}

	private void AnimateGameOver(){
		LightController.onFinishLightTransition -= AnimateGameOver;
		goGameOver.GetComponentInChildren<TweenScale>().ResetToBeginning();
		goGameOver.GetComponentInChildren<TweenScale>().enabled = true;
		goGameOver.GetComponentInChildren<TweenScale>().Play();
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
		Debug.Log("INTROLOGO");
		yield return new WaitForSeconds (0.5f);
		tsLogo.enabled = true;
		tsLogo.ResetToBeginning();
		tsLogo.Play();
		NGUITools.SetActive(tsLogo.gameObject, true);
		yield return new WaitForSeconds (3f);
		tsLogo.animationCurve = normalAnimCurve;
		tsLogo.duration = 0.3f;
		tsLogo.enabled = true;
		tsLogo.PlayReverse();
		AudioController.Instance.PlayBGM(BGM.DUNGEON);
		yield return new WaitForSeconds(2f);
		Player.Instance.HidePlayer(false);
		LightController.Instance.GradualLightChange(8f);
		yield return new WaitForSeconds(0.5f);
		ShowInGameUI();
		isIntroDone = true;
//		if (PlayerPrefs.HasKey ("isFirstPlay")) {
//			if (PlayerPrefs.GetInt ("isFirstPlay") == 1) {
//				ShowInstructions (true);
//			}
//		} else {
		ShowInstructions(true);
//		}

		yield break;
	}

	void Update(){
		if(Player.Instance){
			//Change Progress Bar Value Based on players lightVal
			progBarLight.value = (Player.Instance.lightVal / 100f);
			progBarFood.value = (Player.Instance.foodVal / 100f);
			lblScore.text = FormatTime(Player.Instance.score);
			lblFoodCount.text = (MazeManager.Instance.totalFoodCount - MazeManager.Instance.intFoodCount).ToString() + "/" + MazeManager.Instance.totalFoodCount.ToString();
			lblFuelCount.text = (MazeManager.Instance.totalFuelCount - MazeManager.Instance.intFuelCount).ToString() + "/" + MazeManager.Instance.totalFuelCount.ToString();


		}
	}


}
