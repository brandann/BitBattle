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
    public GameObject mBullet;
    public GameObject mGlobalGameObject;
    public GameObject mCamera;

    // FIRING DELAY
    private float mLastFireTime;
    private const float DURATION = 0.25f;

    private float mSpeedMod = 1;

    // STARTING LOCATION BASED OF UNITY LOCATION OF GAMEOBJECT
    private Vector3 mStartingPosition;

    // AVAILABLE SHOTS TO THE PLAYER. SHOULD NEVER BE A NEG NUMBER
    public int mAvailShots;
    public int mTotalShotsFired;
    
    private bool mFreezeIsActive = false;

	public GameObject mInvinciblePrefab;
    private GameObject mInvincibleObject;
	private bool mInvincibleIsActive = false;

    public GameObject mFastPrefab;
    private GameObject mFastObject;
	private bool mFastIsActive = false;
	    
    public GameObject mShieldPrefab;
    private GameObject mShieldObject;
    private bool mShieldIsActive = false;

    public GameObject mBurstPrefab;

    public enum ePlayerDeathEvents { Projectile, Lava }

    // Use this for initialization
    void Start () {
        // INIT VARIABLES
        mVelocity = new Vector2(0, 0);
        mStartingPosition = transform.position;
	}
	
	// Update is called once per frame
    void Update()
    {
        Move(); // HANDLE MOVING
        Fire(); // HANDLE SHOOTING
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
        if (null != mCamera)
        {
            mCamera.transform.position = this.transform.position + new Vector3(0, 0, -10);
        }
    }

    private void Fire()
    {
        if (Input.GetButton(InputMap.ButtonB + (int)mPlayerID))
        {
            RaycastHit2D hit1;
            // Ray ray = new Ray(this.transform.position, this.transform.up);
            if (hit1 = Physics2D.Raycast(this.transform.position + this.transform.up, this.transform.up, Mathf.Infinity))
            {
                drawMyLine(this.transform.position, hit1.point, Color.yellow, 1f);

                RaycastHit2D hit2;
                // Ray ray = new Ray(this.transform.position, this.transform.up);

                /*
                TODO

                THE BEAM SHOULD REFLECT OFF THE SURFCE. RIGHT NOW I HAVE A HACKY INCORRECT REFLECTION
                IMPLEMENTED THAT A) ONLY GOES RIGHT OF THE PLAYER, B) ONLY REFLECTS AT A 90 DEG ANGLE.
                SHOULE BE UPDATED WITH NORMALS, FRONTS, DOTS AND CROSS MATH TO ACURATLY REFLECT THE BEAM

                BEAM SHOULD ALSO NOT DUPLICATE, SHOULD BE A SINGLE BEAM THAT UPDATES AS THE PLAYER MOVES.
                */

                Vector2 up = new Vector2(this.transform.up.x, this.transform.up.y) * -1;
                if (hit2 = Physics2D.Raycast(hit1.point + up, this.transform.right, Mathf.Infinity))
                {
                    drawMyLine(hit1.point, hit2.point, Color.yellow, 1f);
                }
            }
            //Debug.DrawLine(transform.position, this.transform.position + (this.transform.up * 20), Color.yellow);
            //drawMyLine(transform.position, this.transform.position + (this.transform.up * 20), Color.yellow, .05f);
        }
        
        // IF: DURATION HAS PASSED
        // IF: RIGHT TRIGGER IS PRESSED
        if (((Time.timeSinceLevelLoad - mLastFireTime) > DURATION) && Input.GetAxis(InputMap.TriggerR + (int)mPlayerID) > 0)
        {
            // RESET THE TIME OF THE THIS SHOOT BEING FIRED
            mLastFireTime = Time.timeSinceLevelLoad;

            // CHECK TO MAKE SURE THE PLAYER HAS SOME SHOTS AVAILABLE
            if(mAvailShots <= 0) { return; } 

            // SHOT COUNTERS
            mAvailShots--;
            mTotalShotsFired++;
            
            FireHelper(this.transform.position + (this.transform.up * 1f));         // CREATE THE CENTER PROJECTILE
            //FireHelper(this.transform.position - (this.transform.right * 0.6f));    // CREATE THE LEFT PROJECTILE
            //FireHelper(this.transform.position + (this.transform.right * 0.6f));    // CREATE THE RIGHT PROJECTILE
            //drawMyLine(transform.position, this.transform.position + (this.transform.up * 20), Color.yellow,0.1f);
        }
    }

    private void FireHelper(Vector3 pos)
    {
        var g3 = GameObject.Instantiate(mBullet);
        g3.transform.position = pos;
        var SPB = g3.GetComponent<SimpleProjectileBehavior>();
        SPB.setTarget(this.transform.position + (this.transform.up * 1000 /*dist should be large number. maybe change to a non distance (non-target)*/));
        SPB.SetOwner((int)this.mPlayerID);
    }

    private void drawMyLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        StartCoroutine(drawLine(start, end, color, duration));
    }
    
    IEnumerator drawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Additive"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        yield return new WaitForSeconds(duration);
        GameObject.Destroy(myLine);
    }
    
    public void Teleport(GameObject go)
    {
        // MOVE MY POSITION TO THE TELEPORT DESTINATION
        this.transform.position = go.transform.position;
    }

    void OnTriggerEnter2D(Collider2D c)
    { 

    }

    public void PickupBullet()
    {
        this.mAvailShots++;
    }

    // SPEED MOD STRUCT REQUIRED FOR COROUTINE ONLY ALLOWING A SINGLE
    // PARAMETER...
    private struct SpeedMod
    {
        public float time;
        public float mod;
    }

    #region FAST_POWER
    // APPLY A SPEED MOD TO THE PLAYER ( +/- )
    public void activateFastPowerup(float time, float mod)
    {
        mFastObject = GameObject.Instantiate(mFastPrefab);
        mFastObject.transform.position = this.transform.position;
        mFastObject.transform.parent = this.transform;

        // CREATE THE SPEED MOD
        SpeedMod fm = new SpeedMod();
        fm.time = time;
        fm.mod = mod;

        // START THE SPEED MOD COROUTINE
        this.mFastIsActive = true;
        StartCoroutine("FastRoutine", fm);
    }

    // COROUTINE FOR SHIELD MOD
	IEnumerator FastRoutine(SpeedMod fm)
	{
		float prevSpeedMod = mSpeedMod;             // SAVE THE PREV SPEED MOD FOR RETIEIVAL AFTER MOD IS FINISHED
		mSpeedMod = fm.mod;                         // SET THE NEW SPEED MOD
		yield return new WaitForSeconds(fm.time);   // WAIT FOR THE MOD DURATION TO FINISH
		if(this.mFastIsActive){
			deactivateFastPowerup(prevSpeedMod);
		}
    }
    
    private void deactivateFastPowerup(float prev = 1)
    {
		Destroy(mFastObject);
		mSpeedMod = prev;
		this.mFastIsActive = false;
		Debug.Log("deactivateFastPowerup");            // DEBUGGING
	}
    #endregion END_FAST
    
    #region FREEZE_POWER
    // APPLY A SPEED MOD TO THE PLAYER ( +/- )
    public void activateSpeedPowerup(float time, float mod)
    {
        // CREATE THE SPEED MOD
        SpeedMod fm = new SpeedMod();
        fm.time = time;
        fm.mod = mod;

        // START THE SPEED MOD COROUTINE
        this.mFreezeIsActive = true;
        StartCoroutine("FreezeRoutine", fm);
    }

    // COROUTINE FOR SPEED MOD
    IEnumerator FreezeRoutine(SpeedMod fm)
    {
        float prevSpeedMod = mSpeedMod;             // SAVE THE PREV SPEED MOD FOR RETIEIVAL AFTER MOD IS FINISHED
        mSpeedMod = fm.mod;                         // SET THE NEW SPEED MOD
        yield return new WaitForSeconds(fm.time);   // WAIT FOR THE MOD DURATION TO FINISH
        if(this.mFreezeIsActive){
			deactivateFreezePowerup(prevSpeedMod);
		}
	}
    
    private void deactivateFreezePowerup(float prev = 1)
    {
     	mSpeedMod = prev;                			// RETURN THE MOD TO ITS PREV MOD
		Debug.Log("deactivateFreezePowerup");       // DEBUGGING
		this.mFreezeIsActive = false;
	}
	#endregion END_FREEZE

    #region INVINCIBLE_POWER
	public void activateInvinciblePowerup(float time)
    {
		mInvincibleObject = GameObject.Instantiate(mInvinciblePrefab);
        mInvincibleObject.transform.position = this.transform.position;
        mInvincibleObject.transform.parent = this.transform;

        // START THE SHIELD MOD COROUTINE
		this.mInvincibleIsActive = true;
		StartCoroutine("InvincibleRoutine", time);
    }

    // COROUTINE FOR SHIELD MOD
	IEnumerator InvincibleRoutine(float time)
    {
        yield return new WaitForSeconds(time);   // WAIT FOR THE MOD DURATION TO FINISH
		if(this.mInvincibleIsActive){ 
			deactivateInvinciblePowerup();
		}
    }
    
	private void deactivateInvinciblePowerup()
	{
		this.mInvincibleIsActive = false;
		Destroy(mInvincibleObject);
		Debug.Log("deactivateInvinciblePowerup");
	}
	#endregion END_INVICIBLE
    
	#region SHIELD_POWER
	public void activateShieldPowerup()
	{
		mShieldIsActive = true;
		mShieldObject = GameObject.Instantiate(mShieldPrefab);
		mShieldObject.transform.position = this.transform.position;
		mShieldObject.transform.parent = this.transform;
	}
	
	private void deactivateShieldPowerup()
	{
		Destroy(mShieldObject);
		mShieldIsActive = false;
		Debug.Log("deactivateShieldPowerup");
	}
	#endregion END_SHIELD

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
		deactivateFastPowerup();
		deactivateInvinciblePowerup();
		deactivateFreezePowerup();
		deactivateShieldPowerup();
	}
    

    private bool KillLava()
    {
        // RIGHT NOW LAVA KILLS YOU NO MATTER WHAT!
        return true; 
    }

    private bool KillProjectile()
    {
        if (mInvincibleIsActive)
        {
            // DO NOT HURT THE PLAYER
            this.mAvailShots++;
            Debug.Log("Kill blocked by Shield");
            return false;
        }
        else if(mShieldIsActive)
        {
        	Destroy(mShieldObject);
        	mShieldIsActive = false;
        	return false;
        }
        return true;
    }
    #endregion END_KILL
}
