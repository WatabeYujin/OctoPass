using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Common : MonoBehaviour {

    private static Common instance;
    [SerializeField, Header("Fade Object")]
    private GameObject FadeObject;
    private static GameObject common_canvas;
    [SerializeField]
    private Result result;

    public static Common Instance
    {
        get
        {
            common_canvas = GameObject.Find("CommonCanvas");
            instance = FindObjectOfType<Common>();
            return instance;
        }
    }

    //フェーズの数
    public int fase = 10;

    //フェーズ毎のパール数を保存
    [HideInInspector]
    public int[] fasePaerl;

    //現パール数
    private int pearlCount = 0;

    //フェード中かどうか
    private bool isFading = false;

    private bool _fadeAnimFinish = false;

    private bool _faseFinish = false;
    private int count = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject.transform.parent);
        FadeObject.SetActive(false);
        Application.targetFrameRate = 60;           //目標FPSを60に設定
        fasePaerl = new int[fase];
    }

    /// <summary>
    /// スコアアップする時に呼ぶ処理
    /// 使い方: Common.Instance.PearlCountUp();
    /// </summary>
    public void PearlCountUp()
    {
        fasePaerl[count]++;
    }

    public void NextPhase()
    {
        const string m_phaseRunkObjName = "PhaseRunk";

        count++;
        Debug.Log(count+ "count");
        pearlCount = 0;
        GameObject.Find(m_phaseRunkObjName).GetComponent<PhaseResult>().RunkView();
    }

    /// <summary>
    /// シーン名
    /// </summary>
    public enum SceneName
    {
        Title = 0,
        Main,
    };

    /// <summary>
    /// シーン遷移
    /// 使い方: Common.Instance.SceneMove(Common.SceneName.シーン名);
    /// </summary>
    /// <param name="s_name"></param>
    public void SceneMove(SceneName s_name)
    {
        StartCoroutine(Fade(s_name));
    }

    /// <summary>
    /// フェードイン処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        FadeObject.SetActive(true);
        _fadeAnimFinish = false;
        this.isFading = true;
        float a = 0;
        while (a < 1)
        {
            FadeObject.GetComponent<Image>().color = new Color(0, 0, 0, a);
            a += 0.02f;
            yield return 0;
        }
        _fadeAnimFinish = true;
    }

    /// <summary>
    /// フェードアウト処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOut()
    {
        float a = 1;
        while (a > 0)
        {
            FadeObject.GetComponent<Image>().color = new Color(0, 0, 0, a);
            a -= 0.02f;
            yield return 0;
        }
        this.isFading = false;
        FadeObject.SetActive(false);
    }

    /// <summary>
    /// フェードイン→遷移→フェードアウト処理
    /// </summary>
    /// <param name="s_name"></param>
    /// <returns></returns>
    private IEnumerator Fade(SceneName s_name)
    {
        StartCoroutine(FadeIn());
        yield return new WaitUntil(() => _fadeAnimFinish);
        SceneManager.LoadScene((int)s_name);
        StartCoroutine(FadeOut());

        if (s_name == SceneName.Title)
        {
            GameObject obj = gameObject.transform.parent.gameObject;
            Destroy(obj,0.2f);
            common_canvas = null;
        }
    }
    public void ViewResult()
    {
        result.ResultInstance();
    }
}