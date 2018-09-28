using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sucker : MonoBehaviour {

    
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
        Debug.Log("真珠側から真珠ロスト時の処理を呼び出す");
    }
}
