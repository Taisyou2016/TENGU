using UnityEngine;
using System.Collections;

public class ohuda : MonoBehaviour {

    private Rigidbody rd;
    public float speed = 10;

	// Use this for initialization
	void Start () {
        rd = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rd.velocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag != "Enemy")
        {
            Died();
        }
    }

    void Died()
    {
        Destroy(this.gameObject);
    }
}
