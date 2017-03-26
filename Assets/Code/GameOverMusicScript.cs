using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMusicScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SoundManager.Instance.PlayMenuMusic();
	}
	
}
