using UnityEngine;
using System.Collections;

public class s_AmuletController : MonoBehaviour {

    [SerializeField]
    private int AmuletCount;
    bool One = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        AmuletCount = transform.childCount;
        if (AmuletCount <= 0)
        {
            Gimmick();
        }
	}
    void Gimmick()
    {
        if (One)
        {
            GetComponent<SphereCollider>().enabled = false;
            print("護符用ギミック作動");
            One = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
           // other.GetComponent<PlayerMove>().SetWindPower(1 , direction);
        }
    }
}
