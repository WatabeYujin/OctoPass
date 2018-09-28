using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sphere : MonoBehaviour {
    

    Vector3 reflectDir;
    ContactPoint contact;
    Vector3 after_hit;
    Vector3 before_hit;
    Vector3 inMagnitude;
    Pearlmove otherScript;
    bool startMoving;
    float time;

    public float reflectSpeed = 1f;
    public GameObject mainBody;
    

    // Use this for initialization
    void Start () {
        otherScript = GetComponent<Pearlmove>();
        before_hit = mainBody.transform.position;
    }

    // Update is called once per frame
    void Update () {
        Debug.DrawRay(transform.position, reflectDir * 10, Color.red);
        Debug.DrawRay(transform.position, contact.normal * 10, Color.blue);
        Debug.DrawRay(transform.position, transform.forward * 10, Color.black);
        Debug.DrawRay(transform.position,　-inMagnitude * 10, Color.green);

        //if (transform.position != Vector3.zero)
        //{
        //    time += Time.deltaTime;
        //}
        //if (time < 3) Destroy(this);
        
        //バールをtranslateで動くします
        if (startMoving)
        {
            Debug.Log("startMoving");
            transform.Translate(reflectDir * reflectSpeed * Time.deltaTime, 0);
            time += Time.deltaTime;
            if (time >= 3.0f)
            {

                //Destroy(this.gameObject);
            }
        }

        else startMoving = false;

        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision touches)
    {
        
        Debug.Log("ball touches " + touches.gameObject.name);
        // if ball touches sucker item destroy else reflect
        // バールが吸盤があった手にあったたらなくなる
        if (touches.gameObject.tag == "Sucker")
        {
            Destroy(this.gameObject);
        }
        //バールが何もついてない手にあったたら反射する
        else if (touches.gameObject.tag == "Untagged")
        {
            //コンフリクトしないよう別のスクリプトを無効にします。
            otherScript.enabled = false;
            //ぶつかる前の位置とぶつかった位置マイナスして、ぶつかるからのmagnitudeを探します。
            after_hit = transform.position;
            Debug.Log(touches.contacts);
            inMagnitude = after_hit - before_hit;
            contact = touches.contacts[0];
            //calculate angle of the ball from origin    
            //バールの反射後角度を探す
            reflectDir = Vector3.Reflect(inMagnitude, contact.normal);
            // change the ball velocity to new direction velocity
            //バールの速度が新しい角度と元々の速度を変換する
            startMoving = true;
        }        
    }
    private void OnCollisionExit(Collision touches)
    {
        //最小からぶつかると、新しい値を探す
        before_hit = touches.transform.position;
    }

    private void OnDestroy()
    {
        Common.Instance.PearlCountUp();
    }

}
