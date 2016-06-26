using UnityEngine;
using System.Collections;
using witchplease;

public class SimpleMoveForward : MonoBehaviour {

	public Vector3 Target = Vector3.zero;
	public float Speed;
	public GameObject mBurstPrefab;
	public float DeathDelay = Mathf.Infinity;
	private float _StartTime;
	
	// Use this for initialization
	void Start () {
		_StartTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		// TURN TO FACE THE TARGET (CHANGE TARGET INTO A TRANSFORM AND THE PROJECTILE WILL HOME IN ON TARGET)
		transform.LookAt(transform.position + new Vector3(0, 0, 1), Target - transform.position);
		transform.position += transform.up * Speed * Time.deltaTime;
		
		if(Time.timeSinceLevelLoad - _StartTime > DeathDelay)
		{
			Destroy(this.gameObject);
		}
	}
	
	void OnCollisionEnter2D(Collision2D c)
	{
        if (c.gameObject.tag == "platform")
        {
            var burstGO = GameObject.Instantiate(mBurstPrefab);
            burstGO.transform.position = this.transform.position;
            burstGO.GetComponent<BurstManager>().mColor = this.GetComponent<SpriteRenderer>().color;
            Destroy(this.gameObject);
        }
	}

    public void DoHitAPlayer()
    {
        var burstGO = GameObject.Instantiate(mBurstPrefab);
        burstGO.transform.position = this.transform.position;
        burstGO.GetComponent<BurstManager>().mColor = this.GetComponent<SpriteRenderer>().color;
        Destroy(this.gameObject);
    }
}
