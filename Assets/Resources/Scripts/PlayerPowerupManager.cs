using UnityEngine;
using System.Collections;
using witchplease;

public class PlayerPowerupManager : MonoBehaviour {

	// PUBLIC JUST FOR TESTING
	private bool FreezeActive = false;
	private bool FastActive = false;
	private bool InvincibleActive = false;
	private bool ShieldActive = false;
	
	public bool mFreezeIsActive{
		private set{FreezeActive = value;}
		get{return FreezeActive;}
	}
	public bool mInvincibleIsActive{
		private set{InvincibleActive = value;}
		get{return InvincibleActive;}
	}
	public bool mFastIsActive{
		private set{FastActive = value;}
		get{return FastActive;}
	}
	public bool mShieldIsActive{
		private set{ShieldActive = value;}
		get{return ShieldActive;}
	}
	
	public GameObject mInvinciblePrefab;
	private GameObject mInvincibleObject;
	
	public GameObject mFastPrefab;
	private GameObject mFastObject;
	
	public GameObject mShieldPrefab;
	private GameObject mShieldObject;

    private Player2AxisMovement mPlayerMovement;
	
	// Use this for initialization
	void Start () {
        mPlayerMovement = this.GetComponent<Player2AxisMovement>();
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
		float prevSpeedMod = mPlayerMovement.mSpeedMod;             // SAVE THE PREV SPEED MOD FOR RETIEIVAL AFTER MOD IS FINISHED
		mPlayerMovement.mSpeedMod = fm.mod;                         // SET THE NEW SPEED MOD
		yield return new WaitForSeconds(fm.time);   // WAIT FOR THE MOD DURATION TO FINISH
		if(this.mFastIsActive){
			deactivateFastPowerup(prevSpeedMod);
		}
	}
	
	public void deactivateFastPowerup(float prev = 1)
	{
		Destroy(mFastObject);
		mPlayerMovement.mSpeedMod = prev;
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
		float prevSpeedMod = mPlayerMovement.mSpeedMod;             // SAVE THE PREV SPEED MOD FOR RETIEIVAL AFTER MOD IS FINISHED
		mPlayerMovement.mSpeedMod = fm.mod;                         // SET THE NEW SPEED MOD
		yield return new WaitForSeconds(fm.time);   // WAIT FOR THE MOD DURATION TO FINISH
		if(this.mFreezeIsActive){
			deactivateFreezePowerup(prevSpeedMod);
		}
	}
	
	public void deactivateFreezePowerup(float prev = 1)
	{
		mPlayerMovement.mSpeedMod = prev;                			// RETURN THE MOD TO ITS PREV MOD
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
	
	public void deactivateInvinciblePowerup()
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
	
	public void deactivateShieldPowerup()
	{
		Destroy(mShieldObject);
		mShieldIsActive = false;
		Debug.Log("deactivateShieldPowerup");
	}
	#endregion END_SHIELD
}
