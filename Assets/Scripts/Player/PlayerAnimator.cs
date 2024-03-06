 using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private SpriteRenderer _sprite;

    private readonly int _idMove = Animator.StringToHash("Move");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _sprite = GetComponent<SpriteRenderer>(); 
    }

    private void Update()
    {
        ChangeAnimationState();
    }

    void ChangeAnimationState()
    {
        if (_playerMovement.moveDir != Vector2.zero)
        {
            _animator.SetBool(_idMove,true);
            
            SpriteDirectionChecker();
        }
        else
        {
            _animator.SetBool(_idMove, false);
        }
    }

    void SpriteDirectionChecker()
    {
        if (_playerMovement.moveDir.x < 0)
        {
            _sprite.flipX = true;
        }
        else if (_playerMovement.moveDir.x > 0)
        {
            _sprite.flipX = false;
        }
    }
}
