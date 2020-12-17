using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD: MonoBehaviour
{
    
    public Text nameText;
    public Image healthbar;
    
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        
        healthbar.fillAmount = unit.currentHP / unit.maxHP;
    }

    public void SetHP(Unit unit)
    {
        healthbar.fillAmount = unit.currentHP / unit.maxHP;    
    }
}
