using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.namba.Script
{
    public class StateManager<T>
    {
        private IState<T> currentState;

        public StateManager()
        {
            currentState = null;
        }

        public IState<T> CurrentState
        {
            get { return currentState; }
        }

        public void ChangStatee(IState<T> state)
        {
            if(this.currentState != null)
            {
                this.currentState.End();
            }

            this.currentState = state;
            this.currentState.Initialize();
        }

        public void Update()
        {
            if(currentState != null)
            {
                currentState.Execute();
            }
        }
    }
}
