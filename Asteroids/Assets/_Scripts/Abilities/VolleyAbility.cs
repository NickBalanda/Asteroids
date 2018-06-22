using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolleyAbility : Ability {
    
    public int numberOfBullets;

    public GameObject volleyBullet;

    public float radius;
    public float moveSpeed;

    Vector3 startPoint;

    public override void AbilityTriggered() {
        Volley(numberOfBullets);
        base.AbilityTriggered();
    }


    public void Volley(int numberOfProjectiles) {
        startPoint = transform.position;
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numberOfProjectiles - 1; i++) {

            float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirZposition = startPoint.z + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileDirXposition,0, projectileDirZposition);
            Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

            //Pool volley bullets
            GameObject bullet = ObjectPooler.instance.GetPooledObject("VolleyBullet");
            if (bullet != null) {
                bullet.transform.position = startPoint;
                bullet.transform.rotation = Quaternion.identity;
                bullet.GetComponent<Rigidbody>().velocity = new Vector3(projectileMoveDirection.x, 0, projectileMoveDirection.z) * moveSpeed;
                bullet.SetActive(true);
            }

            angle += angleStep;
        }
    }
}
