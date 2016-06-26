using UnityEngine;
using System.Collections;

public class Player2AxisMovement : MonoBehaviour {

    private float _speed = 6;
    private float _speedMod = 1;

    private Vector3 mStartingPosition;
    private Vector3 mVelocity;
    private Vector3 mLookAt;

    private PlayerStateManager mPlayer;

	void Start () {
        mVelocity = new Vector3(0, 0, 0);
        mLookAt = new Vector3(0, 0, 0);
        mStartingPosition = transform.position;
        mPlayer = this.GetComponent<PlayerStateManager>();
	}
	
	// Update is called once per frame
	void Update () {
        // GET ID TO REDUCE CASTING IN UPDATE
        int ID = (int)mPlayer.mPlayerID;

        // GET THE VELOCITY FROM INPUT AND ANDJUST IT TO SPEEDS
        mVelocity.y = Input.GetAxis(InputMap.VerticalL + ID);
        mVelocity.x = Input.GetAxis(InputMap.HorizontalL + ID);

        // GET THE LOOK AT VECTOR FROM THE RIGHT JOYSTICK
        mLookAt.y = Input.GetAxis(InputMap.VerticalR + ID);
        mLookAt.x = Input.GetAxis(InputMap.HorizontalR + ID);

        if(mVelocity.magnitude > 0)
            transform.LookAt(transform.position + new Vector3(0, 0, 1), mVelocity);
        else if(mLookAt.magnitude > 0)
            transform.LookAt(transform.position + new Vector3(0, 0, 1), mLookAt);
        
        // SET THE RIGIDBIDY2D VELOCITY
        GetComponent<Rigidbody2D>().velocity = (transform.up * mVelocity.magnitude * _speed * _speedMod);
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

    public float mSpeedMod
    {
        get
        {
            return _speedMod;
        }
        set
        {
            _speedMod = value;
        }
    }
}
