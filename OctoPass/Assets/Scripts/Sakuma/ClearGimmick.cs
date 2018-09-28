using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearGimmick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pearl")
        {
            GameObject[] cubes = GameObject.FindGameObjectsWithTag("pearl");
            for(int i = 0; i < cubes.Length; i++)
            {
                Destroy(cubes[i].gameObject);
            }
            GameManager.Gameresult();
            GameManager.GameClaer();
        }

    }


}
