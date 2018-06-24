using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OptionsMenuManager : MonoBehaviour {

    Animator anim;
	
	void Start () {
        anim = GetComponent<Animator>();
	}

    public void OpenOptionsMenu() {
        anim.Play("OptionsMenuIn");
    }

    public void CloseOptionsMenu() {
        anim.Play("OptionsMenuOut");
    }
}
