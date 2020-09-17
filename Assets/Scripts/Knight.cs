using UnityEngine;
using UnityEngine.AI;

namespace CyberCountry
{
    
    public class Knight: Enemy
    {
        private NavMeshAgent _agent;
        public override void OnEnable()
        {
            base.OnEnable();
            _agent = GetComponent<NavMeshAgent>();
        }

        public override void Run()
        {
            _agent.SetDestination(_gameManager.CastlePos().position);
        }

        public override void ReloadGame()
        {
            Health = 100; //TODO: сохранить изначально.
        }
    }
}