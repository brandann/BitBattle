using UnityEngine;
using System.Collections;

public class PlayerFireManager : MonoBehaviour {

	// AVAILABLE SHOTS TO THE PLAYER. SHOULD NEVER BE A NEG NUMBER
	public int mAvailShots;
	public int mTotalShotsFired;
	
	public GameObject mBullet;
    public GameObject mProjectile;
    public bool mFireProjectile;
	
	private PlayerStateManager mPlayer;
	
	// FIRING DELAY
	private float mLastFireTime;
	private const float DURATION = 0.25f;

    // LOAD PROJECTILES
    private float mLastLoadTime;
    private const float LOAD_DURATION = 1;
	
	// Use this for initialization
	void Start () {
		mPlayer = this.GetComponent<PlayerStateManager>();
	}
	
	// Update is called once per frame
	void Update () {
		Fire(); // HANDLE SHOOTING

        if(mProjectile)
        {
            if(Time.timeSinceLevelLoad - mLastLoadTime > LOAD_DURATION)
            {
                if(mAvailShots < 20)
                {
                    mAvailShots++;
                    mLastLoadTime = Time.timeSinceLevelLoad;
                }
            }
        }
	}
	
	public void PickupBullet()
	{
		this.mAvailShots++;
	}
	
	private void Fire()
	{
        if(mFireProjectile)
        {
            if (((Time.timeSinceLevelLoad - mLastFireTime) > DURATION) && Input.GetAxis(InputMap.TriggerR + (int)mPlayer.mPlayerID) > 0)
            {
                // RESET THE TIME OF THE THIS SHOOT BEING FIRED
                mLastFireTime = Time.timeSinceLevelLoad;

                // CHECK TO MAKE SURE THE PLAYER HAS SOME SHOTS AVAILABLE
                if (mAvailShots <= 0) { return; }

                // SHOT COUNTERS
                mAvailShots--;
                mTotalShotsFired++;

                var g3 = GameObject.Instantiate(mProjectile);
                g3.transform.position = this.transform.position + this.transform.up;
                var SMF = g3.GetComponent<SimpleMoveForward>();
                SMF.Target = this.transform.position + (this.transform.up * 1000);
                var SKT = g3.GetComponent<SimpleKillTouch>();
                SKT.setPlayer(this.mPlayer.mPlayerID);
            }

            return;
        }

		if (Input.GetButton(InputMap.ButtonB + (int) mPlayer.mPlayerID))
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
		if (((Time.timeSinceLevelLoad - mLastFireTime) > DURATION) && Input.GetAxis(InputMap.TriggerR + (int) mPlayer.mPlayerID) > 0)
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
		SPB.SetOwner((int) mPlayer.mPlayerID);
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
	
}
