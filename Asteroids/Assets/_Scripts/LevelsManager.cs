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

    //I created this to load the scene after a certain delay, so that the timeline animatian would play
    public void DelayFadeToScene(float delay) {
        StartCoroutine(FadeAfterTime(delay));
    }
    IEnumerator FadeAfterTime(float delay) {
        yield return new WaitForSeconds(delay);
        FadeToScene(1);
    }

    public void LoadScene() {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Quit() {
        Application.Quit();
    }
}
