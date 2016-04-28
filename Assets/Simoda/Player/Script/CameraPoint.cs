using UnityEngine;
using System.Collections;

public class CameraPoint : MonoBehaviour
{
    public GameObject target;

    void Start()
    {

    }

    void Update()
    {
        transform.position = target.transform.position + target.transform.forward * -3.0f + target.transform.up * 3.0f;

        //if (Input.GetKey(KeyCode.Q))
        //{
        //    transform.Rotate(0, -120 * Time.deltaTime, 0);
        //}

        //if (Input.GetKey(KeyCode.E))
        //{
        //    transform.Rotate(0, 120 * Time.deltaTime, 0);
        //}
    }
}
