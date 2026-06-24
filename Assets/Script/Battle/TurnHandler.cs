using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;

public interface ITurnParticipant
{
    string Name { get; }
    int Speed { get; }
    void TakeTurn();
}

[Serializable]
public class TurnHandler
{
    public List<ITurnParticipant> TurnOrder = new();
    public ITurnParticipant currentTurn;
    public int CurrentRound = 0;

    public void AddEnemies(EnemyBattleManager data)
    {
        TurnOrder.Add(data);
        ShowToConsole();
    }
    public void AddPartyCharacter(CharBattleHandler data)
    {
        TurnOrder.Add(data);
        ShowToConsole();
    }
    [SerializeField] private List<string> orderDebug = new();

    public void SortOrder()
    {
        TurnOrder = TurnOrder.OrderByDescending(t => t.Speed).ToList();
        ShowToConsole();
    }

    public void NextTurn()
    {
        currentTurn = TurnOrder[0];
        currentTurn.TakeTurn();
        TurnOrder.RemoveAt(0);
    }

    private void ShowToConsole()
    {
        orderDebug.Clear();
        orderDebug = TurnOrder.Select(t => t.Name).ToList();
    }

    public void RemoveFromTurnOrder(ITurnParticipant participant)
    {
        TurnOrder.Remove(participant);
    }
}