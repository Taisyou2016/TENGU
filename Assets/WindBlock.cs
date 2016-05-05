using UnityEngine;
using System.Collections;

public class WindBlock : MonoBehaviour
{
    public Vector3 direction;
    public float power;
    public GameObject player;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
            player.GetComponent<PlayerMove>().SetWindPower(20.0f);
        else
            other.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Impulse);
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {

    }

    public void SetForce(Vector3 direction, float power)
    {
        this.direction = direction;
        this.power = power;
    }
}
