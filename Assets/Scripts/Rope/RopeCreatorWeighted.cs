using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCreatorWeighted : RopeCreator {

    public RopeSection weightPrefab;

    protected override void CustomizeRope(Rope rope)
    {
        //base.CustomizeRope(rope);
        // 

        RopeSection weight1 = Instantiate(weightPrefab, rope.sections[0].transform.position, Quaternion.identity);
        rope.sections[0].ConnectTo(weight1);
        rope.InsertSection(weight1, 0);

        Destroy(weight1.GetComponent<ConfigurableJoint>());

        int numSections = rope.sections.Count;
        RopeSection weight2 = Instantiate(weightPrefab, rope.sections[numSections - 1].transform.position, Quaternion.identity);
        weight2.ConnectTo(rope.sections[numSections - 1]);
        rope.AddSection(weight2);
    }
}
