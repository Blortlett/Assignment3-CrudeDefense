using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D Rb; //Players rigid body for movement and collision

    private float MoveAcceleration = 30;    //Character move speed
    private float MaxMoveSpeed = 10;    //Character move speed
    private int LastPlayerMoveInput = 0;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>(); //Sets rigid body
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //Gets player input and moves rigid body
    private void Move()
    {
        int PlayerInput = 0;

        //Gets player input
        if (Input.GetKey(KeyCode.D))
        {
            PlayerInput += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerInput -= 1;
        }


        if (PlayerInput != 0)   //If player is inputing
        {
            Rb.drag = 0;    //Player is frictionless

            float XVelocity = Rb.velocity.x;

            if (LastPlayerMoveInput != PlayerInput) //If change in input direction
            {
                XVelocity = 0;  //Set velocity to zero so character doesn't slide
            }

                XVelocity += PlayerInput * MoveAcceleration * Time.deltaTime;   //Adds to velocity
            XVelocity = Mathf.Clamp(XVelocity, -MaxMoveSpeed, MaxMoveSpeed);    //Clamps velocity between -max and max move speed

            Rb.velocity = new Vector2(XVelocity, 0);  //Sets player velocity
        }
        else
        {
            Rb.drag = 100;  //Increases drag so character slows down
        }

        LastPlayerMoveInput = PlayerInput;  //Sets last player move input
    }
}
