using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Result : MonoBehaviour {

    [SerializeField, Header("リザルトPrefab")]
    private GameObject ResultPrefab;
    private Text Fase_1_Text;
    private Text CountText;
    private Button Nextbtn;
    private GameObject canvas;
    [SerializeField, Header("ホタテの上の貝の部分")]
    private GameObject Scallops_Top;
    [SerializeField, Header("評価Object")]
    private GameObject[] Eval_obj;

    private GameObject Prefab;

    private bool _goal = false;

	// Use this for initialization
	void Start () {
        canvas = GameObject.Find("Canvas");
        _goal = true;
        Common.Instance.fasePaerl = new int[] { 1, 5, 2, 7, 1, 2, 4, 6, 10, 1 };
        //StartCoroutine(ScallopsTopAnim());
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
    private void ResultInstance()
    {
        Prefab = Instantiate(ResultPrefab, canvas.transform);
        Fase_1_Text = GameObject.Find("Canvas/ResultPrefab(Clone)/Hantei").GetComponent<Text>();
        CountText = GameObject.Find("Canvas/ResultPrefab(Clone)/SumPearl/Count").GetComponent<Text>();
        Nextbtn = GameObject.Find("Canvas/ResultPrefab(Clone)/ToTitle").GetComponent<Button>();
        Nextbtn.onClick.AddListener(NextClick);
        int sumPearl = 0;
        for (int i = 0; i < Common.Instance.fase; i++)
        {
            GameObject FaseCount = GameObject.Find("Canvas/ResultPrefab(Clone)/Hantei/Fase/Fase_" + i);
            Evaluation(Common.Instance.fasePaerl[i], FaseCount);
            sumPearl += Common.Instance.fasePaerl[i];
        }
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
