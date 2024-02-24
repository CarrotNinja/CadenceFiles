using Controller;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _anim;
   

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
    private Flute weapon;

    Transform parent; 
    void Awake()
    {
        _player = GetComponentInParent<IPlayerController>();
        parent = GetComponent<Transform>();
        weapon = GetComponent<Flute>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null) return;
        

        if (_player.HurtThisFrame)
        {
            ChangeAnimationState(PLAYERHURT);
        }
        else
        {
            if ((facingRight && _player.Input.X > 0) || (!facingRight && _player.Input.X < 0))
            {
                facingRight = !facingRight;
                transform.Rotate(0f, 180f, 0f);
            }
            movement = _player.RawMovement;
            if (_player.Grounded)
            {
                if (Mathf.Abs(movement.x) > 0.1)
                {
                    ChangeAnimationState(PLAYERRUN);
                    weapon.flute.gameObject.transform.localPosition = new Vector3(0.65f, 0.24f, -1.08f);
                }
                else
                {
                    ChangeAnimationState(PLAYERIDLE);
                    weapon.flute.gameObject.transform.localPosition = new Vector3(0.279f, 0.366f, -1.08f);
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
        
    }
    void ChangeAnimationState(string newState)
    {
        if (newState == currentState) return;
        _anim.Play(newState + currentHealth);
        currentState = newState;

    }
}
