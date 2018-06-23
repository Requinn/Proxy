using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour{
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

	// Update is called once per frame
	void Update (){
	    _timeSinceLastMovement += Time.deltaTime;
	    if (!_isEngaged){
	        if (_timeSinceLastMovement >= speed){
	            _timeSinceLastMovement = 0f;
	            //towers don't move, nor do we move while engaged
	            if (Type != UnitType.Tower){
	                movement.MoveForward(controlledPlayer);
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
        List<Unit> enemies = TileHandler.Instance.FetchAllFromColumn(assignedColumn, controlledPlayer);
        foreach (var e in enemies){
            if (Vector3.SqrMagnitude(e.transform.position - transform.position) < attack.range * attack.range){
                attack.Attack(e, Type);
                _isEngaged = true;
                return;
            }
        }
    }
}
