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
    private const string PLAYERLANDING = "PlayerLanding_";
    private const string PLAYERLIFTOFF = "PlayerLiftoff_";
    private const string PLAYERFLOAT = "PlayerFloat_";

    private string currentState;
    private char currentHealth = 'H';

    private IPlayerController _player;
    private Vector2 movement;
    void Awake() => _player = GetComponentInParent<IPlayerController>();

    // Update is called once per frame
    void Update()
    {
        if (_player == null) return;
        if (_player.Input.X != 0) transform.localScale = new Vector3(_player.Input.X > 0 ? 1 : -1, 1, 1);
        movement = _player.RawMovement;

        if (currentState == PLAYERFLOAT && _player.Grounded)
        {
            ChangeAnimationState(PLAYERLANDING);
        }
        else if ((currentState == PLAYERRUN || currentState == PLAYERIDLE) && !_player.Grounded)
        {
            ChangeAnimationState(PLAYERLIFTOFF);
        }
        else if (Mathf.Abs(movement.x) < 0.1 && _player.Grounded)
        {
            ChangeAnimationState(PLAYERIDLE);
        }
        else if (_player.Grounded)
        {
            ChangeAnimationState(PLAYERRUN);
        }
        else if (!_player.Grounded)
        {
            ChangeAnimationState(PLAYERFLOAT);
        }

    }

    bool IsAnimationFinished()
    {
        AnimatorStateInfo stateInfo = _anim.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime >= 1.0f;
    }

    void ChangeAnimationState(string newState)
    {
        if (newState == currentState) return;
        if (!IsAnimationFinished() && (currentState == PLAYERLIFTOFF || currentState == PLAYERLANDING)) return;
        _anim.Play(newState + currentHealth);
        currentState = newState;

    }
}
