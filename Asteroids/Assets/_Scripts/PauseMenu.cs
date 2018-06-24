using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject continueButton;

    bool ispaused;

    public Ease ease = Ease.Linear;
    bool isTweening;
    // Use this for initialization
    void Update () {
        if (Input.GetButtonDown("Cancel")) {
            if (ispaused) {
                UnpauseGame();
            } else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        if (!ispaused) {
            if (!isTweening)
                StartCoroutine(Pause());
        }
    }
    IEnumerator Pause() {
        isTweening = true;
        pauseMenu.SetActive(true);
        pauseMenu.transform.localScale = Vector3.zero;
        Tween myTween = pauseMenu.transform.DOScale(1, 0.3f).SetEase(ease);
        yield return myTween.WaitForCompletion();
        ispaused = true;
        isTweening = false;
        Time.timeScale = 0;
    }

    public void UnpauseGame() {
        if (ispaused) {
            if (!isTweening)
                StartCoroutine(Unpause());
        }
    }
    IEnumerator Unpause() {
        Time.timeScale = 1;
        isTweening = true;
        Tween myTween = pauseMenu.transform.DOScale(0, 0.3f).SetEase(ease);
        yield return myTween.WaitForCompletion();
        pauseMenu.SetActive(false);
        ispaused = false;
        isTweening = false;
    }

}
