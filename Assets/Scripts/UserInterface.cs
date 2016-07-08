using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour {

	public void OnClickTap(){
		Debug.Log("TAPPED");
		if(Player.Instance != null){
			Player.Instance.MovePlayer();
		}
	}
}
