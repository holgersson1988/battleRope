using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDampToward : MonoBehaviour {

    public bool setTargetToStartPos;
    public bool lockToYAxis;
    public Vector3 targetPosition;
    private Vector3 currentVelocity = Vector3.zero;

    private float timeToTarget = 1f;
    private float maxSpeed = 1f;


    private void Awake()
    {
        if (setTargetToStartPos)
        {
            targetPosition = transform.position;
        }
    }

    private void Update()
    {
        if (!lockToYAxis)
        {
            transform.position = Vector3.SmoothDamp(transform.position, 
                                                    targetPosition, 
                                                    ref currentVelocity, timeToTarget, maxSpeed, Utility.DeltaTime());
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, 
                                                    new Vector3(transform.position.x, targetPosition.y, transform.position.z), 
                                                    ref currentVelocity, timeToTarget, maxSpeed, Utility.DeltaTime());
        }
        
    }
}
