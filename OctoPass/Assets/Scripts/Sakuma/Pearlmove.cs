using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pearlmove : MonoBehaviour {

    private float angle = 0;
    private bool move = false;

    private float time = 0;
    GameObject scallops;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float destroyTime;


    // Update is called once per frame
    void Update () {
        if (move)
        {
            this.transform.Translate(Mathf.Cos(angle)*speed, Mathf.Sin(angle)*speed, 0f);
            time += Time.deltaTime;
            if(time >= destroyTime)
            {
                Destroy(this.gameObject);
            }
        }
	}

    private void Start()
    {
        
        scallops = GameObject.Find("scallops");
        angle = scallops.transform.localEulerAngles.z * Mathf.PI / 180.0f;
        move = true;
    }

}
