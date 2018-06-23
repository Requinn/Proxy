using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour {

    //move forward
    public void MoveForward(int player){
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + player);
    }
}
