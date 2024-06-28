using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;

    public bool TakeDamage(int enemyDmg){
        currentHP -= enemyDmg;

        if(currentHP <= 0){
            return true;
        }
        else{
            return false;
        }
    }

    public void Heal(int amt){
        currentHP += amt;
        if(currentHP > maxHP){
            currentHP = maxHP;
        }
    }
}
