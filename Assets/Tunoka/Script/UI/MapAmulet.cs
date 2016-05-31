using UnityEngine;
using System.Collections;

public class MapAmulet : MonoBehaviour {
    public GameObject amulet;
    private GameObject player;

    

    void Start () {
        player = GameObject.Find("Player");
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        var heading = player.transform.rotation.y;
        print(heading);
    }
}
