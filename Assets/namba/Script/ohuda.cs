using UnityEngine;
using System.Collections;

public class ohuda : MonoBehaviour {

    private Rigidbody rd;
    public GameObject fire;
    public float speed = 10;


	// Use this for initialization
	void Start () {
        rd = GetComponent<Rigidbody>();
        rd.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update () {
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
        Instantiate(fire, this.transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

}
