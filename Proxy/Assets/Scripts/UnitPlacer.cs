using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class UnitPlacer : NetworkBehaviour{
    public List<TileObject> validTiles = new List<TileObject>();
    private TileHandler tiles;
    private NetworkInstanceId id;
    private NetworkStartPosition[] spawns;
    private Camera cam;

    void Start(){
        tiles = GetComponent<TileHandler>();
        int tileID = 0;
        int columnID = 0;
        id = netId;
        foreach (var v in validTiles){
            v.ID = tileID++;
            v.HeldColumn = columnID++;
            if (columnID > 5){
                columnID = 0;
            }
            v.owningPlayer = (int)id.Value;
            v.Confirm += CreateUnitAtTile;
            v.Selected += ToggleTile;
        }
        cam = GetComponentInChildren<Camera>();
        if (!isLocalPlayer){
            cam.enabled = false;
            cam.GetComponent<AudioListener>().enabled = false;
        }
    }

    private void ToggleTile(TileObject t, int direction){
        if (!isLocalPlayer){
            return;
        }
        t.highlightMat.Toggle(direction);
    }

    private void CreateUnitAtTile(TileObject t, int ownedBy){
        if (!isLocalPlayer){
            return;
        }
        if (t.transform.rotation.eulerAngles.y == 180f || t.transform.rotation.eulerAngles.y == -180){ //eulers are important
            tiles.CmdCreateAndAddToColumn(t.HeldColumn, Unit.UnitType.Infantry,
                new Vector3(t.transform.position.x - 1f, t.transform.position.y + 1.5f, t.transform.position.z + 1f),
                t.transform.rotation);
        }
        else{
            tiles.CmdCreateAndAddToColumn(t.HeldColumn, Unit.UnitType.Infantry,
                new Vector3(t.transform.position.x + 1f, t.transform.position.y + 1.5f, t.transform.position.z - 1f),
                t.transform.rotation);
        }
    }

}
