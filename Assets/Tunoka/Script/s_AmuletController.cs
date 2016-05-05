using UnityEngine;
using System.Collections;

public class s_AmuletController : MonoBehaviour {

    [SerializeField]
    private int AmuletCount;
    public GameObject Gimmick;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        AmuletCount = transform.childCount;

        if (AmuletCount <= 0)
        {
            print("護符用ギミック作動");
            //Gimmick.GetComponent<Gimmick>().Gimmick();
        }
	}
}
