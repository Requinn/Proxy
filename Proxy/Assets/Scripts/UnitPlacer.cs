using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnitPlacer : NetworkBehaviour{
    public List<TileObject> validTiles = new List<TileObject>();
    private NetworkInstanceId id;
    private NetworkStartPosition[] spawns;
    private Camera cam;

    void Start(){
        int tileID = 0;
        id = netId;
        foreach (var v in validTiles){
            v.ID = tileID++;
            v.owningPlayer = (int)id.Value;
            v.Confirm += CreateUnitAtTile;
        }
        cam = GetComponentInChildren<Camera>();
        if (!isLocalPlayer){
            cam.enabled = false;
            Camera.main.enabled = false;
        }
    }

    private void CreateUnitAtTile(int tileid, int ownedBy){
        Debug.Log(tileid + " " + ownedBy);
    }
}
