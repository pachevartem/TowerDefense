using System;
using System.Linq;
using UnityEngine;

namespace CyberCountry
{
    public class GameManager: MonoBehaviour, IGameManager
    {
        public Transform CastleModel;
        public Transform TowerModel;
        public Transform PortalModel;
       
        //
        private Portal _portal;
        private Castle _castle;
        private Tower _tower;
        
        private ITrackerService _trackerService;
        private IUserInterface _userInterface;

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
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PauseGame();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadGame();
            }
        }

        public void CreateTree()
        {
            print("Create tree");
        }

        public void SwitchSound(bool isOn)
        {
            print("SwitchSound");
        }

        public void ReloadGame()
        {
            print("ReloadGame");
            Enemy.StopEnemies();
            _portal.ReloadGame();
            _castle.ReloadGame();
            _tower.ReloadGame();
            
        }

        public void StartGame()
        {
            print("StartGame");
            _portal.Play(this);
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
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;

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

        
    }
}