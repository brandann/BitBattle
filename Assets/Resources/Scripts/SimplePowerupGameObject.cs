﻿using UnityEngine;
using System.Collections;

public class SimplePowerupGameObject : MonoBehaviour {

    public GameObject mPlayer1;
    public GameObject mPlayer2;
    public GameObject mPlayer3;
    public GameObject mPlayer4;

    public enum ePowerups { Freeze, FastSpeed}
    public ePowerups mPowerup;

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            switch(mPowerup)
            {
                case (ePowerups.Freeze):
                    int id = (int)c.gameObject.GetComponent<PlayerTopDownMovement>().mPlayerID;
                    FreezePlayer(mPlayer1, id);
                    FreezePlayer(mPlayer2, id);
                    FreezePlayer(mPlayer3, id);
                    FreezePlayer(mPlayer4, id);
                    Destroy(this.gameObject);
                    break;
                case (ePowerups.FastSpeed):
                    c.gameObject.GetComponent<PlayerTopDownMovement>().activateSpeedPowerup(4, 2f);
                    Destroy(this.gameObject);
                    break;
            }
        }
    }

    void FreezePlayer(GameObject p, int id)
    {
        if((int)p.gameObject.GetComponent<PlayerTopDownMovement>().mPlayerID == id) { return; }
        p.gameObject.GetComponent<PlayerTopDownMovement>().activateSpeedPowerup(3, 0.3f);
    }
}