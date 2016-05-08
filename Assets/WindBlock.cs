using UnityEngine;
using System.Collections;

public class WindBlock : MonoBehaviour
{
    public GameObject windPrefab;

    public void OnTriggerEnter(Collider other)
    {
        windPrefab.GetComponent<Wind>().HitForce(other.gameObject);
    }

    void Start()
    {
    }

    void Update()
    {

    }
}
