using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    private List<State> statelist = new List<State>();
    private IState currentState = null;

    public EnemyManager() { }

    public void ChangeState(State name)
    {
        if(this.currentState != null)
        {
            this.currentState.Exit();
        }
        this.currentState.Initialize();
    }
    	
	// Update is called once per frame
	void Update () {
        if(this.currentState == null)
        {
            return;
        }
        this.currentState.Update();
	}
}
