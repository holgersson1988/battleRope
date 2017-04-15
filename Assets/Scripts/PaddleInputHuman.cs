    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Paddle))]
public class PaddleInputHuman : MonoBehaviour {

    public enum Player
    {
        Player1,
        Player2
    };

    Paddle paddle;
    public Player player;
    
    bool left = false;
    bool right = false;
    bool drawRope = false;
    bool releaseRope = false;

    // Use this for initialization
    void Start () {
        paddle = GetComponent<Paddle>();
	}
	
	// Update is called once per frame
	void Update() {

        bool lastDrawRope = drawRope;

        if (player == Player.Player1)
        {
            left = Input.GetKey(KeyCode.LeftArrow);
            right = Input.GetKey(KeyCode.RightArrow);
            drawRope = Input.GetKey(KeyCode.RightControl);

            releaseRope = (lastDrawRope && !drawRope);
        }
        else
        {
            left = Input.GetKey(KeyCode.A);
            right = Input.GetKey(KeyCode.D);
            drawRope = Input.GetKey(KeyCode.Space);

            releaseRope = (lastDrawRope && !drawRope);
        }
        

        paddle.HandleInput(left, right, drawRope, releaseRope);
    }
}
