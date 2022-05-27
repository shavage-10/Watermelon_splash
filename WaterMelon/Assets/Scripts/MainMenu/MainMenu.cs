using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Playgame()
    {
        SceneManager.LoadScene("Game");

    }
    public void Scoreboard()
    {
        SceneManager.LoadScene("scoreboard");
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void Skins()
    {
        SceneManager.LoadScene("Skins");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}