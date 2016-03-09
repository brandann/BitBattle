using UnityEngine;
using System.Collections;

public class BurstManager : MonoBehaviour {

    public GameObject mBurstPrefab;
    public float mBurstEmitterCount;

    // Use this for initialization
    void Start () {
		for(int i = 0; i < mBurstEmitterCount; i++){
			makeBurstPoint ();
		}
    }

    private void makeBurstPoint() {
		GameObject burstGO = Instantiate(mBurstPrefab) as GameObject;
		burstGO.transform.position = this.transform.position;
		burstGO.transform.Rotate(new Vector3(0,0,Random.Range(0,360)));
    }
}
