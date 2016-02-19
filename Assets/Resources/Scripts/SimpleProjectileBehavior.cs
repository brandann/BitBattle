using UnityEngine;
using System.Collections;

public class SimpleProjectileBehavior : MonoBehaviour {
    
    // ENDING LOCATION OF THE PROJECTILE
    private Vector3 Target = Vector3.zero;

    // PROJECTILE SPEED
    private float Speed = 10f;
	
	// Update is called once per frame
	void Update () {
        // IF THE TARGET IS NOT SET THEN DO NOTHING
        if (Target == Vector3.zero) { return; }

        // TURN TO FACE THE TARGET (CHANGE TARGET INTO A TRANSFORM AND THE PROJECTILE WILL HOME IN ON TARGET)
        transform.LookAt(transform.position + new Vector3(0, 0, 1), Target - transform.position);
        transform.position += transform.up * Speed * Time.deltaTime;

        // DESTROY THE PROJECTILE WHEN IT GETS TO ITS TARGET
        if ((Target - transform.position).magnitude < .1f)
        {
            Destroy(this.gameObject);
        }
    }

    // CAN BE CALLED FROM OUTSIDE TO INIT THE TARGET
    public void setTarget(Vector3 targ)
    {
        Target = targ;
    }
}
