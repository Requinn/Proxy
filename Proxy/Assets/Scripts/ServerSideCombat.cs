using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerSideCombat : NetworkBehaviour{
    void Start(){
        InvokeRepeating("CheckForAttacks", 0.5f, 0.5f);
    }

    public void CheckForAttacks(){
        foreach (var player in Network.connections){
        }
    }
}
