using UnityEngine;
using System.Collections;

public class shooter : MonoBehaviour {

    private Transform player;
    private Vector3 vec;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update () {
        vec = player.transform.position - this.transform.position;
        vec.x = 0;
        vec.z = 0;
        transform.rotation = Quaternion.LookRotation(vec);
	}
}
