using UnityEngine;
using System.Collections;
using witchplease;

public class SimpleKillTouch : MonoBehaviour {

    public LayerMask mLayerMask;
    private ePlayerID owner;
    public ePlayerDeathEvents DeathEvent;

    // mLayerMask CONTAINS THE LAYERS THAT WILL BE DESTROYED IF COLLIDED WITH
    // 2^this.gameObject.layer TO GET THE GO.LAYER UP TO THE mLayerMask POWER
    void OnCollisionEnter2D(Collision2D c)
    {
        if ((mLayerMask.value & (int)Mathf.Pow(2f, (float)c.gameObject.layer)) != 0)
        {
            if(c.gameObject.tag == "Player" && owner != c.gameObject.GetComponent<PlayerStateManager>().mPlayerID)
            {
                c.gameObject.GetComponent<PlayerStateManager>().kill(DeathEvent);
                var SMF = this.GetComponent <SimpleMoveForward>();
                if(null != SMF)
                {
                    SMF.DoHitAPlayer(c.gameObject.GetComponent<SpriteRenderer>().color);
                }
            }
        }
    }

    public void setPlayer(ePlayerID id)
    {
        owner = id;
    }
}
