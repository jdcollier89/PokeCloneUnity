using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI maxHP;
    public TextMeshProUGUI currentHP;

    public Slider hpSlider;

    public void SetHUD(Unit unit){
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        maxHP.text = unit.maxHP.ToString();
        currentHP.text = unit.currentHP.ToString();

        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void SetHP(int hp){
        if (hp<0){
            hp = 0;
        }
        hpSlider.value = hp;
        currentHP.text = hp.ToString();
    }
}
