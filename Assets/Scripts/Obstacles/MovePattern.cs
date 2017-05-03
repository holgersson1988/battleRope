using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePattern : MonoBehaviour {

    public List<Vector3> points;
    public bool isLooping;
    public float speed;
    public int index;
    public int travelDirection;

    public Color gizmoColor = Color.white;

    private int nextIndex;
    private Rigidbody rb;
    

	void Start () {
        transform.position = points[index];
        rb = GetComponent<Rigidbody>();
        Debug.Assert(rb, "No rigidbody on moving object");

        // To start movement
        nextIndex = index + travelDirection;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        foreach (Vector3 point in points)
        {
            Gizmos.DrawSphere(point, 0.25f);
        }
    }

    void FixedUpdate () {

        Vector3 nextPosition = points[nextIndex];
        // Find direction to next point
        Vector3 direction = Utility.FromTo(transform.position, nextPosition).normalized;

        // Add force towards next point
        rb.MovePosition(transform.position + (direction*speed*Time.deltaTime));

        // When close to the target position reset and move towards next
        Vector3 distance = Utility.FromTo(transform.position, nextPosition);
        if (distance.magnitude < rb.velocity.magnitude * Utility.DeltaTime())
        {          
            // Move to target and reset speed
            rb.MovePosition(nextPosition);

            // Find new indices
            index += travelDirection;

            if (index < 0){
                index = points.Count - 1;
            }else if (index > points.Count - 1){
                index = 0;
            }
            
            nextIndex = index + travelDirection;
            if (nextIndex >= points.Count || nextIndex < 0)
            {
                // If looping go to the other end of the list
                if (isLooping){
                    if (nextIndex < 0){
                        nextIndex = points.Count - 1;
                    }else{
                        nextIndex = 0;
                    }
                }
                // If NOT looping
                else{
                    travelDirection *= -1;
                    nextIndex = index + travelDirection;
                }
            }
        }
	}
}
