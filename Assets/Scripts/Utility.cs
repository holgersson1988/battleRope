using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility {
    
    public enum Player
    {
        Player1, 
        Player2
    };

    public static Color player1Color;
    public static Color player2Color;
    public static Color neutralColor;


    public static float DeltaTime()
    {
        return Time.deltaTime;
    }

    /// <summary>
    /// Returns the Vector3 that points from "from" to "to"
    /// </summary>
    /// <returns>The to.</returns>
    /// <param name="from">From.</param>
    /// <param name="to">To.</param>
    public static Vector3 FromTo(Vector3 from, Vector3 to)
    {
        return to - from;
    }

}
