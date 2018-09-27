using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctfootParts : MonoBehaviour {
    [SerializeField]
    OctofootController octofootController;

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
        Debug.Log("接触");
        const string m_pearlTagName = "pearl";
        if (pearTransform.tag != m_pearlTagName) return;
        octofootController.FootOut();
    }
}
