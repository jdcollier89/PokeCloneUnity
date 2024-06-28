using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattlestation;
    public Transform enemyBattlestation; 

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public TextMeshProUGUI dialogueText;

    Unit playerUnit;
    Unit enemyUnit;


    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }


    IEnumerator SetupBattle(){
        GameObject playerGO = Instantiate(playerPrefab, playerBattlestation);
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattlestation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " appears...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack(){
        // Damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = playerUnit.unitName + " hits for " + playerUnit.damage + " damage.";

        yield return new WaitForSeconds(2f);

        // Change state based on what happened
        if(isDead){
            state = BattleState.WON;
            EndBattle();
        }
        else {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal(){
        dialogueText.text = playerUnit.unitName + " heals for 5 health.";
        yield return new WaitForSeconds(1f);
        playerUnit.Heal(5);
        playerHUD.SetHP(playerUnit.currentHP);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn(){
        dialogueText.text = enemyUnit.unitName + " attacks!";
        yield return new WaitForSeconds(1f);
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);
        if (isDead){
            state = BattleState.LOST;
            EndBattle();
        }
        else {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle(){
        if(state == BattleState.WON){
            dialogueText.text = "You have won the battle!";
        }
        else if (state == BattleState.LOST){
            dialogueText.text = "You were defeated";
        }
    }

    void PlayerTurn(){
        dialogueText.text = "Chose an action:";
    }

    public void OnAttackButton(){
        if (state != BattleState.PLAYERTURN)
        {return;}

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton(){
        if (state != BattleState.PLAYERTURN)
        {return;}

        StartCoroutine(PlayerHeal());
    }

}
