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

    private void FireHelper(Vector3 pos)
    {
        var g3 = GameObject.Instantiate(mBullet);
        g3.transform.position = pos;
        var SPB = g3.GetComponent<SimpleProjectileBehavior>();
        SPB.setTarget(this.transform.position + (this.transform.up * 20));
        SPB.SetOwner((int) this.mPlayerID);
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

    // APPLY A SPEED MOD TO THE PLAYER ( +/- )
    public void activateSpeedPowerup(float time, float mod)
    {
        // CREATE THE SPEED MOD
        SpeedMod fm = new SpeedMod();
        fm.time = time;
        fm.mod = mod;

        // START THE SPEED MOD COROUTINE
        StartCoroutine("FreezeRoutine", fm);
    }

    // SPEED MOD STRUCT REQUIRED FOR COROUTINE ONLY ALLOWING A SINGLE
    // PARAMETER...
    private struct SpeedMod
    {
        public float time;
        public float mod;
    }

    // COROUTINE FOR SPEED MOD
    IEnumerator FreezeRoutine(SpeedMod fm)
    {
        
        float prevSpeedMod = mSpeedMod;             // SAVE THE PREV SPEED MOD FOR RETIEIVAL AFTER MOD IS FINISHED
        mSpeedMod = fm.mod;                         // SET THE NEW SPEED MOD
        yield return new WaitForSeconds(fm.time);   // WAIT FOR THE MOD DURATION TO FINISH
        mSpeedMod = prevSpeedMod;                   // RETURN THE MOD TO ITS PREV MOD
        //Debug.Log("Powerup Finished");            // DEBUGGING
    }

    // KILL THE PLAYER AND REPORT THE DEATH TO GLOBAL
    public void kill()
    {
        this.transform.position =mStartingPosition;

        // REGESTER DEATH WITH THE SIMPLE GLOBAL
        mGlobalGameObject.GetComponent<SimpleGlobal>().Death((int)mPlayerID);
    }
}
