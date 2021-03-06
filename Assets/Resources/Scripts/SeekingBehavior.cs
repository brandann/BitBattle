﻿using UnityEngine;
using System.Collections;
using witchplease;

public class SeekingBehavior : MonoBehaviour {

    public GameObject mOther;
    public ePlayerID owner;

    public float mSpeed;
    public float mRotation;

    private 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(mOther == null)
        {
            transform.position += transform.up * (mSpeed * .5f * Time.smoothDeltaTime);
            return;
        }
        var LR = NMath.GetLeftRight(this.transform, mOther.transform);
        var FB = NMath.GetFrontBack(this.transform, mOther.transform);
        var dir = (LR == NMath.LeftRight.Left) ? -1 : 1;
        transform.Rotate(Vector3.forward, dir * -1 * (mRotation * Time.smoothDeltaTime));
        transform.position += transform.up * (mSpeed * Time.smoothDeltaTime);
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player" && c.gameObject.GetComponent<PlayerStateManager>().mPlayerID != owner)
        {
            mOther = c.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if(mOther == c.gameObject)
            mOther = null;
    }

    public void setPlayer(ePlayerID id)
    {
        owner = id;
        this.GetComponentInChildren<DroneBehavior>().owner = id;
    }

    public void SetTargetPosition(Vector3 Target)
    {
        transform.LookAt(transform.position + new Vector3(0, 0, 1), Target + transform.position);
    }
}
