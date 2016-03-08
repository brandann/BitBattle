using UnityEngine;
using System.Collections;

public class BurstManager : MonoBehaviour {

    public GameObject burstPrefab;

    #region privateVar
    //private float timeElapsed;
    public float duration = 3;
    #endregion

    // Use this for initialization
    void Start () {
		for(int i = 0; i < duration; i++)
		{
				makeBurstPoint ();
		}
    }

    // Update is called once per frame
    void Update () {
        //timeElapsed++;
        //if ((Time.timeSinceLevelLoad - timeElapsed) > duration) {
        //    Destroy(this.gameObject);
        //}
        //makeBurstPoint ();
    }

    private void makeBurstPoint() {
    
        GameObject e = Instantiate(burstPrefab) as GameObject;
        BurstBehavior spawnedEnemy = e.GetComponent<BurstBehavior>();

        if(spawnedEnemy != null) {
          e.transform.position = this.transform.position;
          spawnedEnemy.transform.Rotate(new Vector3(0,0,Random.Range(0,360)));
        }
    }
}
