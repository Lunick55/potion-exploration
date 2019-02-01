using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] float gravity = 0.0f;
    [SerializeField] float jumpSpeed = 0.0f;
	private Transform myTrans;
    float vSpeed = 0;

    CharacterController cc;

    private bool canMovePlayer = true;
	private bool canJump = false;
	private bool canInteract = true;

    private Vector3 inputDir = Vector3.zero;

	private GameObject pickup;
	private bool isHoldingPickup = false;

	enum Interaction
	{
		NONE,
		PICKUP,
	};

	Interaction currInteract;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("UP", HandleUp);
        EventManager.StartListening("DOWN", HandleDown);
        EventManager.StartListening("LEFT", HandleLeft);
        EventManager.StartListening("RIGHT", HandleRight);
		EventManager.StartListening("E_down", HandleInteraction);
		EventManager.StartListening("SPACE", HandleSpace);

		cc = GetComponent<CharacterController>();
		myTrans = this.transform;

		currInteract = Interaction.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (canMovePlayer)
        {
            //sets the left and right movement
            inputDir = new Vector3(inputDir.x, 0.0f, inputDir.z);

            //apply speed
            inputDir = inputDir * speed;

            //check if grounded
            if (cc.isGrounded)
            {
                vSpeed = 0;

                //jump thing. 
                if (canJump)
                {
                    vSpeed = jumpSpeed;
                }
            }
			else
			{
				canJump = false;
			}

            //apply accelerating gravity
            inputDir.y = (vSpeed -= (gravity * Time.deltaTime)); 

            //move the object
            cc.Move(inputDir * Time.deltaTime); // this might need to get out of the if statement
        }

        inputDir = Vector3.zero;
    }


	//Interaction Handler. Depending on current Interaction type execute specific functions
	void HandleInteraction()
	{
		if (canInteract)
		{ 
			if (currInteract == Interaction.NONE)
			{
				return;
			}
			else if (currInteract == Interaction.PICKUP)
			{
				PickupObject();
			}
		}
	}

	//Pickups up the object currently in "pickup" if there is one
	void PickupObject()
	{
		if (pickup == null)
		{
			Debug.LogWarning("You are somehow trying to pickup nothing.");
			return;
		}
		if (isHoldingPickup)
		{
			//Debug.Log("Drop: " + pickup.name);
			//We are holding something, so drop it where we are.
			pickup.transform.parent = null;
			pickup.transform.position = new Vector3(myTrans.position.x, myTrans.position.y, myTrans.position.z);
			pickup.GetComponent<Collider>().enabled = true;
			isHoldingPickup = false;
			return;
		}

		Transform pickupTrans = pickup.transform;

		//Debug.Log("Pick up: " + pickup.name);
		//TODO: define pickupTrans on pickup, make it a member variable
		//We are able to pick something up, so snag it
		pickupTrans.parent = this.transform;
		pickupTrans.position = new Vector3(myTrans.position.x, myTrans.position.y + 0.5f, myTrans.position.z);
		pickup.GetComponent<Collider>().enabled = false;
		isHoldingPickup = true;
	}

    private void OnTriggerEnter(Collider col)
    {

    }

	private void OnTriggerStay(Collider col)
	{

	}

	private void OnTriggerExit(Collider col)
	{

	}





	//sets directions based on input handler
	void HandleLeft()
    {
        inputDir.x = -1;
    }
    void HandleRight()
    {
        inputDir.x = 1;
    }
    void HandleUp()
    {
        inputDir.z = 1;
    }
    void HandleDown()
    {
        inputDir.z = -1;
    }
	void HandleSpace()
	{
		if (cc.isGrounded)
		{
			canJump = true;
		}
	}
}
