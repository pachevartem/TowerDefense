using UnityEngine;

namespace CyberCountry
{
    public interface IGameManager
    {
        void CreateTree();
        void SwitchSound();
        void ReloadGame();
        void StartGame();
        void EndGame();
        void PauseGame();

        void HealCastle();
        

        Transform CastlePos();
        Transform TowerPos();
        Transform PortalPos();
    }
}