using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int numberOfAsteroidsSpawned = 4;

    public static GameManager instance;
    void Awake() {
        instance = this;
    }

    void Start () {
        //Wait to spawn after the pooler Instantiates
        Invoke("SpawnAsteroids", 1);
        //Check every few seconds if new asteroids shoud be spawned
        InvokeRepeating("CheckIfAllAsteroidsDestroyed", 2 , 2);
        InvokeRepeating("SpawnUFO", 8, Random.Range(8,12));
    }

    void SpawnAsteroids() {
        for (int i = 0; i < numberOfAsteroidsSpawned; i++) {
            GameObject newAsteroid = ObjectPooler.instance.GetPooledObject("AsteroidLarge");
            if (newAsteroid != null) {
                newAsteroid.transform.position = GetRandomEdgePosition();
                newAsteroid.SetActive(true);
            }
        }
    }
    void SpawnUFO() {
        GameObject ufo = ObjectPooler.instance.GetPooledObject("UFO");
        if (ufo != null) {
            ufo.transform.position = GetRandomEdgePosition();
            ufo.SetActive(true);
        }
    }

    //Returns a random Edge position
    Vector3 GetRandomEdgePosition() {
        int ran = Random.Range(0, 4);

        if (ran == 0) {
            return new Vector3(-18, 0, Random.Range(0, 12));
        } else if (ran == 1) {
            return new Vector3(18, 0, Random.Range(0, 12));
        } else if (ran == 2) {
            return new Vector3(Random.Range(0, 18), 0, 12);
        } else {
            return new Vector3(Random.Range(0, 18), 0, -12);
        }
    }

    //Check if all asteroids were destroyed, if so spawn new ones +1
    public void CheckIfAllAsteroidsDestroyed() {
        bool allInactive = true;
        foreach (var asteroid in ObjectPooler.instance.pooledObjects) {
            if (asteroid.tag == "AsteroidLarge" || asteroid.tag == "AsteroidMedium" || asteroid.tag == "AsteroidSmall") {
                if (asteroid.activeInHierarchy) {
                    allInactive = false;
                    break;
                }
            }
        }
        if (allInactive) {
            numberOfAsteroidsSpawned++;
            SpawnAsteroids();
        }
    }

    public void GameOver() {
        CancelInvoke();

    }
}
