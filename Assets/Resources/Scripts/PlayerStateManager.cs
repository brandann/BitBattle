using UnityEngine;
using System.Collections;
using witchplease;

public class PlayerStateManager : MonoBehaviour
{

    private PlayerPowerupManager mPlayerPowerups;
    private PlayerFireManager mPlayerFire;
    private Player2AxisMovement mPlayerMovement;

    public GameObject mBurstPrefab;

    // PLAYER ID
    public ePlayerID mPlayerID;

    // REFRENCED GAMEOBJECTS
    public GameObject mGlobalGameObject;

    // Use this for initialization
    void Start()
    {
        mPlayerPowerups = this.GetComponent<PlayerPowerupManager>();
        mPlayerFire = this.GetComponent<PlayerFireManager>();
        mPlayerMovement = this.GetComponent<Player2AxisMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D c)
    {

    }

    #region DEATH_EVENTS
    // KILL THE PLAYER AND REPORT THE DEATH TO GLOBAL
    public void kill(ePlayerDeathEvents e)
    {
        bool death = false;
        switch (e)
        {
            case (ePlayerDeathEvents.Projectile):
                death = isKilledByProjectile();
                break;
            case (ePlayerDeathEvents.Lava):
                death = isKilledByLava();
                break;
        }

        if (death)
        {
            deactivatePowerups();

            var burstGO = GameObject.Instantiate(mBurstPrefab);
            burstGO.transform.position = this.transform.position;
            burstGO.GetComponent<BurstManager>().mColor = this.GetComponent<SpriteRenderer>().color;
            mPlayerMovement.ResetPosition();

            // REGESTER DEATH WITH THE SIMPLE GLOBAL
            Global.Death((int)mPlayerID);
        }
    }

    private void deactivatePowerups()
    {
        mPlayerPowerups.deactivateFastPowerup();
        mPlayerPowerups.deactivateInvinciblePowerup();
        mPlayerPowerups.deactivateFreezePowerup();
        mPlayerPowerups.deactivateShieldPowerup();
    }

    private bool isKilledByLava()
    {
        // RIGHT NOW LAVA KILLS YOU NO MATTER WHAT!
        return true;
    }

    private bool isKilledByProjectile()
    {
        if (mPlayerPowerups.mInvincibleIsActive)
        {
            // DO NOT HURT THE PLAYER
            mPlayerFire.mAvailShots++;
            Debug.Log("Kill blocked by Shield");
            return false;
        }
        else if (mPlayerPowerups.mShieldIsActive)
        {
            mPlayerPowerups.deactivateShieldPowerup();
            return false;
        }
        return true;
    }
    #endregion END_KILL
}
