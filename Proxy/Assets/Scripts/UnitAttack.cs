using System.Collections;
using System.Collections.Generic;
using MovementEffects;
using UnityEngine;
using UnityEngine.Networking;

public class UnitAttack : NetworkBehaviour {
    public float range = 2f;
    public int damage = 2;
    public float attackSpeed;
    private bool _canAttack = true;

    [ClientRpc]
    public void RpcAttack(Unit target, Unit.UnitType sourceType) {
        if (_canAttack){
            target.health.TakeDamage(damage, sourceType);
            Timing.RunCoroutine(AttackDelay());
        }
    }

    private IEnumerator<float> AttackDelay(){
        _canAttack = false;
        yield return Timing.WaitForSeconds(attackSpeed);
        _canAttack = true;
    }
}
