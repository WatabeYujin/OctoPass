using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OctofootController : MonoBehaviour {

    [SerializeField]
    private List<Transform> foots = new List<Transform>();  //タコ足のリスト。最後を足の先っぽにすること
    [SerializeField]
    private float footActionSpeed=1;                        //タコ足の出し入れのスピード
    [SerializeField]
    private float footBackSpeed = 1;                        //再度タコ足を出現させるまでの時間

    private int nowFootLength;                              //現在のタコ足の長さ                                
    private bool isfootOut = false;                         //タコ足が引っ込んでいるか

    private List<Mesh> defaltMesh = new List<Mesh>();
    private List<Material> defaltMaterial = new List<Material>();
    private List<Avatar> defaltAvatar = new List<Avatar>();

    private Mesh pointEndMesh;
    private Material pointEndMaterial;
    private Avatar pointEndAvatar;

    private IEnumerator footBackAction;                     

    void Awake () {
        ThisModelGet();
        pointEndModelGet();
        nowFootLength = foots.Count - 1;
        FootPartsSetting();
    }
	
	void Update () {
		
	}

    //指定したたこ足に判定用の処理を追加する
    void FootPartsSetting()
    {
        foreach(Transform m_foots in foots)
        {
            m_foots.gameObject.AddComponent<OctfootParts>().OctoFootControllerSet(this);
        }
    }

    //自身の元のmesh・material・avatarを取得する
    void ThisModelGet()
    {
        foreach (Transform m_foots in foots)
        {
            SkinnedMeshRenderer m_mesh = m_foots.GetComponentInChildren<SkinnedMeshRenderer>();
            defaltMesh.Add(m_mesh.sharedMesh);
            defaltMaterial.Add(m_mesh.material);
            defaltAvatar.Add(m_foots.GetComponent<Animator>().avatar);
        }
    }
    
    //たこ足の先端のmesh・material・avatarを取得する
    void pointEndModelGet()
    {
        SkinnedMeshRenderer m_mesh = foots[foots.Count-1].GetComponentInChildren<SkinnedMeshRenderer>();
        pointEndMesh = m_mesh.sharedMesh;
        pointEndMaterial = m_mesh.material;
        pointEndAvatar = foots[foots.Count - 1].GetComponent<Animator>().avatar;
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
    IEnumerator FootOutAction()
    {
        Debug.Log(nowFootLength);
        for (; nowFootLength > 0 ; nowFootLength--)
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
                FootDefaltModelChange(foots[nowFootLength - 1], nowFootLength-1);
            yield return new WaitForSeconds(footActionSpeed / foots.Count);
        }
    }

    /// <summary>
    /// タコ足のパーツの表示・非表示の変更を行う
    /// </summary>
    /// <param name="footTransform">変更するタコ足</param>
    /// <param name="isActive">表示非表示</param>
    void FootPartsisActive(Transform footTransform,bool isActive)
    {
        footTransform.GetComponent<Collider>().enabled = isActive;
        SkinnedMeshRenderer[] m_skinnedMeshes= footTransform.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer skinned in m_skinnedMeshes)
        {
            skinned.enabled = isActive;
        }
    }
    
    /// <summary>
    /// たこ足の先端に見た目を変更させる処理
    /// </summary>
    /// <param name="footTransform">変更させるタコ足</param>
    void FootEndModelChange(Transform footTransform)
    {
        SuckerColliderisActive(footTransform, false);
        footTransform.GetComponent<Animator>().enabled = false;
        SkinnedMeshRenderer m_mesh = footTransform.GetComponentInChildren<SkinnedMeshRenderer>();
        footTransform.GetComponent<Animator>().avatar = pointEndAvatar;
        m_mesh.sharedMesh = pointEndMesh;
        m_mesh.material = pointEndMaterial;
        
    }

    /// <summary>
    /// 元のmesh・avatar・materialに戻す処理
    /// </summary>
    /// <param name="footTransform">変更するタコ足</param>
    /// <param name="isActive">タコ足のリストでの番号</param>
    void FootDefaltModelChange(Transform footTransform,int footsID)
    {
        SuckerColliderisActive(footTransform, true);
        footTransform.GetComponent<Animator>().enabled = true;
        SkinnedMeshRenderer m_mesh = footTransform.GetComponentInChildren<SkinnedMeshRenderer>();
        footTransform.GetComponent<Animator>().avatar = defaltAvatar[footsID];
        m_mesh.sharedMesh = defaltMesh[footsID];
        m_mesh.material = defaltMaterial[footsID];
    }

    /// <summary>
    /// 吸盤のcolliderのon・offを変更する処理
    /// </summary>
    /// <param name="footTransform">子に吸盤が存在するか確認するtransform</param>
    /// <param name="isActive">colliderのon・off</param>
    /// <returns>発見し、変更を行った場合true、それ以外はfalse</returns>
    bool SuckerColliderisActive(Transform footTransform,bool isActive)
    {
        Debug.Log("吸盤"+ footTransform.name);
        const string m_suckerTagName = "Sucker";
        const string m_sukerSetName = "SuckerSet";
        foreach (Transform child in footTransform)
        {
            if (child.tag == m_suckerTagName)
            {
                child.GetComponent<Collider>().enabled = isActive;
                Debug.Log(footTransform);
                GameObject m_sukerSet = footTransform.Find(m_sukerSetName).gameObject;
                m_sukerSet.SetActive(isActive);
                return true;
            }
        }
        return false;
    }

}
