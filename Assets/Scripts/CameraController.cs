using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	[SerializeField] private GameObject goPlayer;

	Transform playerTransform;

	// Use this for initialization
	void Start () {
//		playerTransform = goPlayer.transform;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(goPlayer.transform.position.x, goPlayer.transform.position.y, -10);
	}
}
