using System;
using UnityEngine;

public class BattleAnimationFeedback : MonoBehaviour
{
    public Action OnAttackEnd;

    public void AttackAnimEnd()
    {
        OnAttackEnd?.Invoke();
    }
}