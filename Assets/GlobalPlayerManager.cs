using UnityEngine;
using System.Collections;

public class GlobalPlayerManager : MonoBehaviour {

    private PlayerManager mPlayerManager;
    public PlayerManager playerManager
    {
        get
        {
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
