using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	[SerializeField] private GameObject goPlayer;

	Transform playerTransform;

	void Awake(){
		goPlayer = GameObject.FindObjectOfType<Player>().gameObject;
	}

	// Use this for initialization
	void Start () {
//		playerTransform = goPlayer.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(goPlayer){
			this.transform.position = new Vector3 (goPlayer.transform.position.x, goPlayer.transform.position.y, -10);
		}
	}
}
