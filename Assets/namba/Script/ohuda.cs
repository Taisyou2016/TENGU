using UnityEngine;
using System.Collections;

public class ohuda : MonoBehaviour {

    private Rigidbody rd;
    private bool flag = true;
    public GameObject fire;
    public float speed = 10;


	// Use this for initialization
	void Start () {
        rd = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update () {
        if (flag)
        {
            rd.velocity = transform.forward * speed;
        }
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

    public void Ricochet()
    {
        flag = false;
    }
}
