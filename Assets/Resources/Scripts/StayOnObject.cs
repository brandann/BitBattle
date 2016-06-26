using UnityEngine;
using System.Collections;
using witchplease;

public class StayOnObject : MonoBehaviour {
    public GameObject mTargetObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(mTargetObject == null)
        {
            return;
        }
        this.transform.position = new Vector3(
            mTargetObject.transform.position.x,
            mTargetObject.transform.position.y,
            -10);
	}
}
