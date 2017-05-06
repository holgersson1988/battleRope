using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSection : MonoBehaviour {

    public enum SectionType
    {
        Rope, 
        Weight
    }

    public SectionType type;

    public ConfigurableJoint joint;

    Rigidbody body;
    BoxCollider col;
    //ConfigurableJoint joint;

    LineRenderer line;

    public Vector3 gravityDirection;
    public float gravity;

    public RopeSection prevSection;
    public RopeSection nextSection;

	// Use this for initialization
	void Awake () {
        body = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();

        //joint = GetComponent<ConfigurableJoint>();

        line = GetComponent<LineRenderer>();

        prevSection = null;
        nextSection = null;
    }

    private void Update()
    {
        if (prevSection != null && nextSection != null)
        {
            if (type == SectionType.Rope)
            {
                Vector3 start = prevSection.transform.position;
                Vector3 middle = transform.position;
                Vector3 end = nextSection.transform.position;

                line.numPositions = 5;
                line.SetPosition(0, start);
                line.SetPosition(1, Vector3.Lerp(start, middle, 0.5f));
                line.SetPosition(2, middle);
                line.SetPosition(3, Vector3.Lerp(middle, end, 0.5f));
                line.SetPosition(4, end);

                //transform.rotation = Quaternion.LookRotation()
            }
            else if (type == SectionType.Weight)
            {

            }
        }
        else
        {
            line.numPositions = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate () {
        // Apply gravity
        body.AddForce(gravityDirection * gravity*body.mass * Time.deltaTime);
    }

    public void ConnectTo(RopeSection rs)
    {
        // Link in code
        prevSection = rs;
        rs.nextSection = this;

        // Connect the Joint
        if (joint)
        {
            joint.connectedBody = rs.GetComponent<Rigidbody>();
        }
        //spring.connectedBody = rb;
    }

    public void SetColor(Color col)
    {
        if (type == SectionType.Rope)
        {
            GetComponent<LineRenderer>().material.SetColor("_EmissionColor", col);
        }
        else if (type == SectionType.Weight)
        {
            GetComponent<SpriteRenderer>().color = col;
        }
    }
}
