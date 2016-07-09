using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static GameManager Instance;
	
	public bool isGameStarted = false;
	float timer = 0f;
	
	void Awake () {
		Instance = this;
	}
	
	void Start(){
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isGameStarted) {
			timer += Time.deltaTime;
			Player.Instance.SetScore(timer);
		}
	}
	
	[ContextMenu("Start Game")]
	public void StartGame(){
		isGameStarted = true;
		InvokeRepeating("ReduceValues", 1f, 1f);
	}
	
	public void ReduceValues(){
		Player.Instance.ReduceLightValue(2f);
	}
	
	
	
}
