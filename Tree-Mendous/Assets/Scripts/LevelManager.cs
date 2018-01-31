using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public GameObject LevelSelect;

    public void StartBtn(string GameLevel)
    {
        LevelSelect.gameObject.SetActive(true);
    } 
    public void ExitBtn()
    {
        Application.Quit();
    }
    public void LevelBtn(string GameLevel)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(GameLevel);
    }
}
