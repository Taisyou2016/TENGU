using UnityEngine;
using System.Collections;

public class TornadoDecion : MonoBehaviour {

    public Vector3 direction;
    public float power;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
        else
        {
            other.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Impulse);
        }
    }
}
