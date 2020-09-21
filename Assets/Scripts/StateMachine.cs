using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;

namespace CyberCountry
{
    public class StateMachine<T> where T:Enum
    {

        private Dictionary<T, Action> startFunctions;
        private Dictionary<T, Action> updateFunctions;
        private Dictionary<T, Action> exitFunctions;

        public T CurrentState { get; private set; }        

        public StateMachine()
        {
            //if (!statePresentation.IsEnum)
            //    throw new ArgumentException($"Type {statePresentation} is not enum-type!");

            startFunctions = new Dictionary<T, Action>();
            updateFunctions = new Dictionary<T, Action>();
            exitFunctions = new Dictionary<T, Action>();
        }

        public void SetStateStart(T state, Action start)
        {
            if (start == null)
                throw new ArgumentException("Action is null");

            if (!startFunctions.ContainsKey(state))
            {
                startFunctions.Add(state, start);
            }
            else
            {
                startFunctions[state] = start;
            }
        }

        public void SetStateUpdate(T state,  Action update)
        {
            if (update == null)
                throw new ArgumentException("Action is null");

            if (!updateFunctions.ContainsKey(state))
            {
                updateFunctions.Add(state, update);
            }
            else
            {
                updateFunctions[state] = update;
            }
        }

        public void SetStateExit(T state, Action exit)
        {
            if (exit == null)
                throw new ArgumentException("Action is null");

            if (!exitFunctions.ContainsKey(state))
            {
                exitFunctions.Add(state, exit);
            }
            else
            {
                exitFunctions[state] = exit;
            }
        }

        public void ChangeState(T stateRepresent)
        {

            if(exitFunctions.ContainsKey(CurrentState))
            {
                exitFunctions[CurrentState].Invoke();
            }


            if(startFunctions.ContainsKey(stateRepresent))
            {
                startFunctions[stateRepresent].Invoke();
            }

            CurrentState = stateRepresent;
        }

        public void UpdateCurrentState()
        {
            if(updateFunctions.ContainsKey(CurrentState))
                updateFunctions[CurrentState].Invoke();
        }

    }
}
