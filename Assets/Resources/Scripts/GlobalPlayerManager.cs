using UnityEngine;
using System.Collections;

public class GlobalPlayerManager : MonoBehaviour {

    // STATIC GLOBAL PLAYERMANAGER OBJECT
    private static PlayerManager mPlayerManager;
    public static PlayerManager playerManager
    {
        get
        {
            // IF THE PLAYER MANAGER IS NULL THEN CREATE A NEW STATIC INSTANCE OF
            // PLAYER MANAGER, OTHERWISE RETURN THE CURRENT ACTIVE PLAYER MANAGER
            if (mPlayerManager == null) { mPlayerManager = new PlayerManager(); }
            return mPlayerManager;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
