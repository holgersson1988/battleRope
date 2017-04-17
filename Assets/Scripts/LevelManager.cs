using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public Paddle paddle1;
    public Paddle paddle2;

    public Color player1Color;
    public Color player2Color;
    public Color goalColor;

    public GoalArea goal1;
    public GoalArea goal2;


    public int player1Score;
    public int player2Score;

    

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoalScored(Paddle losingPaddle)
    {
        int scorePerGoal = 10;
        if (losingPaddle.player == Utility.Player.Player1)
        {
            player2Score += scorePerGoal;
        }
        else
        {
            player1Score += scorePerGoal;
        }
        ResetLevel();
    }

    public void ResetLevel()
    {
        Rope[] ropes = FindObjectsOfType<Rope>();

        int size = ropes.Length;
        for (int i = size-1; i >= 0; i--)
        {
            Destroy(ropes[i].gameObject);
        }

        paddle1.ResetLevel();
        paddle2.ResetLevel();
    }
}
