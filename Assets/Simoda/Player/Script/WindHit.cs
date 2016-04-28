using UnityEngine;
using System.Collections;

public class WindHit : MonoBehaviour
{
    public GameObject enemy;
    private TestEnemy TEnemy;

    private Vector3 hitVector = Vector3.zero;

    public void OnCollisionEnter(Collision collision)
    {
        print("hit1");
        if (collision.gameObject.tag == "Enemy")
        {
            print("hit2");
            TEnemy.Hit(hitVector, 10);
            //collision.rigidbody.AddForce(hitVector, ForceMode.Impulse);
        }
    }

    void Start()
    {
        TEnemy = enemy.GetComponent<TestEnemy>();
    }

    void Update()
    {

    }

    public void SetVector(Vector3 vector)
    {
        hitVector = vector;
    }
}
