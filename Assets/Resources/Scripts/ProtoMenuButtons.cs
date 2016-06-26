using UnityEngine;
using System.Collections;
using witchplease;

public class ProtoMenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SingleCamera()
    {
        Application.LoadLevel("game");
    }

    public void MultiCamera()
    {
        Application.LoadLevel("GameZoomedCamera");
    }
}
