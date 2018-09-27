using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// タイトルに戻る処理
    /// </summary>
    public void NextClick()
    {
        Common.Instance.SceneMove(Common.SceneName.Title);
    }
}
