using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CyberCountry
{
    public class UserInterface : MonoBehaviour, IUserInterface
    {
        private Button _play;
        private Button _sound;
        private Button _reload;
        private Button _createTree;
        private Button _health;

        private RectTransform _track_tag_panel;
        private RectTransform _press_play_panel;

        private IGameManager _manager;

        private void FindButton()
        {
            var _t = FindObjectsOfType(typeof(UIElement)).Cast<UIElement>();

            foreach (var o in _t)
            {
                switch (o.id)
                {
                    case "play":
                        _play = o.GetComponent<Button>();
                        _play.onClick.AddListener(StartGame);                          
                        break;
                    case "sound":
                        _sound = o.GetComponent<Button>();
                        _sound.onClick.AddListener(SwitchSound);
                        break;
                    case "reload":
                        _reload = o.GetComponent<Button>();
                        _reload.onClick.AddListener(ReloadGame);
                        _reload.gameObject.SetActive(false);
                        break;
                    case "tree":
                        _createTree = o.GetComponent<Button>();
                        _createTree.onClick.AddListener(CreateTree);
                        break;
                    case "health":
                        _health = o.GetComponent<Button>();
                        _health.onClick.AddListener(HealCastle);
                        break;
                    case "track_tag":
                        _track_tag_panel = o.GetComponent<RectTransform>();                        
                        break;
                    case "press_play":
                        _press_play_panel = o.GetComponent<RectTransform>();                        
                        break;
                    default:
                        break;
                }
            }
        }

        private void Awake()
        {
            FindButton();
        }

        public void StartGame()
        {
            Debug.Log("Started!");

            if (_manager.StartGame())
            {
                _track_tag_panel.gameObject.SetActive(false);
                _press_play_panel.gameObject.SetActive(false);
                _play.gameObject.SetActive(false);
                _reload.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Game not started!");
            }
        }

        public void SwitchSound()
        {
            _manager.SwitchSound();
        }

        public void ReloadGame()
        {
            _manager.ReloadGame();
        }

        public void CreateTree()
        {

        }

        public void HealCastle()
        {
            _manager.HealCastle();
        }

        public IUserInterface Inject(IGameManager manager)
        {
            _manager = manager;
            return this;
        }

        public void ShowTrackOnTagPanel()
        {
            _press_play_panel.gameObject.SetActive(false);
            _track_tag_panel.gameObject.SetActive(true);
            //Показать панель с предложением трекинга
        }

        public void ShowPressPlayPanel()
        {
            _track_tag_panel.gameObject.SetActive(false);
            _press_play_panel.gameObject.SetActive(true);            
            //Показать панель с предложением нажать на кнопку Play
        }
    }
}