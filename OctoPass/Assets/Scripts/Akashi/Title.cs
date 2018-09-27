using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

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
        Common.Instance.SceneMove(Common.SceneName.Main);
    }

    private void ScallopsAnim()
    {

    }

}
