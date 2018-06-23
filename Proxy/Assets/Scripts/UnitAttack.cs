using System.Collections;
using System.Collections.Generic;
using MovementEffects;
using UnityEngine;

public class UnitAttack : MonoBehaviour{
    public float range = 2f;
    public int damage = 2;
    public float attackSpeed;
    private bool _canAttack;

    public void Attack(Unit target, Unit.UnitType sourceType) {
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
