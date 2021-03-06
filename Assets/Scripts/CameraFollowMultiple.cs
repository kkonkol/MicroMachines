﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollowMultiple : MonoBehaviour {

    public string tagToFollow = "Car";
    public Vector3 visionAngle;
    public float standardDistance = 10;
    public float distanceMultiplier = 1;
    public float minimumDistance = 30;
    public float maximumDistance = 80;

	// Use this for initialization
	void Start () {
	
	}

    public void addObjectToFollowList(GameObject go)
    {
        //Do nothing
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        gameObject.transform.Rotate(new Vector3(1, 0, 0), visionAngle.x, Space.World);
        gameObject.transform.Rotate(new Vector3(0, 1, 0), visionAngle.y, Space.World);
        gameObject.transform.Rotate(new Vector3(0, 0, 1), visionAngle.z, Space.World);

        Vector3 direction = gameObject.transform.forward;

        Vector3 midPoint = new Vector3(0, 0, 0);

        GameObject[] objectsToFollow = GameObject.FindGameObjectsWithTag(tagToFollow);

        if (objectsToFollow.Length < 1)
        {
            return;
        }
        foreach (GameObject ob in objectsToFollow)
        {
            if (ob == null)
            {
                return;
            }
            if (ob.transform == null)
            {
                return;
            }
            midPoint = midPoint + ob.transform.position;
        }
        midPoint = midPoint / objectsToFollow.Length;

        if (midPoint.x == 0 && midPoint.y == 0 && midPoint.z == 0)
        {
            midPoint = new Vector3(-33.02f, 43.16f, -144.39f);
        }

        float distance = 0;

        foreach (GameObject ob1 in objectsToFollow)
        {
            foreach (GameObject ob2 in objectsToFollow)
            {
                if (!ob1.Equals(ob2))
                {
                    float dist = (ob1.transform.position - ob2.transform.position).magnitude;
                    distance = Mathf.Max(distance, dist);
                }
            }
        }

        distance = Mathf.Log(Mathf.Abs(distance - standardDistance));
        distance = distance * distanceMultiplier;
        distance = Mathf.Max(distance, minimumDistance);
        distance = Mathf.Min(distance, maximumDistance);

        Vector3 camPos = midPoint;

        gameObject.transform.position = camPos-direction*distance;

        Camera camera = (Camera) GameObject.Find("Main Camera").GetComponent<Camera>();
        GameObject parent = gameObject.transform.parent.gameObject;
        if (camera.orthographic)
        {
            parent.transform.position = camPos - direction * (distance + 50);
            gameObject.transform.position = camPos - direction * (distance + 50);
            camera.orthographicSize = distance;
        }
	}
}
