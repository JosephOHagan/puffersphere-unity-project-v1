using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//This script generate maekers on the sphere
public class Marker_Generater : MonoBehaviour {
    public GameObject marker;
    private Vector2[] positionsV2;
    private Vector3[] positionsV3;
    private float radius;
    private Vector3 center;
    private int n = 0;
    private float Current_Time;

    public int index()
    {
        return n;

    }

    public float TimeCount()
    {

        return Current_Time;


    }
    // Use this for initialization
    void Start () {
        positionsV2 = new Vector2[6];

        // define points with (longtitude,latitude)
        positionsV2[0] = new Vector2(121.522179f, 31.267401f);
        positionsV2[1] = new Vector2(11.576124f, 48.137154f);
        positionsV2[2] = new Vector2(115.188919f, -8.409518f);
        positionsV2[3] = new Vector2(0f, 90f);
        positionsV2[4] = new Vector2(90f, 0f);
        positionsV2[5] = new Vector2(90f, 90f); 
        positionsV3 = TransformCoordinateData(positionsV2);
        Current_Time = 0f;
    }

    // Update is called once per frame
    void Update () {
        //when press mouse, detele last marker and generate new one
        if (Input.GetMouseButtonDown(0)) {
            Current_Time = 0f;
            //stop at the last point
            if (n == positionsV3.Length) {
                Debug.Log("Last Point");
                return;
            }
            //destroy last marker
            if (n != 0) {
                Destroy(GameObject.Find((n-1).ToString()));
            }
            GameObject Current_Marker;
            center = GameObject.Find("Empty").transform.position;


            Current_Marker = Instantiate(marker, new Vector3(positionsV3[n].x + center.x, positionsV3[n].y + center.y, positionsV3[n].z + center.z), Quaternion.identity) as GameObject; 
            Current_Marker.transform.LookAt(center,Vector3.up);
            Current_Marker.transform.parent = gameObject.transform;
            
            Current_Marker.name = n.ToString();
            n++;
        }
        Current_Time += Time.deltaTime;
    }


    public Vector3[] TransformCoordinateData(Vector2[] SphericalCoordinate)
    {

        Vector3[] coordinatesV3 = new Vector3[SphericalCoordinate.Length];
        radius = GameObject.Find("Globe_Tracked").GetComponent<SpiralSphere>().radius+0.004f;
       
        for (int c = 0; c < SphericalCoordinate.Length; c++)
        {
            float longitude = SphericalCoordinate[c].x * Mathf.PI / 180;//fita
            float latitude = (90.0f - SphericalCoordinate[c].y) * Mathf.PI / 180;//theta
            float X = (float)radius * (float)Math.Sin((float)latitude) * (float)Math.Cos((float)longitude);
            float Y = (float)radius * (float)Math.Cos((float)latitude);
            float Z = (float)radius * (float)Math.Sin((float)latitude) * (float)Math.Sin((float)longitude);
            coordinatesV3[c] = new Vector3(X, Y, Z);
        }
        return coordinatesV3;
    }

    
}
