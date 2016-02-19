using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    // IS THE PORTAL OBJECT A PORTAL ENTRANCE
    public bool mPortalEnter;

    // DESTINATION FOR PLAYER ENTERING THE PORTAL
    public GameObject mDestination;

    void OnCollisionEnter2D(Collision2D c)
    {
        // IF: THIS PORTAL IS AN ENTRANCE
        // IF: GAMEOBJECT TAG IS PLAYER
        if (mPortalEnter && (c.gameObject.tag == "Player"))
        {
            // CALL PLAYERTOPDOWNMOVEMENT TO TELEPORT THE PLAYER
            c.gameObject.GetComponent<PlayerTopDownMovement>().Teleport(mDestination);
        }
    }
}
