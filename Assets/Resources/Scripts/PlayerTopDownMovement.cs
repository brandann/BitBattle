using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerTopDownMovement : MonoBehaviour {

    // MOVEMENT SPEEDS
    public float mForwardSpeed;
    private Vector2 mVelocity;

    //public GameObject mCamera;
    public PlayerStateManager mPlayer;

	private float _speedmod = 1;
    public float mSpeedMod{
	    set{ _speedmod = value;}
	    get{ return _speedmod;}
    }

    // STARTING LOCATION BASED OF UNITY LOCATION OF GAMEOBJECT
    private Vector3 mStartingPosition;

    // Use this for initialization
    void Start () {
        // INIT VARIABLES
        mVelocity = new Vector2(0, 0);
        mStartingPosition = transform.position;
        mPlayer = this.GetComponent<PlayerStateManager>();
	}
	
	// Update is called once per frame
    void Update()
    {
        Move(); // HANDLE MOVING
    }

    private void Move()
    {
        // GET ID TO REDUCE CASTING IN UPDATE
		int ID = (int) mPlayer.mPlayerID;

        // GET THE VERICAL VELOCITY FROM INPUT AND ANDJUST IT TO SPEEDS
        mVelocity.y = Input.GetAxis(InputMap.VerticalL + ID);

        // GET THE HORIZONTAL VELOCITY FROM INPUT AND ADJUST IT TO SPEEDS
        mVelocity.x = Input.GetAxis(InputMap.HorizontalL + ID);

        // GET MOVEMENT ACTIVE WITH A BUTTON
        // int accelerate = (Input.GetButton(InputMap.ButtonA + ID)) ? 1 : 0;
        int accelerate = 1; // for keyboard its easier to not use this!

        //if (mVelocity.magnitude != 0)
        if((Input.GetAxisRaw(InputMap.VerticalL + ID) != 0) || (Input.GetAxisRaw(InputMap.HorizontalL + ID) != 0))
        {
            transform.LookAt(transform.position + new Vector3(0, 0, 1), mVelocity);
        }
        
        // SET THE RIGIDBIDY2D VELOCITY
        GetComponent<Rigidbody2D>().velocity = (transform.up * mVelocity.magnitude * mForwardSpeed * mSpeedMod * accelerate);

        // IF A CAMERA IS ATTACHED TO THIS GAME OBJECT THEN MOVE THE CAMERA WITH THE PLAYER
        // THIS IS A TESTING THING, REMOVE CODE IF SPLIT SCREEN IS TO NOT BE USED
        //if (null != mCamera)
        //{
        //    mCamera.transform.position = this.transform.position + new Vector3(0, 0, -10);
        //}
    }

    public void Teleport(GameObject go)
    {
        // MOVE MY POSITION TO THE TELEPORT DESTINATION
        this.transform.position = go.transform.position;
    }
    
    public void ResetPosition()
    {
		this.transform.position = mStartingPosition;
    }
}
