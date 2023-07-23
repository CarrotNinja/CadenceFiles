using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private LayerMask _groundMask;

    private IPlayerController _player;
    void Awake() => _player = GetComponentInParent<IPlayerController>();

    // Update is called once per frame
    void Update()
    {
        if (_player == null) return;
        if (_player.Input.X != 0) transform.localScale = new Vector3(_player.Input.X > 0 ? 1 : -1, 1, 1);

        _anim.SetBool("Jumping", _player.JumpingThisFrame);
        _anim.SetFloat("VelocityX", Mathf.Abs(_player.RawMovement.x));
        _anim.SetFloat("VelocityY", _player.RawMovement.y);
        _anim.SetBool("Grounded", _player.Grounded);
    }

}
