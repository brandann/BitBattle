﻿using UnityEngine;
using System.Collections;
using witchplease;

public class SimpleShootPrefab : MonoBehaviour {

	public GameObject Prefab;
	public float Durration;
	
	private float _LastShotTime;
	
	// Use this for initialization
	void Start () {
		_LastShotTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeSinceLevelLoad - _LastShotTime >= Durration)
		{
			var go = GameObject.Instantiate(Prefab);
			//go.AddComponent<SimpleMoveForward>();
			//var GOSimpleMoveForward = go.GetComponent<SimpleMoveForward>();
			
			//GOSimpleMoveForward.Target = this.transform.up * 100;
			_LastShotTime = Time.timeSinceLevelLoad;
            go.transform.position = this.transform.position;
            //go.transform.LookAt(this.transform.position + this.transform.forward);
            go.transform.up = this.transform.up;
		}
	}
}
