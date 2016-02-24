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
        var v = this.transform.up * -1;
        Target = this.transform.position + v;
        //Stop();
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
        this.gameObject.GetComponent<SimpleRotation>().setRotation(1, 100, true);
        print("stop");
        mActive = false;
    }
}
