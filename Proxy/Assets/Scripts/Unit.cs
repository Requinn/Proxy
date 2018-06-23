using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Unit : NetworkBehaviour{
    public int ID;
    public enum UnitType{
        Infantry,
        Armored,
        Air,
        Tower
    }
    public UnitType Type;
    public UnitHealth health;
    public UnitMovement movement;
    public UnitAttack attack;

    [Range(-1,1)]
    public int controlledPlayer;
    public int assignedColumn;

    //How often do we act? lower is faster
    public float speed = 1f;
    private float _timeSinceLastMovement = 0f;

    private bool _isEngaged; // are we fighting a target

    public void SetEngaged(bool b){
        _isEngaged = b;
    }

    public override void OnStartLocalPlayer(){
        GetComponent<Renderer>().material.color = Color.blue;
    }

    // Update is called once per frame
	void Update (){
	    _timeSinceLastMovement += Time.deltaTime;
	    if (!_isEngaged){
	        if (_timeSinceLastMovement >= speed){
	            _timeSinceLastMovement = 0f;
	            //towers don't move, nor do we move while engaged
	            if (Type != UnitType.Tower){
	                movement.MoveForward();
	            }
	        }
	    }

	    CheckForAttack();
	}

    /// <summary>
    /// check a distance in front from the list of enemies in the same column
    /// </summary>
    private void CheckForAttack(){
        _isEngaged = false;
        TileHandler.Instance.CmdCheckForAttacks();
    }

    public void Die(){
        TileHandler.Instance.CmdRemoveFromColumn(ID, assignedColumn);
        Destroy(gameObject, 1f);
    }
}
