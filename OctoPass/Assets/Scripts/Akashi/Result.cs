using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Result : MonoBehaviour {

    [SerializeField, Header("リザルトPrefab")]
    private GameObject ResultPrefab;
    private Text CountText;
    private Button Nextbtn;
    [SerializeField, Header("ホタテの上の貝の部分")]
    private GameObject Scallops_Top;

    private GameObject Prefab;

    private bool _goal = false;

	// Use this for initialization
	void Start () {
        _goal = true;
        //Common.Instance.fasePaerl = new int[] { 1, 5, 2, 7, 1, 2, 4, 6, 10, 1 };
        //ResultInstance();
	}

    private void Update()
    {

    }

    /// <summary>
    /// ホタテが開く時のアニメーション処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator ScallopsTopAnim()
    {
        Scallops_Top.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1.2f);
        ResultInstance();
    }

    /// <summary>
    /// タイトルに戻る処理
    /// </summary>
    private void NextClick()
    {
        Common.Instance.SceneMove(Common.SceneName.Title);
        Destroy(Prefab, 2f);
    }

    /// <summary>
    /// リザルトPrefabを生成
    /// </summary>
    public void ResultInstance()
    {
        Prefab = Instantiate(ResultPrefab,transform);
        CountText = GameObject.Find("ResultPrefab(Clone)/SumPearl/Count").GetComponent<Text>();
        CountText.text = null;
        Nextbtn = GameObject.Find("ResultPrefab(Clone)/ToTitle").GetComponent<Button>();
        Nextbtn.onClick.AddListener(NextClick);
        Nextbtn.gameObject.SetActive(false);
        StartCoroutine(EvalWaitAnim());
    }

    /// <summary>
    /// 評価のアニメーション待機処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator EvalWaitAnim()
    {
        int sumPearl = 0;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < Common.Instance.fase; i++)
        {
            GameObject FaseCount = GameObject.Find("ResultPrefab(Clone)/Hantei/Fase/Fase_" + i);
            Evaluation(Common.Instance.fasePaerl[i], FaseCount);
            sumPearl += Common.Instance.fasePaerl[i];
        }
        yield return new WaitForSeconds(1.0f);
        Nextbtn.gameObject.SetActive(true);
        CountText.text = sumPearl.ToString();
    }

    /// <summary>
    /// パールの数に応じて評価を変更する処理
    /// </summary>
    /// <param name="count">パールの数</param>
    /// <param name="fase">n番目のフェーズ</param>
    private void Evaluation(int count,GameObject faseChild)
    {
        GameObject EvalPrefab = null;
        switch (count)
        {
            case 0:
            case 1:
                EvalPrefab = Resources.Load("Prefabs/Bubble_S") as GameObject;
                break;
            case 2:
                EvalPrefab = Resources.Load("Prefabs/Bubble_A") as GameObject;
                break;
            case 3:
            case 4:
            case 5:
                EvalPrefab = Resources.Load("Prefabs/Bubble_B") as GameObject;
                break;
            default:
                EvalPrefab = Resources.Load("Prefabs/Bubble_C") as GameObject;
                break;
        }
        Instantiate(EvalPrefab, faseChild.transform);
        
    }
}
