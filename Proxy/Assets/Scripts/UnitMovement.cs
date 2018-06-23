using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnitMovement : NetworkBehaviour {

    //move forward
    public void MoveForward(int player){
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + player);
    }
}
