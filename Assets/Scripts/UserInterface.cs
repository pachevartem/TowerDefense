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
                        _play.onClick.AddListener(() => {
                            _play.gameObject.SetActive(false);
                            _reload.gameObject.SetActive(true);
                        });                        
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
            _manager.StartGame();
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
    }
}