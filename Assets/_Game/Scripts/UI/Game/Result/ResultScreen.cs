using UnityEngine;

public class ResultScreen : BaseScreen
{
    private void Awake()
    {
        /*AudioManager.Instance.Stop(SoundType.GameAmbience);
        AudioManager.Instance.Play(SoundType.MenuAmbience);*/
    }

    // bound from inspector
    public void PlayAgain()
    {
        Time.timeScale = 1;
        GameManager.Instance.Restart();
        SceneLoadManager.Instance.RestartGame();
    }

    // bound from inspector
    public void MainMenu()
    {
        Time.timeScale = 1;
        GameManager.Instance.Restart();
        SceneLoadManager.Instance.GoGameToMenu();
    }
}
