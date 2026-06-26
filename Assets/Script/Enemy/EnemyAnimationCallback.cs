using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimationCallback : MonoBehaviour
{
    private EnemyCombatManager _combatManager;
    public UnityEvent OnAttackEnd;
    public Action DealDamageAction;
    private void Awake()
    {
        _combatManager = GetComponentInParent<EnemyCombatManager>();
    }

    public void OnAttackAnimationEnd()
    {
        EnemyCombatManager.EndTurn();
        OnAttackEnd?.Invoke();
    }

    public void DealDamage()
    {
        DealDamageAction?.Invoke();
    }
}
