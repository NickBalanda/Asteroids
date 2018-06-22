using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : Ability {

    public GameObject shieldObject;
    public float shieldTime = 2;
    float shieldSize = 2.5f;

    void Start() {
        shieldSize = shieldObject.transform.localScale.x;
    }

    public override void AbilityTriggered() {
        StartCoroutine(Shield());
        base.AbilityTriggered();
    }

    public IEnumerator Shield() {
        shieldObject.SetActive(true);
        shieldObject.GetComponent<Renderer>().material.SetFloat("_Level", 0);
        shieldObject.transform.localScale = Vector3.zero;
        shieldObject.transform.DOScale(shieldSize, 0.2f).SetEase(Ease.InOutElastic);
        yield return new WaitForSeconds(shieldTime);
        float level = 0;
        while (level < 1) {
            level = level + 0.5f * Time.deltaTime;
            shieldObject.GetComponent<Renderer>().material.SetFloat("_Level", level);
            yield return null;
        }
        shieldObject.SetActive(false);
    }
}
