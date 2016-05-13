using UnityEngine;
using System.Collections;

public class BulletDestruction : MonoBehaviour {

    private int time;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time++;
        if(time > 120)
        {
            Destroy(gameObject);
        }
	}
}
