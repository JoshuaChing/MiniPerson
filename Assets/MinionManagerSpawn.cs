﻿using UnityEngine;
using System.Collections;

public class MinionManagerSpawn : MonoBehaviour {

    public GameObject[] greenMinions;
    public int greenMinionsCount;
    public Vector3 greenMinionsSpawnPoint;
    private int greenMinionsMax = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ManageGreenMinions();
	}

    void ManageGreenMinions()
    {
        greenMinions = GameObject.FindGameObjectsWithTag("Green Minion");
        greenMinionsCount = greenMinions.Length;

        if (greenMinionsCount < greenMinionsMax) {
            InvokeRepeating("SpawnMinion", 3f, 10f);
        }
    }

    void SpawnMinion()
    {
        greenMinionsSpawnPoint.x = 0;
        greenMinionsSpawnPoint.y = 3;
        greenMinionsSpawnPoint.z = 0;

        Instantiate(greenMinions[0], greenMinionsSpawnPoint, Quaternion.identity);
        CancelInvoke();
    }
}
