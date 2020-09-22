namespace CyberCountry
{
    public interface IUserInterface
    {
        void StartGame();
        void SwitchSound();
        void ReloadGame();
        void CreateTree();
        void HealCastle();

        void ShowTrackOnTagPanel();
        void ShowPressPlayPanel();

        //
        IUserInterface Inject(IGameManager manager); //TODO: ы
    }
}