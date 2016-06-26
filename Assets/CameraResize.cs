using UnityEngine;
using System.Collections;

public class CameraResize : MonoBehaviour {

    public GameObject[] Players;

    public float DISTANCE_MULTIPLIER = 1.5f;
    public float DISTANCE_ADDITION = 1;
    private const float CAMERA_MIN = 6;
    private const float CAMERA_MAX = 15;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        // set center of camera
        var center = Get2MidPoint(Players[0].transform.position, Players[1].transform.position);
        center.z = -10;
        this.transform.position = center;
        
        // set size of camera
        var dist = Get2Distance(Players[0].transform.position, Players[1].transform.position) * 0.5f;
        this.GetComponent<Camera>().orthographicSize = Mathf.Clamp(dist * DISTANCE_MULTIPLIER + DISTANCE_ADDITION, CAMERA_MIN, CAMERA_MAX);
	}

    private Vector3 Get2MidPoint(Vector3 v1, Vector3 v2)
    {
        return (v1 + v2) / 2;
    }

    private float Get2Distance(Vector3 v1, Vector3 v2)
    {
        return (v1 - v2).magnitude;
    }
}
