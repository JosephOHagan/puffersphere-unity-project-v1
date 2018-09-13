using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//this script used to test alignment selection tasks. The marker and the target will turn blue if the task is complete
public class Task_Detector : MonoBehaviour {
    private float Hold_Time;
    private float Current_Time;
    private bool count;
    private bool isComplete;
    private bool isReported;
    public float Fix_Time;
    private float Current_Fix_Time;
    // Use this for initialization
    void Start () {
        count = false;
        isComplete = false;
        Current_Time = 0f;
        Hold_Time = 0.5f;

        isReported = false;
        Current_Fix_Time = 0f;
        //Debug.Log("Task Detector strat");
    }
	
	// Update is called once per frame
	void Update () {

        if (count==true && isComplete==false)
        {
            
            Current_Time += Time.deltaTime;
            //Debug.Log(Current_Time);
            if (Current_Time>= Hold_Time && isComplete == false) {
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

                GameObject current_marker = GameObject.FindWithTag("Marker");
                //offline test
                current_marker.GetComponent<SpriteRenderer>().color = Color.blue;
                isComplete = true;
                float time = GameObject.FindWithTag("Time").GetComponent<Marker_Generater>().TimeCount();
                Debug.Log("isComplete = true; Time is:"+ time+" Seconds");
                Debug.Log("Wait for 3 second to report accuracy");
                return;

            }
            return;

        }

        if (isComplete == true && isReported == false)
        {
            Current_Fix_Time += Time.deltaTime;
           // Debug.Log(Current_Fix_Time);
            if (Current_Fix_Time >= Fix_Time)
            {
                Vector3 position_dot = GameObject.FindWithTag("Marker").transform.position;
                Vector3 position_circle = GameObject.FindWithTag("Target").transform.position;
                //Debug.Log("position_dot:"+ position_dot+ ";position_circle:"+ position_circle);
                Vector3 delta = position_dot - position_circle;
                float distance = (float)Math.Sqrt(Math.Pow(delta.x, 2) + Math.Pow(delta.y, 2) + Math.Pow(delta.z, 2));
               // Debug.Log("distance:"+ distance);
                float r = GameObject.Find("Globe_Tracked").GetComponent<SpiralSphere>().radius + 0.004f;
                //Debug.Log("r"+r);
                float angle = 2*((float)Math.Asin(distance/(2*r))*180/Mathf.PI);
                isReported = true;
                Debug.Log("Task completed. The distance is: "+ angle+" grad");
                
                return;
            }
            return;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter");
        if (other.gameObject.tag=="Marker") {
            count = true;
            //Debug.Log("begin to count");
        }
    }


    void OnTriggerExit(Collider other)
    {
       // Debug.Log("OnTriggerExi");
        if (other.gameObject.tag == "Marker")
        {
            count = false;
            Current_Time = 0.0f;
            //Debug.Log("reset count");

        }
    }




}
