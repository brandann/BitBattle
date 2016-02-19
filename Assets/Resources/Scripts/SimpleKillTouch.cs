using UnityEngine;
using System.Collections;

public class SimpleKillTouch : MonoBehaviour {

    // SIMPLE CLASS THAT JUST KILLS THE PLAYER

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            c.gameObject.GetComponent<PlayerTopDownMovement>().kill();
        }
    }
}
