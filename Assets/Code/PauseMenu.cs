using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    // Use this for initialization
    public static PauseMenu instance;
    void Awake()
    {
        instance = this;
        this.gameObject.GetComponent<Canvas>().enabled = false;
    }
    void Start () {
		
	}

    public void stopTime()
    {
        GameController.Instance.stopTime();
    }
    public void show()
    {
        if (this.gameObject.GetComponent<Canvas>().enabled)
        {
            this.gameObject.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<Canvas>().enabled = true;
        }
    }
    public void QuitGame()
    {
        Debug.Log("PRESSSSSSSS");
        Application.Quit();
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
