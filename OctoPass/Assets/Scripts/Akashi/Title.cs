using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

    [SerializeField, Header("ほたてObject")]
    private GameObject Hotate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// スタートボタン処理
    /// </summary>
    public void StartClick()
    {
        ScallopsAnim();
    }

    /// <summary>
    /// ほたてのアニメーション処理
    /// </summary>
    private IEnumerator ScallopsAnim()
    {
        Animator anim = Hotate.GetComponent<Animator>();
        anim.Play("Title_HotateAnim");
        yield return new WaitForSeconds(1.5f);
        Common.Instance.SceneMove(Common.SceneName.Main);
    }

}
