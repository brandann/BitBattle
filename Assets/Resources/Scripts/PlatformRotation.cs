using UnityEngine;
using System.Collections;
using witchplease;

public class PlatformRotation : MonoBehaviour {
	
	public float dir;
	private float speed;
	public float X, Y, Z;

	// Use this for initialization
	void Start () {
		speed = Random.Range(100,150);
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate((new Vector3(X,Y,Z)), dir * (speed * Time.deltaTime));
		
	}
}
