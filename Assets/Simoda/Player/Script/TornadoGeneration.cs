using UnityEngine;
using System.Collections;

public class TornadoGeneration : MonoBehaviour
{
    public GameObject tornadoPrefab;


    void Start()
    {

    }

    void Update()
    {

    }

    public void TornadoGeneration1()
    {
        GameObject tornado = Instantiate(tornadoPrefab);

        tornado.transform.position =
            transform.position
            + transform.forward * 5.0f;
    }
}
