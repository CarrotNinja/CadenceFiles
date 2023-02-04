using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
/*
 * author rash25
 * Last edited 2/2/23
 * This program handles player movement
 */
public class Move : MonoBehaviour
{
    //SerializedFields allow unity to display private vaiables in the inspector
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;//Controls maxSpeed
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;//Controls maxAcceleration
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;//Controllers AirAcceleration

    private Vector2 _direction, _desiredVelocity, _velocity;//Vectors for direction, desiredVelocity and velocity
    private Rigidbody2D _body;//rigidbody
    private Ground _ground;//ground
    private Controller _controller;//which controller is being used

    private float _maxSpeedChange;//The greates speedChange
    private float _accelerration;//Acceleration
    private bool _onGround;//is grounded?
    private bool _facingRight;//facing right?
    
    void Awake()//When the program starts
    {
        _body = GetComponent<Rigidbody2D>();//Rigidbody component is taken from the player
        _ground = GetComponent<Ground>();//Ground component taken from player
        _controller = GetComponent<Controller>();//Controller component taken from player
    }

    // Update is called once per frame
    void Update()
    {
        _direction.x = _controller.input.RetrieveMoveInput();//Direction x is set to the input of A and D keys
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _ground.GetFriction(), 0f);//Desired velocity is set to a new vector with input and friction and Maxspeed considered
    }

    private void FixedUpdate()//Every fixed frame
    {
        _onGround = _ground.GetOnGround();//gets onGround from ground component
        _velocity = _body.velocity;//velocity set equal to current velocity

        _accelerration = _onGround ? _maxAcceleration : _maxAirAcceleration;//Acceleration if grounded is set to maxAcceleration, otherwise AirAcceleration
        _maxSpeedChange = _accelerration * Time.deltaTime;//maxSpeedChange is set to acceleration * Time since last frame
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);//Velocity x moves towards desiredVelocity

        _body.velocity= _velocity;//rb velocity is updated

        if ((_controller.input.RetrieveMoveInput() < 0 && _facingRight == false) || (_controller.input.RetrieveMoveInput() > 0 && _facingRight == true))// If the direction input conflicts the direction thats being faced
        {
            Flip();//Flip
        }
    }
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;//Stores the current transform of the player
        currentScale.x *= -1;//Reflects it in the x direction
        gameObject.transform.localScale = currentScale;//Sets the transform ot the inverted transform
        _facingRight = !_facingRight;//Flips the boolean facing value
    }
}
