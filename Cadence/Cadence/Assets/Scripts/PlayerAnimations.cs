using Controller;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private LayerMask _groundMask;

    private const string PLAYERIDLE = "PlayerIdle_";
    private const string PLAYERRUN = "PlayerRun_";
    private const string PLAYERHURT = "PlayerHurt_";
    private const string PLAYERFALL = "PlayerFall_";
    private const string PLAYERJUMP = "PlayerJump_";

    private string currentState;
    private char currentHealth = 'H';
    private bool facingRight;

    private IPlayerController _player;
    private Vector2 movement;
    void Awake() => _player = GetComponentInParent<IPlayerController>();

    // Update is called once per frame
    void Update()
    {
        if (_player == null) return;
        if ((facingRight && _player.Input.X > 0) || (!facingRight && _player.Input.X < 0))
        {
            facingRight=!facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
        movement = _player.RawMovement;


        if (_player.Grounded)
        {
            if (Mathf.Abs(movement.x) > 0.1)
            {
                ChangeAnimationState(PLAYERRUN);
            }
            else
            {
                ChangeAnimationState(PLAYERIDLE);
            }
        }
        else
        {
            if (movement.y > 0)
            {
                ChangeAnimationState(PLAYERJUMP);
            }
            else
            {
                ChangeAnimationState(PLAYERFALL);
            }
        }
    }
    void ChangeAnimationState(string newState)
    {
        if (newState == currentState) return;
        _anim.Play(newState + currentHealth);
        currentState = newState;

    }
}
