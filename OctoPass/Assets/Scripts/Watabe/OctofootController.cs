using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OctofootController : MonoBehaviour {

    [SerializeField]
    private List<Transform> foots = new List<Transform>();      //タコ足のリスト。最後を足の先っぽにすること
    [SerializeField]
    private float footActionSpeed=1;                            //
    [SerializeField]
    private float footBackSpeed = 1;

    private Mesh defaltMesh;
    private Material defaltMaterial;
    private int nowFootLength;
    private IEnumerator footBackAction;
    private bool isfootOut=false;
    private Mesh pointEndMesh;
    private Material pointEndMaterial;
    // Use this for initialization
    void Awake () {
        ThisModelGet();
        pointEndModelGet();
        nowFootLength = foots.Count - 1;
        //FootOut();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ThisModelGet()
    {
        SkinnedMeshRenderer m_mesh = GetComponent<SkinnedMeshRenderer>();
        defaltMesh = m_mesh.sharedMesh;
        defaltMaterial = m_mesh.material;
    }
    
    void pointEndModelGet()
    {
        SkinnedMeshRenderer m_mesh = foots[foots.Count-1].GetComponent<SkinnedMeshRenderer>();
        pointEndMesh = m_mesh.sharedMesh;
        pointEndMaterial = m_mesh.material;
    }
    
    /// <summary>
    /// たこ足を引っ込める処理を行う
    /// </summary>
    public void FootOut()
    {
        if (isfootOut) return;
        isfootOut = true;
        if (footBackAction != null) StopCoroutine(footBackAction);
        Debug.Log("引っ込み開始");
        StartCoroutine(FootOutAction());
    }

    /// <summary>
    /// たこ足を出現させる処理を行う
    /// </summary>
    public void FootIn()
    {
        if (!isfootOut) return;
        isfootOut = false;
        Debug.Log("出現開始");
        footBackAction = FootInIAction();
        StartCoroutine(footBackAction);
    }

    /// <summary>
    /// 実際にたこ足の引っ込みを行う処理
    /// </summary>
    /// <returns></returns>
    IEnumerator FootOutAction()
    {
        Debug.Log(nowFootLength);
        for (; nowFootLength >= 0 ; nowFootLength--)
        {
            FootPartsisActive(foots[nowFootLength], false);
            if(nowFootLength - 1>=0)
                FootEndModelChange(foots[nowFootLength - 1]);
            yield return new WaitForSeconds(footActionSpeed / foots.Count);
        }
        FootIn();
    }

    /// <summary>
    /// 実際にたこ足の出現を行う処理
    /// </summary>
    /// <returns></returns>
    IEnumerator FootInIAction()
    {
        yield return new WaitForSeconds(footBackSpeed);
        nowFootLength = -1;
        while (nowFootLength < foots.Count - 1)
        {
            nowFootLength++;
            Debug.Log(nowFootLength);
            FootPartsisActive(foots[nowFootLength], true);
            if (nowFootLength - 1 >= 0)
                FootDefaltModelChange(foots[nowFootLength - 1]);
            yield return new WaitForSeconds(footActionSpeed / foots.Count);
        }
    }

    /// <summary>
    /// タコ足のパーツの非表示化を行う
    /// </summary>
    /// <param name="footTransform"></param>
    /// <param name="isActive"></param>
    void FootPartsisActive(Transform footTransform,bool isActive)
    {
        const string m_suckerTagName = "Sucker";
        footTransform.GetComponent<Collider>().enabled = isActive;
        footTransform.GetComponent<Renderer>().enabled = isActive;
        Debug.Log("子に吸盤があるか判定");
        if (footTransform.childCount<=0) return;
        if (footTransform.GetChild(0).tag != m_suckerTagName) return;
        FootPartsisActive(footTransform.GetChild(0), isActive);
    }

    void FootEndModelChange(Transform footTransform)
    {
        //const string PointedEnd = "PointEnd"
        SkinnedMeshRenderer m_mesh = footTransform.GetComponent<SkinnedMeshRenderer>();
        m_mesh.sharedMesh = pointEndMesh;
        m_mesh.material = pointEndMaterial;
    }

    void FootDefaltModelChange(Transform footTransform)
    {
        SkinnedMeshRenderer m_mesh = footTransform.GetComponent<SkinnedMeshRenderer>();
        m_mesh.sharedMesh = defaltMesh;
        m_mesh.material = defaltMaterial;
    }
}
