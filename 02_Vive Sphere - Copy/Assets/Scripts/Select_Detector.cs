using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Select_Detector : MonoBehaviour {
    private bool count;
    private bool isComplete;
    public float Hold_Time;
    private float Current_Time;
    

    //private GameObject Finger;
   // private Vector3 Index_position;
    // Use this for initialization
    void Start () {
        count = false;
        isComplete = false;
        Current_Time = 0f;
        //Finger = GameObject.Find("Controller (right)");
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 Current_Finger_Position = GameObject.Find("TouchManager").GetComponent<TouchScript.InputSources.TuioInput>().xyzPosition;
        Vector3 marker_Position = gameObject.transform.position;
        count = Is_in_Touch(Current_Finger_Position, marker_Position);


        if (count == true && isComplete == false)
        {

            Current_Time += Time.deltaTime;
            //Debug.Log(Current_Time);
            if (Current_Time >= Hold_Time && isComplete == false)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                isComplete = true;
                float time = GameObject.FindWithTag("Time").GetComponent<Marker_Generater>().TimeCount();
                
                Debug.Log("isComplete = true; Time: " + time + "second");
                return;

            }
            return;

        }
        
    }

    private bool Is_in_Touch(Vector3 Finger_Position, Vector3 Marker_Position)
    {
        bool isTouch;
        Vector3 delta = Finger_Position - Marker_Position;
        float distance = (float)Math.Sqrt(Math.Pow(delta.x, 2) + Math.Pow(delta.y, 2) + Math.Pow(delta.z, 2));
        // if (distance < radius + 0.01f && distance > radius - 0.01f)
        if (distance < 0.05f)
        {
            isTouch = true;
        }
        else
        {
            isTouch = false;
        }

        return isTouch;

    }
}
