using UnityEngine;
using System.Collections;

public class SimpleProjectileBehavior : MonoBehaviour {
    
    // ENDING LOCATION OF THE PROJECTILE
    private Vector3 Target = Vector3.zero;

    // PROJECTILE SPEED
    private float Speed = 10f;

    public bool mAvailableForPickup = false;
    private float mSpeedMod = 1;

    private bool mActive = true;

    private int mOwnerID;

	// Update is called once per frame
	void Update () {
        // IF THE TARGET IS NOT SET THEN DO NOTHING
        if (Target == Vector3.zero) { return; }

        if(!mActive) { return; }

        // TURN TO FACE THE TARGET (CHANGE TARGET INTO A TRANSFORM AND THE PROJECTILE WILL HOME IN ON TARGET)
        transform.LookAt(transform.position + new Vector3(0, 0, 1), Target - transform.position);
        transform.position += transform.up * Speed * Time.deltaTime * mSpeedMod;

        // DESTROY THE PROJECTILE WHEN IT GETS TO ITS TARGET
        if ((Target - transform.position).magnitude < .1f)
        {
            //Destroy(this.gameObject);
            Stop();
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            collideWithPlayer(c.gameObject.GetComponent<PlayerTopDownMovement>());
        }
        else
        {
            /*
                TODO

                THE PROJECTILE SHOULD REFLECT OFF THE SURFCE. RIGHT NOW I HAVE A HACKY INCORRECT REFLECTION
                IMPLEMENTED THAT ONLY REFLECTS NEG FRONT VEC
                SHOULE BE UPDATED WITH NORMALS, FRONTS, DOTS AND CROSS MATH TO ACURATLY REFLECT THE PROJECILE

                PROJECTILE SHOULD BOUNCE MORE THAN 1 TIME? MAKE CODE ABLE TO SUPPORT A VARIABLE NUMBER OF WALL BOUNCES.
                CAN BE IN A STRUCT THAT CONTAINS THE TARGET, SPEED, BOUNCES, OWNER, ETC.
            */

            //var v = Vector3.Reflect(this.transform.up, c.transform.up);
            var v = this.transform.up * -1;
            Target = this.transform.position + v;
        }
    }

    private void collideWithPlayer(PlayerTopDownMovement go)
    {
        if ((int)go.mPlayerID == mOwnerID)
        {
            return; // DO NOTHING, THIS IS MY OWN PROJECTILE
        }

        // STOP AND RECYCLE TO PROJECTILE
        if (mAvailableForPickup)
        {
            // AVAILABLE PROJECTILE GETS PICKED UP BY PLAYER
            go.gameObject.GetComponent<PlayerFireManager>().PickupBullet();
            Destroy(this.gameObject);
        }
        else 
        {
            // ACTIVE PROJECTILE DIES WHEN COLLISDES WITH A PLAYER
            go.kill(PlayerTopDownMovement.ePlayerDeathEvents.Projectile);  // PROJECTILE KILLS ME
            Destroy(this.gameObject);
        }
    }

    // CAN BE CALLED FROM OUTSIDE TO INIT THE TARGET
    public void setTarget(Vector3 targ)
    {
        Target = targ;
    }

    public void Stop()
    {
        //var c = this.GetComponent<CircleCollider2D>();
        //c.radius = c.radius * 2;
        mSpeedMod = 0;
        mAvailableForPickup = true;
        this.gameObject.GetComponent<SimpleRotation>().SetRotation(1, 100, true);
        //print("stop");
        mActive = false;
        mOwnerID = -1;
        //this.gameObject.GetComponentInChildren<PolygonCollider2D>().enabled = false;
        //this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    public void SetOwner(int id)
    {
        mOwnerID = id;
    }
}
