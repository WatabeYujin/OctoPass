using System.Collections;
using UnityEngine;

public class Title : MonoBehaviour {

    [SerializeField, Header("ほたてObject")]
    private GameObject Hotate;

    private bool _once = false;

	// Use this for initialization
	void Start () {
        Result result = GetComponent<Result>();
        _once = false;
	}

    /// <summary>
    /// スタートボタン処理
    /// </summary>
    public void StartClick()
    {
        if (!_once)
        {
            StartCoroutine(ScallopsAnim());
            _once = true;
        }
    }

    /// <summary>
    /// ほたてのアニメーション処理
    /// </summary>
    private IEnumerator ScallopsAnim()
    {
        //Hotate.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1.2f);
        Common.Instance.SceneMove(Common.SceneName.Main);
    }
}
