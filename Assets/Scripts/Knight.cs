using UnityEngine;
using UnityEngine.AI;

namespace CyberCountry
{
    
    
    public class Knight: Enemy
    {
        private Animator _anim;
        private NavMeshAgent _agent;
        public override void OnEnable()
        {
            base.OnEnable();
            _agent = GetComponent<NavMeshAgent>();
            _anim = GetComponent<Animator>();
        }

        public override void Run()
        {
            _agent.SetDestination(_gameManager.CastlePos().position);
        }

        public override void ReloadGame()
        {
            Health = 100; //TODO: сохранить изначально.
        }

        public override void Stop()
        {
            Debug.Log($"{this.GetType()} stopped!");
        }

        void Update()
        {
            _anim.SetFloat("speed", _agent.velocity.magnitude);
        }
    }
}