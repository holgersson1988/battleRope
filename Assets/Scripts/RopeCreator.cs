﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCreator : MonoBehaviour {

    public Rope ropePrefab;
    public RopeSection ropeSectionPrefab;
    Paddle creatorPaddle;
    Vector2 ropeEnds;
    LineRenderer line;
    public int index = 0;
    float usedPaint = 0f;
    float paintAvailable = 0f;

	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
        line.material.SetColor("_EmissionColor", creatorPaddle.ropeColor);
    }

	// Update is called once per frame
	void Update () {
        Vector3 paddlePos = creatorPaddle.transform.position;
        if (paddlePos.x < ropeEnds.x)
        {
            ropeEnds.x = paddlePos.x;
        }
        else if (paddlePos.x > ropeEnds.y)
        {
            ropeEnds.y = paddlePos.x;
        }

        usedPaint = ropeEnds.y - ropeEnds.x;
        if (usedPaint > paintAvailable)
        {

        }

        line.SetPosition(0, new Vector3(ropeEnds.x, transform.position.y, 0));
        line.SetPosition(1, new Vector3(ropeEnds.y, transform.position.y, 0));
	}

    public void SetupRopeCreator(float xpos, Paddle paddle)
    {
        ropeEnds = new Vector2(xpos, xpos);
        creatorPaddle = paddle;
        paintAvailable = paddle.paint;
    }

    public void Release()
    {   
        // Create Rope Container
        Rope ropeContainer = Instantiate(ropePrefab, transform.position, Quaternion.identity) as Rope;
        ropeContainer.name = "Rope " + index;
        

        // Create First section and remove its joint
        RopeSection lastSection = Instantiate(ropeSectionPrefab, new Vector3(ropeEnds.x, transform.position.y, 0), Quaternion.identity) as RopeSection;
        Destroy(lastSection.GetComponent<ConfigurableJoint>());
        // Connect to Rope Container
        ropeContainer.AddSection(lastSection);

        // Count variables and section length
        float offset = 0f;
        float sectionLength = 0.25f;

        // Calculate number of sections based on draw length
        float distance = ropeEnds.y - ropeEnds.x;
        int num = Mathf.CeilToInt(distance / sectionLength);
        while (num > 0)
        {
            // Count
            num--;
            offset += sectionLength;

            // Add new Section
            RopeSection newSection = Instantiate(ropeSectionPrefab, new Vector3(ropeEnds.x + offset, transform.position.y, 0), Quaternion.identity) as RopeSection;
            newSection.ConnectTo(lastSection);

            // Add to Rope Container
            ropeContainer.AddSection(newSection);

            // Reset for next iteration
            lastSection = newSection;
        }

        ropeContainer.Setup(creatorPaddle.ropeColor, creatorPaddle.gravityDirection, creatorPaddle.gravity);

        if (creatorPaddle.gameObject.layer == LayerMask.NameToLayer("Player1Paddle"))
            ropeContainer.SetLayerRecursively(ropeContainer.gameObject, LayerMask.NameToLayer("Player1Ropes"));
        else
            ropeContainer.SetLayerRecursively(ropeContainer.gameObject, LayerMask.NameToLayer("Player2Ropes"));

        Destroy(gameObject);
    }
}
