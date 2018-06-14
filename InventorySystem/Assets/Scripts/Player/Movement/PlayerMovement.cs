using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
    public GameObject ItemsArea;

    //Speed Variables for the player
    public float speed = 7;

    public float defaultSpeed = 7;

    public float jumpHeight = 12;
    public float defaultJumpHeight = 0;

    //Player graivity and check if grounded or not
    public float gravity = 55;

    //Disable crouching, jumping, running
    public bool allowJumping = true;

    public bool isGrounded = false;

    public bool DisableMovement = false;

    //The player direction
    public Vector3 moveDirection = Vector3.zero;

    //Define Different Script or Components
    CharacterController characterController;

    //Set the X axis and Z values
    float movementX = 0;    //Left-Right
    float movementZ = 0;    //Forward-Backwards

    void Awake()
    {
        //Adding Character Controller in case it's missing
        characterController = GetComponent<CharacterController>();
    }

    //This will replace the movementZ variable
    float MoveForwardBackwards(float movement, float forward, float backward)
    {
        if (forward != 0)
            movement += 1;
        
        if (backward != 0)
            movement -= 1;

        return movement;
    }

    //This will replace the movementZ variable
    float MoveLeftRight(float movement, float right, float left)
    {
        if (right != 0)
            movement += 1;
        
        if (left != 0)
            movement -= 1;

        return movement;
    }

    //This is the function that takes care of the players movement
    public void Movement(float forward, float backward, float right, float left, float jump)
    {
        //If Player is grounded run the below code
        if (isGrounded && DisableMovement == false)
        {
            //Move the player based on the keys he enters
            moveDirection = new Vector3(MoveLeftRight(movementX, right, left), 0, MoveForwardBackwards(movementZ, forward, backward));
            moveDirection = transform.TransformDirection(moveDirection).normalized;
            moveDirection *= speed;

            //If player is allowed to jump he can by pressing the jump key
            if (allowJumping == true)
            {
                if (jump > 0)
                    moveDirection.y = jumpHeight;
                else
                    moveDirection.y = defaultJumpHeight;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        var flags = characterController.Move(moveDirection / 2 * Time.deltaTime);
        isGrounded = (flags & CollisionFlags.CollidedBelow) != 0;
    }

    void Update() 
    {
        ItemsArea.transform.position = transform.position;
        Movement(Input.GetAxis("Forward"), Input.GetAxis("Backward"), Input.GetAxis("Right"), Input.GetAxis("Left"), Input.GetAxis("Jump"));
    }
}