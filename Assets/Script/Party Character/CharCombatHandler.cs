using System;
using Naninovel.UI;
using Unity.VisualScripting;
using UnityEngine;

public class CharCombatHandler : MonoBehaviour, ITurnParticipant, ITakingDamage, IAilment
{
    public PartyCharacter partyCharacter;
    public string Name => gameObject.name;
    public int CurrentHealth { get; private set; }
    public int Speed => SpeedModifier();
    public int Attack => AttackModifier();

    public Ailment ActiveAilment { get; private set; }
    public Stance Stance { get; private set; }
    public void SetStance(Stance stance) => Stance = stance;

    public Action OnTakingDamage;
    public Action AttackPerformed;
    public Action OnBlocked;
    public Action OnTakeTurn;
    public Action OnPoisonOccur;
    public Action OnStunOccur;
    public Action OnParalyzeOccur;
    public Action OnSleepOccur;
    public Action OnDeadOccur;

    private void OnEnable()
    {
        InstantiateCharacter();
    }

    private void InstantiateCharacter()
    {
        Instantiate(partyCharacter.characterObject, transform.position, transform.rotation, transform);
        CurrentHealth = partyCharacter.baseHealth;
        RegisterParticipant();
    }

    public void TakingDamage(int damage)
    {
        if (Stance == Stance.Defending)
        {
            OnBlocked?.Invoke();
            return;
        }

        CurrentHealth -= damage;
        OnTakingDamage?.Invoke();
        if (CurrentHealth > 0) return;
        SetActiveAilment(Ailment.Dead);
        DeadOccur();
    }

    #region StatsModifiers

    private int SpeedModifier()
    {
        var modified = partyCharacter.baseSpeed;
        return modified;
    }

    private int AttackModifier()
    {
        var modified = partyCharacter.baseAttack;
        return modified;
    }

    #endregion

    #region ManageTurn

    public void RegisterParticipant()
    {
        var bm = BattleManager.Instance;
        bm.TurnHandler.RegisterParticipant(this);
    }

    public void RemoveOnDeath()
    {
        if ((ActiveAilment & Ailment.Dead) == 0) return;
        var bm = BattleManager.Instance;
        bm.TurnHandler.RemoveFromTurnOrder(this);
    }

    public void TakeTurn()
    {
        if ((ActiveAilment & Ailment.Dead) != 0)
        {
            EndTurn();
            return;
        }

        OnTakeTurn?.Invoke();
    }

    public void EndTurn() => BattleManager.Instance.TurnHandler.NextTurn();

    #endregion

    #region Ailment

    public void SetActiveAilment(Ailment ailment) => ActiveAilment |= ailment;

    public void CureAilment(Ailment ailment) => ActiveAilment &= ~ailment;

    public void CureAllAilments() => ActiveAilment = 0;

    public void PoisonOccur() => OnPoisonOccur?.Invoke();

    public void StunOccur() => OnStunOccur?.Invoke();

    public void ParalyzeOccur() => OnParalyzeOccur?.Invoke();

    public void SleepOccur() => OnSleepOccur?.Invoke();

    public void DeadOccur()
    {
        var bm = BattleManager.Instance;
        bm.TurnHandler.RemoveFromTurnOrder(this);
        OnDeadOccur?.Invoke();
    }

    #endregion
}