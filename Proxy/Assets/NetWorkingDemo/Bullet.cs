using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    void OnCollisionEnter(Collision c){
        var health = c.gameObject.GetComponent<Health>();
        if (health){
            health.TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
