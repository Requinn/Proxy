using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour{
    public GameObject enemyPrefab;
    public int numberofEnemies;

    public override void OnStartServer(){
        for (int i = 0; i < numberofEnemies; i++){
            var spawnPos = new Vector3(Random.Range(-8f, 8f),0f, Random.Range(-8f, 8f));
            var spawnRot = Quaternion.Euler(0f, Random.Range(0, 180), 0f);

            var enemy = Instantiate(enemyPrefab, spawnPos, spawnRot);
            NetworkServer.Spawn(enemy);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
