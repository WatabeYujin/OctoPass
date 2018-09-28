using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private int stageNum;
    static private int nowstage;

    private GameObject nextobj;
    private GameObject moveobj;
    
    //クローン材
    [SerializeField]
    private GameObject[] list;
    [SerializeField]
    private GameObject maincamera;

    static private GameObject map;

    private int next;
    static public bool rsflg = false;

    [SerializeField]
    private GameObject retext;

    static public bool anime = false;
    private float cmid = 0;
    private void Update()
    {
        if (rsflg)
        {
            retext.GetComponent<Text>().text = "リザルト";
            if (Input.GetMouseButton(0))
            {
                retext.GetComponent<Text>().text = "";
                anime = true;
                rsflg = false;
            }




        } else
        {
            if (next != nowstage)
            {
                gamestart(nowstage);
            }

            next = nowstage;
            if (anime)
            {
                if (cmid < 24)
                {
                    maincamera.transform.Translate(0.1f, 0, 0);
                    cmid += 0.1f;
                    moveobj =GameObject.Find("stage" + (nowstage - 1)).transform.FindChild("Clear(Clone)").gameObject;
                    moveobj.transform.Translate(0.05f, 0, 0);
                }
                else
                {
                    anime = false;
                    nextobj.SetActive(true);
                    map = GameObject.Find("stage" + (nowstage-1).ToString());
                    foreach (Transform child in map.gameObject.transform)
                    {
                        Destroy(child.gameObject);
                    }
                }

            }
        }

    }

    // Use this for initialization
    void Start () {
        map = GameObject.Find("stage");
        nowstage = 1;
        gamestart(nowstage);
        next = nowstage;
	}

     private void gamestart(int num)
    {
        string[] allText1 = File.ReadAllLines("Assets/Mapdatatext/savedata_" + num.ToString()  + ".txt");
        var texts = allText1[0].Split(',');

        new GameObject("stage" + num.ToString());
        
        for (int i = 0; i < (texts.Length - 1) / 4; i++)
        {
            if (i == 0)
            {
                if (num == 1)
                {
                    maincamera.transform.position = new Vector3(
                        float.Parse(texts[(i * 4) + 1]),
                        float.Parse(texts[(i * 4) + 2]),
                        float.Parse(texts[(i * 4) + 3]));
                }
                else
                {
                    cmid = 0;
                }

            }
            else
            {


                GameObject obj =(GameObject)Instantiate(
                    list[int.Parse(texts[i * 4])],
                    new Vector3(
                        float.Parse(texts[(i * 4) + 1])+((num-1)*24),
                        float.Parse(texts[(i * 4) + 2]),
                        float.Parse(texts[(i * 4) + 3])),
                    Quaternion.identity,
                    GameObject.Find("stage" + num.ToString()).gameObject.transform);

                if (obj.name == "Scallops(Clone)"&&num!=1)
                { 
                    obj.SetActive(false);
                    nextobj = obj;
                }


            }
        }
    }


    static public void GameClaer()
    {       
        nowstage++;
    }

    static public void Gameresult()
    {

        rsflg = true;

    }

}
