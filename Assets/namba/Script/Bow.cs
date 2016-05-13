using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour {

    private Rigidbody rd;
    public float speed = 15;


	// Use this for initialization
	void Start () {
        rd = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rd.velocity = transform.forward * speed;
        //speed *= 0.99f;
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

}
