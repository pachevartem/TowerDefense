using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CyberCountry
{
    public class Portal : MonoBehaviour, IReloaded
    {
        public List<GameObject> Enemies; // тут префабы врагов

        private float _timetPortal = 0;
        private float _delaySpawn = 1;
        private float _dtSpawn = 0;
        private float NextWave = 4; // Magic number
        private int _healthWave = 100; // default = 100;
        
        private IGameManager _gameManager;

        public void Play(IGameManager gameManager)
        {
            _gameManager = gameManager;
            CreateEnemy();
            
            StartCoroutine(UpdatePortal());
        }
        

        void CreateEnemy()
        {
            var k = Enemy.CreateEnemy(_gameManager, Enemies[0], typeof(Knight),
                _healthWave); // использование фабрики для комбинации свойств
            print(k);
            k.Run();

            

        }

        IEnumerator UpdatePortal()
        {
            while (true)
            {
                _timetPortal += Time.deltaTime;
                _dtSpawn += Time.deltaTime;
                yield return null;

                if (_dtSpawn>_delaySpawn)
                {
                    CreateEnemy();
                    _dtSpawn = 0;
                }

                if (_timetPortal>NextWave)
                {
                    LevelUpEnemy();
                    _timetPortal = 0;
                }
            }
            
        }

        void LevelUpEnemy()
        {
            _healthWave += 80; // magic number перенести в Gamedesign
        }

        public void ReloadGame()
        {
            LevelUpEnemy();
            _timetPortal = 0;
            _healthWave = 100;
        }
    }
}