using UnityEngine;
using System.Collections;

public class WindAttack : MonoBehaviour
{
    public GameObject windPrefab;
    public float power = 5.0f;
    public float vectorPower = 5.0f;

    private Vector3 vector = Vector3.zero;
    private GameObject player;
    private GameObject wind;

    void Start()
    {

    }

    void Update()
    {

    }

    public void WindAttack1(Vector3 vector)
    {
        wind = Instantiate(windPrefab);
        wind.transform.position = transform.position + transform.forward + transform.right * -3.0f;

        wind.GetComponent<Rigidbody>().velocity =
        transform.right
        * power;
    }

    public void WindAttack2(Vector3 vector)
    {
        wind = Instantiate(windPrefab);
        wind.transform.position = transform.position + transform.forward + transform.right * 3.0f;

        wind.GetComponent<Rigidbody>().velocity =
        transform.right
        * power
        * -1.0f;
    }

    public void WindGeneration3(Vector3 vector)
    {
        this.vector = Vector3.Normalize(vector);

        wind = Instantiate(windPrefab);
        wind.transform.position = transform.position + transform.forward;

        wind.GetComponent<Rigidbody>().velocity =
        transform.forward
        * power;

        Invoke("WindVectorMove", 0.5f);
    }

    public void WindVectorMove()
    {
        wind.GetComponent<Rigidbody>().velocity +=
        vector * vectorPower;

        //wind.GetComponent<Rigidbody>().AddForce(
        //vector * vectorPower,
        //ForceMode.Impulse);
    }
}
