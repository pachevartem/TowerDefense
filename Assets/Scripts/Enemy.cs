using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace CyberCountry
{
    public abstract class Enemy: MonoBehaviour, IReloaded
    {
        private string idTower = string.Empty;

        private float _startHealth;
        public static Enemy Target; //TODO: может быть много башень.
        public static HashSet<Enemy> All = new HashSet<Enemy>();
        
        public static Enemy CreateEnemy(IGameManager gameManager, GameObject EnemyModel, Type e, int health) // Вот это и есть фабричный метод
        {
            var t = Instantiate(EnemyModel);
            var r = (Enemy)t.AddComponent(e);
            r._gameManager = gameManager;
            r.Health = health;
            r._startHealth = health;
            

            All.Add(r);
            return r;
        }

        public static void StopEnemies()
        {
            Target = null;
            List<Enemy> enemies = All.ToList();
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Stop();
                Destroy(enemies[i].gameObject);
            }
            All.Clear();
        }



        protected int _health;
        private Slider _slider;
        private TextMeshProUGUI _HealthUI;

        protected IGameManager _gameManager;

        #region Abstract methods
        public abstract void Run();
        public abstract void ReloadGame();
        public abstract void Stop();

        #endregion

       

        public virtual void OnEnable()
        {
            _slider = GetComponentInChildren<Slider>();
            _HealthUI = GetComponentInChildren<TextMeshProUGUI>();
        }

        public int Health
        {
            get => _health;
            set
            {
                if (value<0)
                {
                    print("DieEvent - "+ idTower);
                    Tower.Frags(idTower);
                    if (Target == this)
                    {
                        Target = null;
                    }
                    Termination();
                }
                else
                {
                    _health = value;
                    _HealthUI.text = _health.ToString();
                    _slider.value = (float)(_health/_startHealth);
                }
            }
        }

        
        public void GetDamage(int value, string idTower)
        {
            this.idTower  = idTower;
            Health -= value;
            
        }

        public void ReachedCastle()
        {
            Termination();
        }

        private void Termination()
        {
            All.Remove(this);
            Destroy(gameObject);            
        }
        
    }
}