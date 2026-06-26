using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ITurnParticipant
{
    string Name { get; }
    int Speed { get; }
    void RegisterParticipant();
    void RemoveOnDeath();
    void TakeTurn();
}

[Serializable]
public class TurnHandler
{
    public List<ITurnParticipant> Participants = new();
    public List<ITurnParticipant> TurnOrder = new();
    public ITurnParticipant currentTurn;
    public int CurrentRound = 0;
    public bool EndBattle => BattleManager.Instance.CheckBattleValidation;

    public void AddEnemies(EnemyCombatManager data)
    {
        TurnOrder.Add(data);
    }

    public void AddPartyCharacter(CharCombatHandler data)
    {
        TurnOrder.Add(data);
    }

    [SerializeField] private List<string> orderDebug = new();

    public void SortOrder()
    {
        TurnOrder = TurnOrder.OrderByDescending(t => t.Speed).ToList();
    }

    public void RegisterParticipant(ITurnParticipant participant)
    {
        Participants.Add(participant);
    }

    public void StartRound()
    {
        CurrentRound = 0;
    }

    public void NextRound()
    {
        TurnOrder = new List<ITurnParticipant>(Participants);

        foreach (var p in Participants)
        {
            p.RemoveOnDeath();
        }

        SortOrder();
        CurrentRound++;
        NextTurn();
    }

    public void NextTurn()
    {
        if (TurnOrder.Count == 0)
        {
            NextRound();
            return;
        }

        if (EndBattle)
        {
            var bm = BattleManager.Instance;
            BattleManager.BattleEnd();
            return;
        }

        currentTurn = TurnOrder[0];
        currentTurn.TakeTurn();
        TurnOrder.RemoveAt(0);
        ShowToConsole();
    }

    public void RemoveFromTurnOrder(ITurnParticipant p)
    {
        TurnOrder.Remove(p);
        ShowToConsole();
    }

    private void ShowToConsole()
    {
        orderDebug.Clear();
        orderDebug = TurnOrder.Select(t => t.Name).ToList();
    }
}