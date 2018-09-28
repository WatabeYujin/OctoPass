using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sphere : MonoBehaviour {
    

    Vector3 reflectDir;
    ContactPoint contact;
    Rigidbody rb;
    Vector3 before_velocity;

    public GameObject ball;
    // for debug
    float speed = 15;

    // Use this for initialization
    void Start () {
        rb = this.gameObject.GetComponent<Rigidbody>();
        before_velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        // For debug デバッグ用
        float movehorizontal = Input.GetAxis("Horizontal");
        float movevertical = Input.GetAxis("Vertical");
        Vector3 movements = new Vector3(movehorizontal, 0.0f, movevertical);
        rb.AddForce(movements * speed);
        before_velocity = rb.velocity;
    }
    // Update is called once per frame
    void Update () {

        // For debug デバッグ用
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }
        if (Input.GetKey(KeyCode.O))
        {
            rb.velocity = transform.forward * 40;
        }
        if (Input.GetKey(KeyCode.P))
        {
            rb.velocity = -transform.forward * 40;
        }
        Debug.DrawRay(transform.position, reflectDir * 10, Color.red);
        Debug.DrawRay(transform.position, contact.normal * 10, Color.blue);
        Debug.DrawRay(transform.position, before_velocity * -10, Color.green);
        Debug.DrawRay(transform.position, transform.forward * 10, Color.black);
    }

    private void OnCollisionEnter(Collision touches)
    {
        Debug.Log("ball touches " + touches.gameObject.name);
        // if ball touches sucker item destroy else reflect
        // バールが吸盤があった手にあったたらなくなる
        if (touches.gameObject.tag == "sucker")
        {
            Destroy(this.gameObject);
        }
        //バールが何もついてない手にあったたら反射する
        else if (touches.gameObject.tag == "tako")
        {
            contact = touches.contacts[0];
            //calculate angle of the ball from origin    
            //バールの反射後角度を探す
            reflectDir = Vector3.Reflect(before_velocity, contact.normal);
            // change the ball velocity to new direction velocity
            //バールの速度が新しい角度と元々の速度を返還する
            rb.velocity = reflectDir.normalized * 30;
        }
    }



}
