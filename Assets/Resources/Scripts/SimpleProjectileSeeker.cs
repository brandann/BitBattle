using UnityEngine;
using System.Collections;
using witchplease;

/*
    THIS CODE IS NOT USED BY ANY GAME OBJECTS!

    #########################################################
    #                                                       #
    #       BE SURE TO DELETE THIS IF CODE GETS USED        #
    #                                                       #
    #########################################################
*/

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
