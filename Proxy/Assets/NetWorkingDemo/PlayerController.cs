using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

	// Use this for initialization
	void Start () {
		
	}

    public override void OnStartLocalPlayer(){
        GetComponent<Renderer>().material.color = Color.blue;
    }

	// Update is called once per frame
	void Update (){
        //local client control only
	    if (!isLocalPlayer){
	        return;
	    }
	    if (Input.GetKeyDown(KeyCode.Space)){
	        CmdFire();
	    }
	    var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
	    var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

	    transform.Rotate(0, x, 0);
	    transform.Translate(0, 0, z);
	}

    //Commands are called the clients, but executed on the server
    //sends a command to the server
    [Command]
    void CmdFire(){
        var bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6f;
        NetworkServer.Spawn(bullet); //create a bullet on the server, which will create a bullet on all clients
        Destroy(bullet, 2f);
    }
}
