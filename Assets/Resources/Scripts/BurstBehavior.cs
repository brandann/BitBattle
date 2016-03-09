using UnityEngine;
using System.Collections;

public class BurstBehavior : MonoBehaviour {

	public Vector2 RandomSpeedRange;
	public Vector2 RandomDecayRate;
	
    private float mSpeed;
    private float mDecay;

    // Use this for initialization
    void Start () {
        mSpeed = Random.Range (RandomSpeedRange[0], RandomSpeedRange[1]);
		mDecay = Random.Range (RandomDecayRate[0], RandomDecayRate[1]);
    }
    
    // Update is called once per frame
    void Update () {
      if (this.transform.localScale.x < .1f) {
          Destroy(this.gameObject);
      }
      transform.position += mSpeed * Time.smoothDeltaTime * transform.up;
      this.transform.localScale *= mDecay;
    }
}
