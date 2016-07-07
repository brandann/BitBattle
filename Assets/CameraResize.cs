using UnityEngine;
using System.Collections;

public class CameraResize : MonoBehaviour {

    public GameObject[] Players;

    public float DISTANCE_MULTIPLIER;
	public float DISTANCE_ADDITION;
	public bool USE_XY_RESIZE;
    private const float CAMERA_MIN = 6;
    private const float CAMERA_MAX = 100;
    private const float CAMERA_PADDING = 2;
    private readonly float CAMERA_RATIO_DOWN;
    private readonly float CAMERA_RATIO_UP;
    private Camera mCamera;

	public CameraResize()
	{
		CAMERA_RATIO_DOWN = 10f/16f;
		CAMERA_RATIO_UP = 16f/10f;
	}
	
	// Use this for initialization
	void Start () 
	{
		mCamera = this.gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// SET POSITION OF THE CENTER OF THE CAMERA BETWEEN THE PLAYERS
		var pointBetweenPlayers = MidPointOfPlayers(Players);
		var pointBetweenCameraAndPlayersMid = GetMidPoint(pointBetweenPlayers, this.transform.position);
		pointBetweenCameraAndPlayersMid.z = -10;
		this.transform.position = pointBetweenCameraAndPlayersMid;
		
		// SET THE SIZE OF THE CAMERA TO FIT ALL PLAYRS
		// GET DISTANCES
		var xDist = Mathf.Abs(Players[0].transform.position.x - Players[1].transform.position.x);
		var yDist = Mathf.Abs(Players[0].transform.position.y - Players[1].transform.position.y);
		
		// GET NORMALIZED NUMBERS
		var xNorm = (xDist * CAMERA_RATIO_DOWN * .5f) + CAMERA_PADDING;
		var yNorm = (yDist * .5f) + CAMERA_PADDING;
		
		// GET MAX VALUE
		var max = Mathf.Max(xNorm, yNorm);
		var interpolate = (mCamera.orthographicSize + max) * .5f;
		mCamera.orthographicSize = Mathf.Clamp(interpolate, CAMERA_MIN, CAMERA_MAX);
	}

    private Vector3 GetMidPoint(Vector3 v1, Vector3 v2)
    {
        return (v1 + v2) / 2;
    }
    
    private Vector3 MidPointOfPlayers(GameObject[] go)
    {
    	var sum = Vector3.zero;
    	foreach(GameObject g in go)
    		sum += g.transform.position;
		return sum / go.Length;
	}
}
