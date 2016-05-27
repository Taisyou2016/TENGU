using UnityEngine;
using System.Collections;

public class s_AmuletController : MonoBehaviour {

    [SerializeField]
    private int AmuletCount;
    bool One = true;

    public GameObject Center;
    public GameObject WindPoint;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        AmuletCount = transform.childCount;
        if (AmuletCount <= 1)
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

            Center.gameObject.transform.LookAt(other.transform.position);
            iTween.MoveTo(other.gameObject, iTween.Hash("x", WindPoint.transform.position.x, "y", WindPoint.transform.position.y, "z", WindPoint.transform.position.z));
            
        }
    }
}
