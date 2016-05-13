using UnityEngine;
using System.Collections;

public class title : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 120, Screen.height - 40, 100, 20), "スタートボタン"))
        {
            Application.LoadLevel("GamePlay");
        }
    }
}
