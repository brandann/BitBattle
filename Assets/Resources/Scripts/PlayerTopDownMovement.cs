using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerTopDownMovement : MonoBehaviour {

    // PLAYER ID
    public enum ePlayerID { Player1 = 1, Player2 = 2, Player3 = 3, Player4 = 4}
    public ePlayerID mPlayerID;

    // MOVEMENT SPEEDS
    public float mForwardSpeed;
    private Vector2 mVelocity;

    // REFRENCED GAMEOBJECTS
    public GameObject mGlobalGameObject;
    //public GameObject mCamera;

	private float _speedmod = 1;
    public float mSpeedMod{
	    set{ _speedmod = value;}
	    get{ return _speedmod;}
    }

    // STARTING LOCATION BASED OF UNITY LOCATION OF GAMEOBJECT
    private Vector3 mStartingPosition;

    public GameObject mBurstPrefab;

    public enum ePlayerDeathEvents { Projectile, Lava }
    
    private PlayerPowerupManager mPlayerPowerups;
    private PlayerFireManager mPlayerFire;
    
    // Use this for initialization
    void Start () {
        // INIT VARIABLES
        mVelocity = new Vector2(0, 0);
        mStartingPosition = transform.position;
        mPlayerPowerups = this.GetComponent<PlayerPowerupManager>();
        mPlayerFire = this.GetComponent<PlayerFireManager>();
	}
	
	// Update is called once per frame
    void Update()
    {
        Move(); // HANDLE MOVING
    }

    private void Move()
    {
        // GET ID TO REDUCE CASTING IN UPDATE
        int ID = (int)mPlayerID;

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

    void OnTriggerEnter2D(Collider2D c)
    { 

    }

    #region DEATH_EVENTS
    // KILL THE PLAYER AND REPORT THE DEATH TO GLOBAL
    public void kill(ePlayerDeathEvents e)
    {
        bool death = false;
        switch(e)
        {
            case (ePlayerDeathEvents.Projectile):
                death = KillProjectile();
                break;
            case (ePlayerDeathEvents.Lava):
                death = KillLava();
                break;
        }
        
        if(death)
        {
        	deactivatePowerups();
        	
            var burstGO = GameObject.Instantiate(mBurstPrefab);
            burstGO.transform.position = this.transform.position;
            this.transform.position = mStartingPosition;
            
            // REGESTER DEATH WITH THE SIMPLE GLOBAL
            mGlobalGameObject.GetComponent<SimpleGlobal>().Death((int)mPlayerID);
        }
    }
    
    private void deactivatePowerups()
    {
		mPlayerPowerups.deactivateFastPowerup();
		mPlayerPowerups.deactivateInvinciblePowerup();
		mPlayerPowerups.deactivateFreezePowerup();
		mPlayerPowerups.deactivateShieldPowerup();
	}
    

    private bool KillLava()
    {
        // RIGHT NOW LAVA KILLS YOU NO MATTER WHAT!
        return true; 
    }

    private bool KillProjectile()
    {
		if (mPlayerPowerups.mInvincibleIsActive)
        {
            // DO NOT HURT THE PLAYER
			mPlayerFire.mAvailShots++;
            Debug.Log("Kill blocked by Shield");
            return false;
        }
		else if(mPlayerPowerups.mShieldIsActive)
        {
			mPlayerPowerups.deactivateShieldPowerup();
        	return false;
        }
        return true;
    }
    #endregion END_KILL
}
