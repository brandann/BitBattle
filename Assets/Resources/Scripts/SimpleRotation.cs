using UnityEngine;
using System.Collections;

public class SimpleRotation : MonoBehaviour {

    public int RotationDirection;
    public float RotationSpeed;

    private bool mRotationActive = true;

    private Vector3 mRotation;

	// Use this for initialization
	void Start () {
        setRotation(RotationDirection, RotationSpeed, true);
    }
	
	// Update is called once per frame
	void Update () {
        if (mRotationActive)
        {
            this.transform.Rotate(mRotation * Time.deltaTime);
        }
	}

    public void setRotation(int direction, float speed, bool active)
    {
        mRotation = new Vector3(0, 0, -1 * direction * speed);
        mRotationActive = active;
    }

    public void SetRotationEnabled(bool active)
    {
        mRotationActive = active;
    }
    
}
