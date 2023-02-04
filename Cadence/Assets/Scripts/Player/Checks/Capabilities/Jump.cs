using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Controller))]
public class Jump : MonoBehaviour
{
    //SerializedFields allow unity to display private vaiables in the inspector
    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;//float to control how high you can jump
    [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;//Value that allows for double, tripple, quadruple jumping etc.
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;//Controls gravity multiplier
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;//Controls jumping force
    [SerializeField, Range(0f, 0.3f)] private float _coyoteTime = 0.2f;//Specified time that allows for jumping after leaving a platform
    [SerializeField, Range(0f, 0.3f)] private float _jumpBufferTime = 0.2f;//Specified time that allows you to BufferQ a Jump before you hit the ground

    private Controller _controller;//Holds the controller that the player is using (Player/AI)
    private Rigidbody2D _body;//Holds the rb of the gameObject
    private Ground _ground;//Holds an instance of the Ground class
    private Vector2 _velocity;//Holds the velocity

    private int _jumpPhase;//Holds which phase of the jump the player is currently in
    private float _defaultGravityScale;//Holds the default gravity scale
    private float _coyoteCounter;//Used as a countdown after a user leaves a ground
    private float _jumpBufferCounter;

    private bool _desiredJump;//Does the player want to jump?
    private bool _onGround;//Is the player grounded?
    private bool _isJumping;//Is the player jumping?
    

    private void Awake()//As soon as the program begins
    {
        _body = GetComponent<Rigidbody2D>(); //Gets the rigidbody
        _ground = GetComponent<Ground>();//Gets the ground
        _controller = GetComponent<Controller>();//Gets the controller

        _defaultGravityScale = 1f;//Sets default gravity scale to 1

    }

    private void Update()//Every frame
    {
        _desiredJump |= _controller.input.RetrieveJumpInput();//Has the player hit the jump button

    }

    private void FixedUpdate()//Every fixedframe frame
    {
        _onGround = _ground.GetOnGround();//Retrieve if grounded from ground
        _velocity = _body.velocity;//Retrieve velocity from rb

        if(_onGround && _body.velocity.y==0)//If grounded and no vertical velocity is applied to the user
        {
            _jumpPhase = 0;//Set jumpPhase to 0
            _coyoteCounter = _coyoteTime;//Reset coyoteCounter
            _isJumping = false;//Set jumping to false
        }
        else
        {
            _coyoteCounter -= Time.deltaTime;//Start counitng down coyote
        }

        if (_desiredJump)//If the player pressed jump
        {
            _desiredJump = false;//Set the bool to false
            _jumpBufferCounter = _jumpBufferTime;
        }else if(!_desiredJump && _jumpBufferCounter > 0)
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        if (_jumpBufferCounter > 0)
        {
            JumpAction();
        }

        if(_controller.input.RetrieveJumpHoldInput() && _body.velocity.y > 0)//If thejump key is held and the player is travelling up
        {
            _body.gravityScale = _upwardMovementMultiplier;//Set gravity scale to upward multiplier
        }else if (!_controller.input.RetrieveJumpHoldInput() || _body.velocity.y < 0)//If the plyer isnt holding jump and the player is going down
        {
            _body.gravityScale = _downwardMovementMultiplier;//Set the gravity to the downward multiplier
        }else if (_body.velocity.y==0)//If the player is stationary in the y axis
        {
            _body.gravityScale = _defaultGravityScale;//Reset the gravity scale
        }
        
        _body.velocity = _velocity;//Set the rb velocity to velocity
    }

    private void JumpAction()//Runs when someone presses the jump key
    {
        if (_coyoteCounter > 0f || _jumpPhase < _maxAirJumps && _isJumping)//If coyote timer is still up, and not all jumps has been used and the player is jumping midair
        {
            if (_isJumping)
            {
                _jumpPhase += 1;
            }
            _jumpBufferCounter = 0;
            _coyoteCounter = 0;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);
            _isJumping = true;
            if (_velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed-_velocity.y, 0f);
            }
            _velocity.y += jumpSpeed;
        }
    }
}
