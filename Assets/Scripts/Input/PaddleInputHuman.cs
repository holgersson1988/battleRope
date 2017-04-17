    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Paddle))]
public class PaddleInputHuman : PaddleInput
{
    Paddle paddle;
    
    bool left = false;
    bool right = false;
    int drawRope = -1;
    bool releaseRope = false;

    KeyCode leftKey1 = KeyCode.LeftArrow;
    KeyCode rightKey1 = KeyCode.RightArrow;
    KeyCode drawKey1_1 = KeyCode.RightControl;
    KeyCode drawKey2_1 = KeyCode.RightShift;

    KeyCode leftKey2 = KeyCode.A;
    KeyCode rightKey2 = KeyCode.D;
    KeyCode drawKey1_2 = KeyCode.LeftControl;
    KeyCode drawKey2_2 = KeyCode.LeftShift;


    // Use this for initialization
    void Start () {
        paddle = GetComponent<Paddle>();
	}
	
	// Update is called once per frame
	void Update() {

        int lastDrawRope = drawRope;

        if (paddle.player == Utility.Player.Player1)
        {
            left = Input.GetKey(leftKey1);
            right = Input.GetKey(rightKey1);

            
            if (drawRope == -1)
            {
                if (Input.GetKey(drawKey1_1))
                {
                    drawRope = 0;
                }
                else if (Input.GetKey(drawKey2_1))
                {
                    drawRope = 1;
                }
            }
            else
            {
                if (drawRope == 0 && !Input.GetKey(drawKey1_1))
                    drawRope = -1;
                else if (drawRope == 1 && !Input.GetKey(drawKey2_1))
                    drawRope = -1;
            }

            releaseRope = (lastDrawRope != -1 && drawRope == -1);
        }
        else
        {
            left = Input.GetKey(leftKey2);
            right = Input.GetKey(rightKey2);

            drawRope = -1;

            if (drawRope == -1)
            {
                if (Input.GetKey(drawKey1_2))
                {
                    drawRope = 0;
                }
                else if (Input.GetKey(drawKey2_2))
                {
                    drawRope = 1;
                }
            }
            else
            {
                if (drawRope == 0 && !Input.GetKey(drawKey1_2))
                    drawRope = -1;
                else if (drawRope == 1 && !Input.GetKey(drawKey2_2))
                    drawRope = -1;
            }


            releaseRope = (lastDrawRope != -1 && drawRope == -1);
        }
        

        paddle.HandleInput(left, right, drawRope, releaseRope);
    }
}
