﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position_Setting : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = GameObject.Find("Empty").GetComponent<Transform>().position;
    }
}
