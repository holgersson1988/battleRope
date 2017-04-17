    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Paddle))]
public class PaddleInputAI : PaddleInput {

    Paddle paddle;
    
    bool left = false;
    bool right = false;
    int drawRope = -1;
    bool releaseRope = false;

    float time = 0;
    float timeInterval = 0.5f;


    // Use this for initialization
    void Start () {
        paddle = GetComponent<Paddle>();

        left = false;
        right = false;
        drawRope = -1;
    }
	
	// Update is called once per frame
	void Update() {

        time += Time.deltaTime;
        if (time > timeInterval)
        {
            time -= timeInterval;
            timeInterval = Random.Range(0.4f, 0.65f);

            if (left)
            {
                left = false;
                right = true;
            }
            else
            {
                left = true;
                right = false;
            }
        }

        int lastDrawRope = drawRope;
        releaseRope = (lastDrawRope != -1 && drawRope == -1);

        paddle.HandleInput(left, right, drawRope, releaseRope);
    }


}
