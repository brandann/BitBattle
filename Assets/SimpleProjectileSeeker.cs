using UnityEngine;
using System.Collections;

public class SimpleProjectileSeeker : MonoBehaviour {

    public GameObject mPlayer;

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }
}
