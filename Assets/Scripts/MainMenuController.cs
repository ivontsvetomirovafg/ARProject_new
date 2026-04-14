using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]  
    private GameObject selectGame; 

    void Start()
    {
        selectGame.SetActive(false);
    }

    // Botón Play
    public void PlayButton()
    {
        selectGame.SetActive(true);
    }

    // Botones de juegos
    public void Game1()
    {
        SceneManager.LoadScene("Game1");
    }
    
    public void Game2()
    {
        SceneManager.LoadScene("Game2");
    }
    
    public void Game3()
    {
        SceneManager.LoadScene("Game3");
    }
}
