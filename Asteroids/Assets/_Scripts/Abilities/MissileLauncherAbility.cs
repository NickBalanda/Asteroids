using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherAbility : Ability {

    public GameObject missilePrefab;
    public float missileSpeed;

    public override void AbilityTriggered() {
        GameObject missile = Instantiate(missilePrefab, ps.transform.position, ps.transform.rotation);
        missile.GetComponent<Rigidbody>().velocity = missile.transform.forward * missileSpeed;
        base.AbilityTriggered();
    }
}
