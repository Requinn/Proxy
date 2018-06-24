using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnitMovement : NetworkBehaviour {

    //move forward
    public void MoveForward(){
        transform.position += transform.forward;
    }
}
