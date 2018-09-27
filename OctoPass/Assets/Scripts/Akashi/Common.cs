using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Common : MonoBehaviour {

    private static Common instance;
    [SerializeField, Header("Fade Object")]
    private GameObject FadeObject;

    public static Common Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Common>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject.transform.parent);
        FadeObject.SetActive(false);
    }

    //スコアの合計
    [HideInInspector]
    public static int pearlCount = 0;

    [SerializeField, Header("フェードの待つ時間")]
    private float interval = 0f;
    private bool isFading = false;
    private Color fadeColor = Color.black;
    private float fadeAlpha = 0;

    /// <summary>
    /// スコアアップする時に呼ぶ処理
    /// 使い方: Common.Instance.ScoreUp();
    /// </summary>
    public void PearlCountUp()
    {
        pearlCount++;
        //Debug.Log("Score: " + score);
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
    /// フェードイン→遷移→フェードアウト処理
    /// </summary>
    /// <param name="s_name"></param>
    /// <returns></returns>
    private IEnumerator Fade(SceneName s_name)
    {
        FadeObject.SetActive(true);
        this.isFading = true;
        float a = 0;
        while (a < 1)
        {
            FadeObject.GetComponent<Image>().color = new Color(0, 0, 0, a);
            a += 0.01f;
            yield return 0;
        }

        SceneManager.LoadScene((int)s_name);

        while (a > 0)
        {
            FadeObject.GetComponent<Image>().color = new Color(0, 0, 0, a);
            a -= 0.01f;
            yield return 0;
        }
        FadeObject.SetActive(false);
    }
}
