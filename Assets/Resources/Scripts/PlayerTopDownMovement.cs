﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerTopDownMovement : MonoBehaviour {

    // PLAYER ID
    public enum ePlayerID { Player1 = 1, Player2 = 2, Player3 = 3, Player4 = 4}
    public ePlayerID mPlayerID;

    // MOVEMENT SPEEDS
    public float mForwardSpeed;
    public Vector2 mVelocity;

    // REFRENCED GAMEOBJECTS
    public GameObject mBullet;
    public GameObject mGlobalGameObject;
    public GameObject mCamera;

    // FIRING DELAY
    private float mLastFireTime;
    private const float DURATION = 0.25f;

    // STARTING LOCATION BASED OF UNITY LOCATION OF GAMEOBJECT
    private Vector3 mStartingPosition;

	// Use this for initialization
	void Start () {
        // INIT VARIABLES
        mVelocity = new Vector2(0, 0);
        mStartingPosition = transform.position;
	}
	
	// Update is called once per frame
    void Update()
    {
        
        // GET ID TO REDUCE CASTING IN UPDATE
        int ID = (int)mPlayerID;

        // GET THE VERICAL VELOCITY FROM INPUT AND ANDJUST IT TO SPEEDS
        mVelocity.y = Input.GetAxis(InputMap.VerticalL + ID);
        
        // GET THE HORIZONTAL VELOCITY FROM INPUT AND ADJUST IT TO SPEEDS
        mVelocity.x = Input.GetAxis(InputMap.HorizontalL + ID);

        if (mVelocity.magnitude != 0)
        {
            transform.LookAt(transform.position + new Vector3(0, 0, 1), mVelocity);
        }
        
        // SET THE RIGIDBIDY2D VELOCITY
        GetComponent<Rigidbody2D>().velocity = (transform.up * mVelocity.magnitude * mForwardSpeed);

        // HANDLE SHOOTING
        Fire();

        if (null != mCamera) { mCamera.transform.position = this.transform.position + new Vector3(0, 0, -10); }
    }

    
    private void Fire()
    {
        // IF: DURATION HAS PASSED
        // IF: RIGHT TRIGGER IS PRESSED
        if (((Time.timeSinceLevelLoad - mLastFireTime) > DURATION) && Input.GetAxis(InputMap.TriggerR + (int)mPlayerID) > 0)
        {
            // RESET THE TIME OF THE THIS SHOOT BEING FIRED
            mLastFireTime = Time.timeSinceLevelLoad;
            
            // CREATE THE RIGHT PROJECTILE
            var g1 = GameObject.Instantiate(mBullet);
            g1.transform.position = this.transform.position + (this.transform.right*0.6f);
            g1.GetComponent<SimpleProjectileBehavior>().setTarget(this.transform.position + (this.transform.up * 20));

            // CREATE THE LEFT PROJECTILE
            var g2 = GameObject.Instantiate(mBullet);
            g2.transform.position = this.transform.position - (this.transform.right*0.6f);
            g2.GetComponent<SimpleProjectileBehavior>().setTarget(this.transform.position + (this.transform.up * 20));
            
            // CREATE THE CENTER PROJECTILE
            var g3 = GameObject.Instantiate(mBullet);
            g3.transform.position = this.transform.position + (this.transform.up * 1f);
            g3.GetComponent<SimpleProjectileBehavior>().setTarget(this.transform.position + (this.transform.up * 20));
        }
    }

    public void Teleport(GameObject go)
    {
        // MOVE MY POSITION TO THE TELEPORT DESTINATION
        this.transform.position = go.transform.position;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        // IF: COLLIDER TAG IS A PROJECTILE
        if(c.tag == "Projectile")
        {
            // PROJECTILE KILLS ME
            kill();
        }
    }

    public void kill()
    {
        this.transform.position =mStartingPosition;

        // REGESTER DEATH WITH THE SIMPLE GLOBAL
        mGlobalGameObject.GetComponent<SimpleGlobal>().Death((int)mPlayerID);
    }
}
