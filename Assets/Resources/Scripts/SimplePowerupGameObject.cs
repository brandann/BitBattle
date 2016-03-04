﻿using UnityEngine;
using System.Collections;

public class SimplePowerupGameObject : MonoBehaviour {

    public GameObject mPlayer1;
    public GameObject mPlayer2;
    public GameObject mPlayer3;
    public GameObject mPlayer4;

    public enum ePowerups { Freeze, FastSpeed, Shield}
    public ePowerups mPowerup;

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            switch(mPowerup)
            {
                case (ePowerups.Freeze): // MAKE AS A FUNCTION
                    int id = (int)c.gameObject.GetComponent<PlayerTopDownMovement>().mPlayerID;
                    FreezePlayer(mPlayer1, id);
                    FreezePlayer(mPlayer2, id);
                    FreezePlayer(mPlayer3, id);
                    FreezePlayer(mPlayer4, id);
                    Destroy(this.gameObject);
                    break;
                case (ePowerups.FastSpeed): // MAKE AS A FUNCTION
                    c.gameObject.GetComponent<PlayerTopDownMovement>().activateSpeedPowerup(4, 2f);
                    Destroy(this.gameObject);
                    break;
                case (ePowerups.Shield): // MAKE AS A FUNCTION
                    c.gameObject.GetComponent<PlayerTopDownMovement>().activateSheildPowerup(8f);
                    Destroy(this.gameObject);
                    break;
            }
        }
    }

    void FreezePlayer(GameObject p, int id) // MAKE AS A HELPER FUNCTION
    {
        // SHOULD CALL GLOBAL OR SOME POWERUP MANAGER TO DISH OUT TO EACH PLAYER
        // WE DON'T WANT TO ALL POWERUPS TO HAVE A REFRENCE TO EACH PLAYER
        // JUST SEND THE ACTIVATED POWERUP AND THE PLAYERS ID TO A CENTRAL CLASS
        // TO SORT OUT WHAT TO DO...
        if((int)p.gameObject.GetComponent<PlayerTopDownMovement>().mPlayerID == id) { return; }
        p.gameObject.GetComponent<PlayerTopDownMovement>().activateSpeedPowerup(3, 0.3f);
    }
}
