using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MazeManager : MonoBehaviour {
	public static MazeManager Instance;

	public Player player;
	public GameObject goFuel;
	public GameObject goFood;

	public GameObject[] goMazes;
	public GameObject goCurrentMaze;
	public float[] fltRotationAngles;
	public Transform[] transSpawnAreaMazeA;
	public Transform[] transSpawnAreaMazeB;
	public Transform[] transSpawnAreaMazeC;
	public Transform[] transSpawnAreaMazeD;

	public int intFoodCount;
	public int intFuelCount;
//	public int intMaxSpawnAmount;

	void Awake(){

	}
	void Start(){
		SpawnFuelAndFood(11);
	}
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
	public void SpawnFuelAndFood(int intAmount){
		List<Transform> currentMazeA = new List<Transform>(transSpawnAreaMazeA);
		for(int i = 0; i < intAmount; i++){
			int intRandom = UnityEngine.Random.Range(0,currentMazeA.Count);
			int intRandomItem = UnityEngine.Random.Range(0,2);
			GameObject go;
			if(intRandomItem == 0){
				go = (GameObject) GameObject.Instantiate(goFuel);
				intFuelCount += 1;


			}else{
				go = (GameObject) GameObject.Instantiate(goFood);
				intFoodCount += 1;
			}

			go.transform.parent = currentMazeA[intRandom].transform;
			go.transform.localPosition = Vector3.zero;
			currentMazeA.RemoveAt(intRandom);
		}

		List<Transform> currentMazeB = new List<Transform>(transSpawnAreaMazeB);
		for(int i = 0; i < intAmount; i++){
			int intRandom = UnityEngine.Random.Range(0,currentMazeB.Count);
			int intRandomItem = UnityEngine.Random.Range(0,2);
			GameObject go;
			if(intRandomItem == 0){
				go = (GameObject) GameObject.Instantiate(goFuel);
				intFuelCount += 1;

			}else{
				go = (GameObject) GameObject.Instantiate(goFood);
				intFoodCount += 1;

			}
			go.transform.parent = currentMazeB[intRandom].transform;
			go.transform.localPosition = Vector3.zero;
			currentMazeB.RemoveAt(intRandom);

		}

		List<Transform> currentMazeC = new List<Transform>(transSpawnAreaMazeC);
		for(int i = 0; i < intAmount; i++){
			int intRandom = UnityEngine.Random.Range(0,currentMazeC.Count);
			int intRandomItem = UnityEngine.Random.Range(0,2);
			GameObject go;
			if(intRandomItem == 0){
				go = (GameObject) GameObject.Instantiate(goFuel);
				intFuelCount += 1;

			}else{
				go = (GameObject) GameObject.Instantiate(goFood);
				intFoodCount += 1;

			}
			go.transform.parent = currentMazeC[intRandom].transform;
			go.transform.localPosition = Vector3.zero;
			currentMazeC.RemoveAt(intRandom);

		}

		List<Transform> currentMazeD = new List<Transform>(transSpawnAreaMazeD);
		for(int i = 0; i < intAmount; i++){
			int intRandom = UnityEngine.Random.Range(0,currentMazeD.Count);
			int intRandomItem = UnityEngine.Random.Range(0,2);
			GameObject go;
			if(intRandomItem == 0){
				go = (GameObject) GameObject.Instantiate(goFuel);
				intFuelCount += 1;

			}else{
				go = (GameObject) GameObject.Instantiate(goFood);
				intFoodCount += 1;

			}
			go.transform.parent = currentMazeD[intRandom].transform;
			go.transform.localPosition = Vector3.zero;
			currentMazeD.RemoveAt(intRandom);

		}
	}

}
