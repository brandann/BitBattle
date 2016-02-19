using UnityEngine;
using System.Collections;

public class SimpleRotation : MonoBehaviour {

    public int RotationDirection;
    public float RotationSpeed;

    private Vector3 mRotation;

	// Use this for initialization
	void Start () {
        mRotation = new Vector3(0, 0, -1 * RotationDirection * RotationSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(mRotation * Time.deltaTime);
	}
}
