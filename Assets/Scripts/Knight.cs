using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows.Speech;

namespace CyberCountry
{
    
    
    public class Knight: Enemy
    {
        private Animator _anim;
        private NavMeshAgent _agent;

        public float speed; 

        public override void OnEnable()
        {
            base.OnEnable();
            _agent = GetComponent<NavMeshAgent>();
            _anim = GetComponent<Animator>();

            speed = 5;//TODO: magic number!!!
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

        public override bool IsReachTarget()
        {
            return _agent.remainingDistance < 0.1f;
        }
    }
}