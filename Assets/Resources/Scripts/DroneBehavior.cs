using UnityEngine;
using System.Collections;

public class DroneBehavior : MonoBehaviour {
    public GameObject mBurstPrefab;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        print("Collide");
        if (c.gameObject.tag == "Player")
        {
            print("Collide With Player");
            c.gameObject.SendMessage("kill", PlayerStateManager.ePlayerDeathEvents.Projectile);
            var burstGO = GameObject.Instantiate(mBurstPrefab);
            burstGO.transform.position = this.transform.position;
            burstGO.GetComponent<BurstManager>().mColor = Color.white;
            Destroy(this.transform.parent.gameObject);
        }
    }
}
