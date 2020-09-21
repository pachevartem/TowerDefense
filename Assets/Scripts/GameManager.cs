using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace CyberCountry
{

    enum GameManagerState
    {
        Idle,
        Running,
        Pause
    }

    public class GameManager: MonoBehaviour, IGameManager
    {
        public Transform CastleModel;
        public Transform TowerModel;
        public Transform PortalModel;
               
        private Portal _portal;
        private Castle _castle;
        private Tower _tower;
        
        private ITrackerService _trackerService;
        private IUserInterface _userInterface;
        private StateMachine<GameManagerState> stateManager;

        private AudioSource _sound;

        private void Awake()
        {
            _trackerService = GetComponent<ITrackerService>(); //TODO: (bad) В случае если не используете внедрение зависимостей
            var canvas = GameObject.FindObjectsOfType(typeof(UIElement))
                .Cast<UIElement>()
                .Where(x => x.id == "canvas");

            var ui = (UserInterface) canvas.First().gameObject.AddComponent(typeof(UserInterface)); //TODO: тоже плохо. Но Unity по другому не умеет

            _userInterface = ui.Inject(this);
           
            _portal = (Portal) FindObjectOfType(typeof(Portal));

            _castle = (Castle)FindObjectOfType(typeof(Castle));
            _castle.GameManager = this;

            _tower = (Tower)FindObjectOfType(typeof(Tower));

            _sound = FindObjectOfType<AudioSource>();


            #region Машина состояний для GameManager

            stateManager = new StateMachine<GameManagerState>();

            stateManager.SetStateUpdate(GameManagerState.Running, () =>
            {
                if (!_trackerService.IsCastleTracking() || !_trackerService.IsTowerTracking() || !_trackerService.IsPortalTracking())
                {
                    PauseGame();
                    stateManager.ChangeState(GameManagerState.Pause);
                }
            });

            stateManager.SetStateUpdate(GameManagerState.Pause, () =>
            {
                if (_trackerService.IsCastleTracking() && _trackerService.IsTowerTracking() && _trackerService.IsPortalTracking())
                {
                    ResumeGame();
                    stateManager.ChangeState(GameManagerState.Running);
                }
            });

            stateManager.SetStateUpdate(GameManagerState.Idle, () =>
            {
                if (_trackerService.IsCastleTracking() && _trackerService.IsTowerTracking() && _trackerService.IsPortalTracking())
                {
                    //TODO: показать интерфейс, в котором игроку предлагается нажать на Play для старта игры
                }
                else
                {
                    //TODO: показать интерфейс, в котором игроку предлагается навестись на метки
                }
            });

            stateManager.ChangeState(GameManagerState.Idle);

            #endregion
        }

        private void Update()
        {
            stateManager.UpdateCurrentState();
        }

        public void CreateTree()
        {
            print("Create tree");
        }

        public void SwitchSound()
        {
            print("SwitchSound");

            _sound.volume = _sound.volume > 0 ? 0 : 1;
        }

        public void ReloadGame()
        {
            print("ReloadGame");
            Enemy.StopEnemies();
            _portal.ReloadGame();
            _castle.ReloadGame();
            _tower.ReloadGame();
            ResumeGame();
        }

        public bool StartGame()
        {
            if (_trackerService.IsCastleTracking() && _trackerService.IsTowerTracking() && _trackerService.IsPortalTracking())
            {
                print("StartGame");
                ResumeGame();
                _portal.Play(this);
                stateManager.ChangeState(GameManagerState.Running);
                return true;
            }
            return false;
        }

        public void EndGame()
        {
            print("EndGame");
            _portal.Stop();
            _castle.Stop();
            Enemy.StopEnemies();

        }

        public void PauseGame()
        {
            print("StopGame");
            Time.timeScale = 0;
        }


        public void ResumeGame()
        {
            print("ResumeGame");
            Time.timeScale = 1;
        }

        public Transform CastlePos()
        {
            return _trackerService.GetCastlePos();
        }

        public Transform TowerPos()
        {
            return _trackerService.GetTowerPos();
        }

        public Transform PortalPos()
        {
            return _trackerService.GetPortalPos();
        }

        public void HealCastle()
        {
            _castle.Heal();
        }

    }
}