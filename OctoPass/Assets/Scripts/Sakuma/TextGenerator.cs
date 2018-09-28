using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class TextGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject[] list;
    [SerializeField]
    private string name;
    [SerializeField]
    private GameObject maincamera;

    void Start () {
        int i = 0;

        File.WriteAllText("Assets/Mapdatatext/savedata_" + name+".txt", "");
        File.AppendAllText(
            "Assets/Mapdatatext/savedata_" + name + ".txt",
            0 + ","
            + (maincamera.transform.position.x).ToString() + ","
            + (maincamera.transform.position.y).ToString() + ","
            + (maincamera.transform.position.z).ToString() + ","
        );
        foreach (Transform child in transform)
        {
            var text = transform.GetChild(i).gameObject.name;
            var texts = text.Split(' ');



            for (int j = 0; j < list.Length; j++)
            {
                if (texts[0] == list[j].gameObject.name)
                {
                    Debug.Log(j);
                    File.AppendAllText(
                        "Assets/Mapdatatext/savedata_" + name + ".txt",
                        j + ","
                        + (transform.GetChild(i).gameObject.transform.position.x).ToString() + ","
                        + (transform.GetChild(i).gameObject.transform.position.y).ToString() + ","
                        + (transform.GetChild(i).gameObject.transform.position.z).ToString() + ","
                        );
                }
            }





            i++;
        }

        GameObject.Find("Text").GetComponent<Text>().text = "savedata_" + name + ".txtにセーブしますた(*^-^*)";


    }
	
}
