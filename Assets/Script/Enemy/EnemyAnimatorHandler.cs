using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimatorHandler : MonoBehaviour
{
    private Animator _animator;
    private EnemyCombatManager _combatManager;
    
    public UnityEvent OnDeathEvent;
    public UnityEvent OnAttackEvent;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _combatManager = GetComponentInParent<EnemyCombatManager>();
    }

    private void OnEnable()
    {
        _combatManager.OnAttack += OnAttack;
        _combatManager.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        OnDeathEvent?.Invoke();
    }

    private void OnAttack()
    {
        OnAttackEvent?.Invoke();
    }
}
