using UnityEngine;
using System.Collections;

public class TornadoGeneration : MonoBehaviour
{
    public GameObject tornadoPrefab;
    public float tornadoPower = 10;

    void Start()
    {

    }

    void Update()
    {

    }

    public void TornadoGeneration1()
    {
        GameObject tornado = Instantiate(tornadoPrefab);
        tornado.transform.FindChild("Particle").gameObject.GetComponent<ParticleTornado>().power = tornadoPower;

        tornado.transform.position =
            transform.position
            + transform.forward * 5.0f;
    }
}
