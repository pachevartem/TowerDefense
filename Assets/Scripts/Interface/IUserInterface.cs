namespace CyberCountry
{
    public interface IUserInterface
    {
        void StartGame();
        void SwitchSound();
        void ReloadGame();
        void CreateTree();
        void HealCastle();
        //
        IUserInterface Inject(IGameManager manager); //TODO: ы
    }
}