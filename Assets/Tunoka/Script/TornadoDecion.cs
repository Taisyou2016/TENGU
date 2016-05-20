using UnityEngine;
using System.Collections;

public class TornadoDecion : MonoBehaviour {

    public Vector3 direction;
    public float power;

    public void OnTriggerStay(Collider other)
    {
        print(other.tag);
        if (other.tag == "Player")
        {          
            other.GetComponent<PlayerMove>().SetVelocityY((int)power);
        }
        else
        {
            other.GetComponent<Rigidbody>().AddForce(direction * (power / 2), ForceMode.Impulse);
        }
    }
}
