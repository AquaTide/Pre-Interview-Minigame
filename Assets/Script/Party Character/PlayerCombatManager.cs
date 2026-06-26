using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    public List<CharCombatHandler> CharCombatHandlers { get; private set; }

    private void OnEnable()
    {
        CharCombatHandlers = new List<CharCombatHandler>(GetComponentsInChildren<CharCombatHandler>());
    }

    public CharCombatHandler GetRandomChar()
    {
        return CharCombatHandlers[0];
    }
}