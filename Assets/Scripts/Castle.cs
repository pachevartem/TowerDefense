using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CyberCountry
{    
    public class Castle : MonoBehaviour, IReloaded
    {
        private int _health = 100;
        private TextMeshProUGUI _HealthUI;
        private Coroutine process;

        public IGameManager GameManager {get;set;}

        private void OnEnable()
        {
            _HealthUI = this.GetComponentInChildren<TextMeshProUGUI>();
            ShowHealth();
            //process = StartCoroutine(Healing());
        }
        

        public int Health
        {
            get => _health;
            private set
            {
                if (value <= 0)
                {
                    _health = 0;
                    GameManager.EndGame();
                }
                else if(value > 100)
                {
                    _health = 100;
                }
                else
                {
                    _health = value;
                }

                ShowHealth();

                
            }
        }



        public void Heal()
        {
            Health += 30;
        }

        private void Damage(int value)
        {            
            Health -= value;                        
        }

        private void ShowHealth()
        {
            _HealthUI.text = Health.ToString();
        }

        public void Stop()
        {
            if(process!=null)
            {
                StopCoroutine(process);
            }
        }

        public void ReloadGame()
        {
            Health = 100;            
            Stop();           
        }

        private float dt;
        private float healDelay = 10;

        private void Update()
        {
            DetectEnemy();
        }

        private void DetectEnemy()
        {
            Enemy reachedEnemy = null;

            foreach (Enemy enemy in Enemy.All)
            {                
                if(enemy.IsReachTarget())
                {
                    reachedEnemy = enemy;
                    Damage(10);
                    break;
                }    
            }

            reachedEnemy?.ReachedCastle();       
        }



    }
}