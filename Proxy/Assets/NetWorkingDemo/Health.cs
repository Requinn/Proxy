using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour{
    public const int maxHP = 100;
    public bool destroyOnDeath;

    private NetworkStartPosition[] spawns;

    //sync health for all clients
    //syncvar(hook) will invoke the linked functions when this variable is changed
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHP = maxHP;

    public RectTransform healthbar;

    void Start(){
        if (isLocalPlayer){
            spawns = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    public void TakeDamage(int damage){
        //damage is done server side
        if (!isServer){
            return;
        }
        currentHP -= damage;
        if (currentHP <= 0){
            if (destroyOnDeath){
                Destroy(gameObject);
            }
            else{
                currentHP = maxHP;
                RpcRespawn();
            }
        }
    }

    void OnChangeHealth(int currentHealth){
        healthbar.sizeDelta = new Vector2(currentHealth, healthbar.sizeDelta.y);
    }

    //RPCs are called on the server, but executed on the clients
    //RPC from client to relocate to the zero location and refill health
    [ClientRpc]
    void RpcRespawn(){
        if (isLocalPlayer){
            //null case
            Vector3 spawnPt = Vector3.zero;
            //seek a random spawn point from available ones
            if (spawns != null && spawns.Length > 0){
                spawnPt = spawns[Random.Range(0, spawns.Length)].transform.position;
            }
            //assign our new location the assigned point
            transform.position = spawnPt;
        }
    }
}
