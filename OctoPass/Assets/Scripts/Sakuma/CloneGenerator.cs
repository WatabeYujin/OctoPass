using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CloneGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject[] list;
    [SerializeField]
    private string name;
    [SerializeField]
    private GameObject maincamera;

    // Use this for initialization
    void Start () {
        string[] allText1 = File.ReadAllLines("Assets/Mapdatatext/savedata_" + name + ".txt");

        var texts = allText1[0].Split(',');

        Debug.Log((texts.Length - 1) / 4);


        for (int i = 0; i < (texts.Length - 1) / 4; i++)
        {
            if (i == 0)
            {
                maincamera.transform.position = new Vector3(
                    float.Parse(texts[(i * 4) + 1]),
                    float.Parse(texts[(i * 4) + 2]),
                    float.Parse(texts[(i * 4) + 3]));

            }
            else
            {


                Instantiate(
                    list[int.Parse(texts[i * 4])],
                    new Vector3(
                        float.Parse(texts[(i * 4) + 1]),
                        float.Parse(texts[(i * 4) + 2]),
                        float.Parse(texts[(i * 4) + 3])),
                    Quaternion.identity);
            }
        }



    }
	
    

}
