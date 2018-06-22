using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ability : MonoBehaviour {

    [HideInInspector]
    public PlayerController pc;
    [HideInInspector]
    public PlayerShooter ps;


    //key to activate
    public KeyCode rapidFireKey = KeyCode.Q;

    //Cooldown
    public float coolDownDuration = 4f;
    float nextReadyTime;
    float coolDownTimeLeft;

    //UI
    public Image darkMask;
    public TextMeshProUGUI coolDownText;

    void Start() {
        pc = GetComponent<PlayerController>();
        ps = GetComponent<PlayerShooter>();
    }

    void Update () {
        bool coolDownComplete = (Time.time > nextReadyTime);
        if (coolDownComplete) {
            coolDownText.enabled = false;
            darkMask.enabled = false;
            if (Input.GetKeyDown(rapidFireKey)) {
                AbilityTriggered();
            }
        } else {
            CoolDown();
        }
    }
    private void CoolDown() {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(coolDownTimeLeft);
        coolDownText.text = roundedCd.ToString();
        darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
    }

    public virtual void AbilityTriggered() {
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;
        darkMask.enabled = true;
        coolDownText.enabled = true;
    }
}
