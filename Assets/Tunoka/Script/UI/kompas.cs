using UnityEngine;
using System.Collections;

public class kompas : MonoBehaviour {

    public GameObject[] gos;
    public GameObject Map_Amu;

    private GameObject player;

    void Start ()
    {
        player = GameObject.Find("Player");
        AmuletSet();
    }
	
	// Update is called once per frame
	void Update ()
    {
        var heading = player.transform.rotation.y;
        transform.rotation = Quaternion.Euler(0, 0, heading);
    }
    void AmuletSet()
    {
        gos = GameObject.FindGameObjectsWithTag("Amulet");

        for (int i = 0; i < gos.Length ; i++)
        {
            GameObject clone = (GameObject)Instantiate(Map_Amu, transform.position, transform.rotation);
            clone.transform.parent = transform;
            clone.GetComponent<MapAmulet>().amulet = gos[i];
        }
    }
}
