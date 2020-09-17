using UnityEngine;

namespace CyberCountry
{
    public interface IGameManager
    {
        void CreateTree();
        void SwitchSound(bool isOn);
        void ReloadGame();
        void StartGame();
        

        Transform CastlePos();
        Transform TowerPos();
        Transform PortalPos();
    }
}