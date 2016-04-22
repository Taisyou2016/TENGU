using UnityEngine;
using System.Collections;

public class WindAttack : MonoBehaviour
{
    public GameObject windPrefab;
    public float power = 5.0f;
    public float vectorPower = 5.0f;

    private bool generation = false;
    private Vector3 vector = Vector3.zero;
    private GameObject player;
    private GameObject wind;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    void Update()
    {
        if (generation == true)
        {
            wind = Instantiate(windPrefab);
            wind.transform.position = player.transform.position + player.transform.forward;

            wind.GetComponent<Rigidbody>().velocity =
            player.transform.forward
            * power;

            Invoke("WindVectorMove", 0.5f);

            generation = false;
        }
    }

    public void WindGeneration(Vector3 vector)
    {
        generation = true;
        this.vector = Vector3.Normalize(vector);
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
