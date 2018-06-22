using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFireAbility : Ability {

    public float newFireRateduration = 3;
    public float newFireRate = 0.2f;
    float originalFireRate;

    public override void AbilityTriggered() {
        StartCoroutine(RapidFire());
        base.AbilityTriggered();
    }
    public IEnumerator RapidFire() {
        originalFireRate = ps.fireRate;
        ps.fireRate = newFireRate;
        yield return new WaitForSeconds(newFireRateduration);
        ps.fireRate = originalFireRate;
    }
}
