using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PlayerPrefsDelete : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("DELETE PREFSS!!");
		PlayerPrefs.DeleteAll();
	}
}
