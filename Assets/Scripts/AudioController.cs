using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public static AudioController Instance;

	[SerializeField] private AudioSource[] bgm;
	[SerializeField] private AudioSource[] sfx;

	public GameObject goSFX;
	public GameObject goBGM;

	void Awake () {
		if (Instance == null) {
			Instance = this; 
		} else {
			Instance = null;
			Instance = this;
		}
	}

	void Start(){
		sfx = goSFX.GetComponents<AudioSource>();
		bgm = goBGM.GetComponents<AudioSource>();
	}

	public void PlayBGM(BGM type){
		switch (type) {
		case BGM.DUNGEON:
			bgm[0].Play();
			break;
		}
	}

	public void PlaySFX(SFX type){
		switch (type) {
		case SFX.PICKUP:
			sfx[0].Play();
			break;
		case SFX.DEATH:
			sfx[1].Play();
			break;
		}
	}

	public void StopBGM(BGM type){
		switch (type) {
		case BGM.DUNGEON:
			bgm[0].Stop();
			break;
		}
	} 
}
