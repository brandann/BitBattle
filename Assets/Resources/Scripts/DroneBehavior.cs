using UnityEngine;
using System.Collections;
using witchplease;

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
        if (c.gameObject.tag == "Player")
        {
            print("Collide");
            print("Collide With Player");
            c.gameObject.SendMessage("kill", ePlayerDeathEvents.Projectile);
            var burstGO = GameObject.Instantiate(mBurstPrefab);
            burstGO.transform.position = this.transform.position;
            burstGO.GetComponent<BurstManager>().mColor = Color.white;
            Destroy(this.transform.parent.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.tag == "platform")
        {
            var burstGO = GameObject.Instantiate(mBurstPrefab);
            burstGO.transform.position = this.transform.position;
            burstGO.GetComponent<BurstManager>().mColor = this.transform.parent.GetComponent<SpriteRenderer>().color;
            Destroy(this.transform.parent.gameObject);
        }

        if (c.gameObject.tag == "Player")
        {
            print("Collide");
            print("Collide With Player");
            c.gameObject.SendMessage("kill", ePlayerDeathEvents.Projectile);
            var burstGO = GameObject.Instantiate(mBurstPrefab);
            burstGO.transform.position = this.transform.position;
            burstGO.GetComponent<BurstManager>().mColor = Color.white;
            Destroy(this.transform.parent.gameObject);
        }
    }
}
