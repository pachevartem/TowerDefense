using UnityEngine;

namespace CyberCountry
{
    public interface IGameManager
    {
        void CreateTree();
        void SwitchSound();
        void ReloadGame();
        bool StartGame();
        void EndGame();
        void PauseGame();
        void ResumeGame();

        void HealCastle();
        

        Transform CastlePos();
        Transform TowerPos();
        Transform PortalPos();
    }
}