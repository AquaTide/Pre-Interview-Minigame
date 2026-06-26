using System;
using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCombatInfo : MonoBehaviour
{
    [SerializeField][ReadOnly] private CharCombatHandler _charCombatHandler;
    [SerializeField][AutoProperty(editable:true)] private TMP_Text DisplayName;
    [SerializeField][AutoProperty(editable:true)] private Slider HealthSlider;
    
    public void SetCharToInterface(CharCombatHandler combatHandler)
    {
        _charCombatHandler = combatHandler;
        DisplayName.text = _charCombatHandler.partyCharacter.characterName;
        UpdateHealthBar();
        
        _charCombatHandler.OnTakingDamage += UpdateHealthBar;
    }

    public void UpdateHealthBar()
    {
        HealthSlider.maxValue = _charCombatHandler.partyCharacter.baseHealth;
        HealthSlider.value = _charCombatHandler.CurrentHealth;
    }
}
