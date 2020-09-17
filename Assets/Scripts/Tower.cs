using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CyberCountry
{
    public class Tower : MonoBehaviour, IReloaded
    {
        [Range(1, 10)] public float radius = 2;

        [SerializeField] private string id;

        private int frags = 0;
        public static Dictionary<string, Tower> Towers = new Dictionary<string, Tower>();

        public int Frags1
        {
            get => frags;
            set
            {
                frags = value;

                if (frags % 3==0) //if frags % 3==0 
                {
                    Upgrade(55);
                }
            }
        }

        public static void Frags(string id)
        {
            Towers[id].Frags1++;
        }

        private HashSet<Transform> EnemysList = new HashSet<Transform>();
        private int Damage = 55; //MagicNumber

        public GameObject[] LvlObject = new GameObject[2];
        private TextMeshProUGUI DamageText;

        private void Awake()
        {
            id = gameObject.GetInstanceID().ToString(); // TODO: это нормально или нет?
            // id = GetHashCode().ToString(); // TODO: это нормально или нет?
            // id = Guid.NewGuid().ToString(); // TODO: это нормально или нет?

            Towers.Add(id, this);
            DamageText = GetComponentInChildren<UIElement>().GetComponent<TextMeshProUGUI>(); //TODO: поменять
            ShowDamage();
            ChangeModel(1);
        }

        void FindEnemy()
        {
            if (!Enemy.Target)
            {
                foreach (Enemy enemy in Enemy.All)
                {
                    if (Vector3.Distance(enemy.gameObject.transform.position, transform.position) < radius)
                    {
                        EnemysList.Add(enemy.transform);
                    }
                }

                if (EnemysList.Count > 0)
                {
                    Enemy.Target = EnemysList.First().GetComponent<Enemy>();
                }

                EnemysList.Clear();
            }
            else
            {
                if (Vector3.Distance(Enemy.Target.transform.position, transform.position) > radius)
                {
                    Enemy.Target = null;
                }
            }
        }

        void Shot(Enemy target)
        {
            if (target != null)
            {
                target.GetDamage(Damage, id);
            }
        }

        void Upgrade(int value)
        {
            Damage += value;
            ShowDamage(); // перенести в свойство
            ChangeModel(2);
        }

        void ChangeModel(int lvl)
        {
            for (int i = 0; i < LvlObject.Length; i++)
            {
                if (i == lvl - 1)
                {
                    LvlObject[i].SetActive(true);
                }
                else
                {
                    LvlObject[i].SetActive(false);
                }
            }
        }

        void ShowDamage()
        {
            DamageText.text = Damage.ToString();
        }

        public void ReloadGame()
        {
            ChangeModel(1);
            Damage = 30;
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }


        private float delay = 0.5f;
        private float dt = 0;


        private void Update()
        {
            dt += Time.deltaTime;

            FindEnemy();

            if (dt > delay)
            {
                Shot(Enemy.Target);
                dt = 0;
            }
        }
    }
}