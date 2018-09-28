using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScallopsMove : MonoBehaviour {


    Vector3 mousepos = new Vector3(0, 0, 0);
    Vector3 scall = new Vector3(0, 0, 0);
    GameObject scallops;
    [SerializeField]
    GameObject pr;
    
    [SerializeField]
    private float top;
    [SerializeField]
    private float botm;

    GameObject wa;
    GameObject wa1;
    GameObject wa2;

    private void Start()
    {
        Debug.Log(scall);
        scallops = GameObject.Find("scallops");
        wa= GameObject.Find("Line");
        wa1 = GameObject.Find("R");
        wa2 = GameObject.Find("L");
    }


    void Update() {
        mousepos = Input.mousePosition;
        mousepos.z = 10.0f;
        scall = Camera.main.ScreenToWorldPoint(mousepos);
        transform.position = scall;

        float xdata = scallops.transform.position.x - transform.position.x;
        float ydata = scallops.transform.position.y - transform.position.y;

        float angle = 180 + Mathf.Atan2(ydata, xdata) * 180 / Mathf.PI;

        if (angle < top || angle > botm)
        {
            scallops.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);


            /////////////////////////////////////////////////////////////////////////////////////////////

            LineRenderer renderer = wa.GetComponent<LineRenderer>();


            // 線の幅
            renderer.SetWidth(0.1f, 0.1f);
            // 頂点の数
            renderer.SetVertexCount(2);
            renderer.SetColors(Color.blue, Color.red);

            renderer.SetPosition(1, new Vector3(
                scallops.transform.localPosition.x,
                scallops.transform.localPosition.y,
                scallops.transform.localPosition.z - 0.5f));

            renderer.SetPosition(0, new Vector3(
                (Mathf.Cos(angle * Mathf.PI / 180) ) * 2 + scallops.transform.localPosition.x,
                (Mathf.Sin(angle * Mathf.PI / 180) ) * 2 + scallops.transform.localPosition.y,
                this.transform.localPosition.z - 0.5f));

            //LineRenderer renderer1 = wa1.GetComponent<LineRenderer>();


            //// 線の幅
            //renderer1.SetWidth(0.1f, 0.1f);
            //// 頂点の数
            //renderer1.SetVertexCount(2);
            //renderer1.SetColors(Color.blue, Color.red);

            //renderer1.SetPosition(1, new Vector3(
            //    (Mathf.Cos(angle * Mathf.PI / 180)) * 1 + scallops.transform.localPosition.x + (Mathf.Cos(30 * Mathf.PI / 180)),
            //    (Mathf.Sin(angle * Mathf.PI / 180)) * 1 + scallops.transform.localPosition.y + (Mathf.Sin(30 * Mathf.PI / 180)),
            //    scallops.transform.localPosition.z - 0.5f));

            //renderer1.SetPosition(0, new Vector3(
            //    (Mathf.Cos(angle * Mathf.PI / 180)) * 2 + scallops.transform.localPosition.x,
            //    (Mathf.Sin(angle * Mathf.PI / 180)) * 2 + scallops.transform.localPosition.y,
            //    this.transform.localPosition.z - 0.5f));





            /////////////////////////////////////////////////////////////////////////////////////////////

        }
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(pr, scallops.transform.position, Quaternion.identity);

        }


    }
}
