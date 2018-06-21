using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour {

    public int levelToLoad;

    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void FadeToScene(int level) {
        //if game paused unpause it
        Time.timeScale = 1;
        levelToLoad = level;
        anim.Play("FadeOut");
    }

    public void LoadScene() {
        SceneManager.LoadScene(levelToLoad);
    }
}
