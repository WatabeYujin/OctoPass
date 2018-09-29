using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctfootParts : MonoBehaviour {
    private OctofootController octofootController;

    public void OnCollisionEnter(Collision col)
    {
        PearEnter(col.transform);
    }

    public void OnTriggerEnter(Collider col)
    {
        PearEnter(col.transform);
    }

    void PearEnter(Transform pearTransform)
    {
        const string m_pearlTagName = "Pearl";
        if (pearTransform.tag != m_pearlTagName) return;
        octofootController.FootOut();
    }

    public void OctoFootControllerSet(OctofootController controller)
    {
        octofootController = controller;
    }
}
