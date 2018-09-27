using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour {

    [SerializeField, Header("リザルトPrefab")]
    private GameObject ResultPrefab;
    private Text HanteiText;
    private Text CountText;
    private Button Nextbtn;
    private GameObject canvas;

    private bool _goal = false;

	// Use this for initialization
	void Start () {
        canvas = GameObject.Find("Canvas");
        _goal = true;
	}

    /// <summary>
    /// タイトルに戻る処理
    /// </summary>
    private void NextClick()
    {
        Common.Instance.SceneMove(Common.SceneName.Title);
    }

    /// <summary>
    /// リザルトPrefabを生成
    /// </summary>
    public void ResultInstance()
    {
        GameObject Prefab = Instantiate(ResultPrefab, canvas.transform);
        HanteiText = GameObject.Find("Canvas/ResultPrefab(Clone)/Hantei").GetComponent<Text>();
        CountText = GameObject.Find("Canvas/ResultPrefab(Clone)/Count").GetComponent<Text>();
        Nextbtn = GameObject.Find("Canvas/ResultPrefab(Clone)/ToTitle").GetComponent<Button>();
        Nextbtn.onClick.AddListener(NextClick);
        HanteiText.text = Evaluation(Common.pearlCount);
        CountText.text = Common.pearlCount.ToString();
        //if (_goal)
        //{
        //    GameObject Prefab = Instantiate(ResultPrefab, canvas.transform);
        //    HanteiText = GameObject.Find("Canvas/ResultPrefab/Hantei").GetComponent<Text>();
        //    CountText = GameObject.Find("Canvas/ResultPrefab/Count").GetComponent<Text>();
        //    HanteiText.text = Evaluation(Common.pearlCount);
        //    CountText.text = Common.pearlCount.ToString();
        //}
    }

    /// <summary>
    /// パールの数に応じて評価を変更する処理
    /// </summary>
    private string Evaluation(int count)
    {
        string eval;
        switch (count)
        {
            case 1:
                eval = "S";
                break;
            case 2:
                eval = "A";
                break;
            case 3:
            case 4:
            case 5:
                eval = "B";
                break;
            default:
                eval = "C";
                break;
        }
        Debug.Log("数&評価: " + Common.pearlCount + "&" + eval);
        return eval;
    }
}
