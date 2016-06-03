using UnityEngine;
using System.Collections;

public class BossCreateEnemyP : MonoBehaviour {

    // Use this for initialization
    public GameObject ele;
    void Start () {
        int count = 0;
        foreach (Transform child in transform)
        {
            Instantiate(ele, child.transform.position, transform.rotation);
            count++;
        }
        Invoke("DeleteObj", 3.5f);
    }
    void DeleteObj()
    {
        Destroy(gameObject);
    }
	
}
