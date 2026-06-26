using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatInfoManager : MonoBehaviour
{
    [SerializeField] private GameObject HealthInfoObject;
    private List<CharacterCombatInfo> CharHealthInterfaces = new();

    private void OnEnable()
    {
        var bm = BattleManager.Instance;
    }

    private void Start()
    {
        var bm = BattleManager.Instance;
        var partyChar = bm.PlayerPartyCombatManager.CharCombatHandlers;

        foreach (var c in partyChar)
        {
            var o = Instantiate(HealthInfoObject, transform.position, Quaternion.identity, transform);
            var hi =  o.GetComponent<CharacterCombatInfo>();
            
            hi.SetCharToInterface(c);
            CharHealthInterfaces.Add(hi);
        }
        
        UpdateInterface();
    }

    public void UpdateInterface()
    {
        foreach (var hi in CharHealthInterfaces)
        {
            hi.UpdateHealthBar();
        }
    }
}