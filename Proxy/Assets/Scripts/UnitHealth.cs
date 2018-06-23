using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UnitHealth : NetworkBehaviour{
    public int maxHealth;
    public Image healthBar;
    private Unit _owningUnit;

    [SyncVar(hook = "UpdateHealthBar")]
    public int currentHealth;

    void Start(){
        currentHealth = maxHealth;
        _owningUnit = GetComponent<Unit>();
    }

    public void TakeDamage(int amount, Unit.UnitType sourceType){
        //calculate weaknesses
        if(_owningUnit.Type == Unit.UnitType.Infantry && 
            (sourceType == Unit.UnitType.Armored || sourceType == Unit.UnitType.Air)){
            currentHealth -= amount * 2;
        }
        else if (_owningUnit.Type == Unit.UnitType.Armored &&
            (sourceType == Unit.UnitType.Air || sourceType == Unit.UnitType.Tower)){
            currentHealth -= amount * 2;
        }
        else if (_owningUnit.Type == Unit.UnitType.Air &&
            (sourceType == Unit.UnitType.Tower || sourceType == Unit.UnitType.Infantry)){
            currentHealth -= amount * 2;
        }
        else if (_owningUnit.Type == Unit.UnitType.Tower &&
                 (sourceType == Unit.UnitType.Infantry || sourceType == Unit.UnitType.Armored)){
            currentHealth -= amount * 2;
        }
        else{
            currentHealth -= amount;
        }
        
        if (currentHealth <= 0){
            currentHealth = 0;
            _owningUnit.CmdDie();
        }
    }
    void UpdateHealthBar(int hp){
        healthBar.fillAmount = (float) hp / maxHealth;
    }

}
