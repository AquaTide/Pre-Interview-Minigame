using System;
using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
    private PlayerControl _playerControl;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _playerControl = GetComponentInParent<PlayerControl>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if(Mathf.Abs(_playerControl.MoveDirX) > 0.1f || MathF.Abs(_playerControl.MoveDirY) > 0.1f)
            _animator.SetBool("isMoving", true);
        else
            _animator.SetBool("isMoving", false);

        _spriteRenderer.flipX = _playerControl.MoveDirX switch
        {
            > 0 => false,
            < 0 => true,
            _ => _spriteRenderer.flipX
        };
    }
    
    
}
