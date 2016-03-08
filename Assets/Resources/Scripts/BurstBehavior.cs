using UnityEngine;
using System.Collections;

public class BurstBehavior : MonoBehaviour {


    #region privateVar
    private float timeLived = 0;
    private float speed = 6;
    private float dacayRate = .95f;
    #endregion

    // Use this for initialization
    void Start () {

        int randomInt = Random.Range (0, 2);
    }
    
    // Update is called once per frame
    void Update () {
      if (this.transform.localScale.x < .1f) {
          Destroy(this.gameObject);
      }
      transform.position += (speed * Time.smoothDeltaTime) * transform.up;
      this.transform.localScale *= dacayRate;
    }
}
