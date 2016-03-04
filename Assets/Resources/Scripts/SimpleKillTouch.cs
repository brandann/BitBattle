using UnityEngine;
using UnityEditor;
using System.Collections;

public class SimpleKillTouch : MonoBehaviour {

    public LayerMask mLayerMask;

    // mLayerMask CONTAINS THE LAYERS THAT WILL BE DESTROYED IF COLLIDED WITH
    // 2^this.gameObject.layer TO GET THE GO.LAYER UP TO THE mLayerMask POWER
    void OnCollisionEnter2D(Collision2D c)
    {
        if ((mLayerMask.value & (int)Mathf.Pow(2f, (float)c.gameObject.layer)) != 0)
        {
            c.gameObject.GetComponent<PlayerTopDownMovement>().kill(PlayerTopDownMovement.ePlayerDeathEvents.Lava);
            //Destroy(c.gameObject);
        }
    }
}
