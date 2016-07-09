using UnityEngine;
using System.Collections;

public class MazeManager : MonoBehaviour {
	public Player player;
	public GameObject[] goMazes;
	public GameObject goCurrentMaze;
	public float[] fltRotationAngles;

	public void RotateMazes(){
		switch(player.currentMaze){
		case MAZES.A:
			goCurrentMaze = goMazes[0];
			break;
		case MAZES.B:
			goCurrentMaze = goMazes[1];
			break;
		case MAZES.C:
			goCurrentMaze = goMazes[2];
			break;
		case MAZES.D:
			goCurrentMaze = goMazes[3];
			break;
		}

		for(int i = 0; i < goMazes.Length; i++){
			if(goCurrentMaze.name != goMazes[i].name){
				int intRandom = UnityEngine.Random.Range(0,4);
				goMazes[i].transform.rotation = Quaternion.identity;
				goMazes[i].transform.Rotate(new Vector3(0f,0f,fltRotationAngles[intRandom]));
			}
		}
	}
}
