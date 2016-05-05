using UnityEngine;
using System.Collections;

public class s_Amulet : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        print("お札解除");
        Destroy(gameObject);
    }
}
