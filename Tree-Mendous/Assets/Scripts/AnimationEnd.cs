using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEnd : MonoBehaviour {

    public string NextScene;

    void LoadLevel()
    {
        SceneManager.LoadScene(NextScene);
    }
}
