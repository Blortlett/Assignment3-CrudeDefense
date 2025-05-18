using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Pickup variables
    public GameObject GrabLocation;
    private GameObject HeldObject = null;






    private Rigidbody2D Rb; //Players rigid body for movement and collision

    private float MoveAcceleration = 30;    //Character move speed
    private float MaxMoveSpeed = 10;    //Character move speed
    private int LastPlayerMoveInput = 0;

    private bool InteractButtonPressed = false;

    private List<GameObject> OverlappingObjects = new List<GameObject>();

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

        if (Input.GetKey(KeyCode.E))    //If pressed E
        {
            if (!InteractButtonPressed)
            {
                InteractButtonPressed = true;

                if (HeldObject == null) //If not holding object
                {
                    GameObject Object = FindFirstPickupableObject();      //Get object to pickup if there is one, else it is null

                    if (Object != null)   //If there is object to pick up
                    {
                        PickupObject(Object);   //Picks up the object
                    }
                }
                else
                {
                    PutdownObject();    //Put down object
                }
            }
        }
        else
        {
            InteractButtonPressed = false;
        }

        if (HeldObject != null) //If holding object
        {
            HeldObject.transform.position = GrabLocation.transform.position;    //Sets held object postion to the plays grab location
        }
    }

    //Gets player input and moves rigid body
    private void Move()
    {
        int PlayerInput = 0;

        //Gets player input
        if (Input.GetKey(KeyCode.D))
        {
            PlayerInput += 1;
            transform.localScale = new Vector3(1, 1, 1);    //Make player face right direction
        }
        if (Input.GetKey(KeyCode.A))    //Make player face left direction
        {
            PlayerInput -= 1;
            transform.localScale = new Vector3(-1, 1, 1);
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


    private void PickupObject(GameObject _Object)
    {
        HeldObject = _Object;   //Set held object
        IPickupable PickupableScript = HeldObject.GetComponent<IPickupable>();
        PickupableScript.Pickup();

        HeldObject.transform.position = GrabLocation.transform.position;    //Move object to the grab position
    }

    private void PutdownObject()
    {
        IPickupable PickupableScript = HeldObject.GetComponent<IPickupable>();
        PickupableScript.PutDown();

        HeldObject.transform.position = new Vector3(HeldObject.transform.position.x, HeldObject.GetComponent<IPickupable>().GetOriginalYPosition(), HeldObject.transform.position.z); //Put object back on ground
        HeldObject = null;  //Playing is no longer holding object

    }

    GameObject FindFirstPickupableObject()
    {
        if (OverlappingObjects.Count != 0)  //If player is overlapping something
        {
            foreach (GameObject Object in OverlappingObjects)   //For each overlapping object
            {
                IPickupable PickupableScript = Object.GetComponent<IPickupable>();    //Get pickupable script
                IInteractable InteractableScript = Object.GetComponent<IInteractable>();    //Get interactable script
                if (PickupableScript != null) //If object has interactable scrip
                {
                    //Return object for pickup
                    if (PickupableScript.CanPickup())
                    {
                        return Object;
                    }
                }
                else if (InteractableScript != null)
                {
                    if (InteractableScript.CanInteract())    //If object is interactable and can interact
                    {
                        InteractableScript.Interact();  //Call Interact function
                    }
                }
            }
        }

        return null;
    }

    //On overlap
    private void OnTriggerEnter2D(Collider2D _Collider)
    {
        OverlappingObjects.Add(_Collider.gameObject);  //Add to list
    }

    //On stop overlap
    private void OnTriggerExit2D(Collider2D _Collider)
    {
        OverlappingObjects.Remove(_Collider.gameObject);   //Remove from list
    }

}