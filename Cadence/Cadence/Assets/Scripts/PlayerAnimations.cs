using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private LayerMask _groundMask;

    private IPlayerController _player;
    private bool _playerGrounded;
    private Vector2 _movement;
    void Awake() => _player = GetComponentInParent<IPlayerController>();

    // Update is called once per frame
    void Update()
    {
        if (_player == null) return;
        if (_player.Input.X != 0) transform.localScale = new Vector3(_player.Input.X > 0 ? 1 : -1, 1, 1);
    }
}
