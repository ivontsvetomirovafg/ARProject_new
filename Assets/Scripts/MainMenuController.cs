using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]  
    private GameObject selectGame; 
    [SerializeField]  
    private GameObject bananaCat; 

    void Start()
    {
        //selectGame.SetActive(false);
        //selectGame.SetActive(true);
    }
    public void PlayButton()
    {
        selectGame.SetActive(true);
        bananaCat.SetActive(false);
    }

    // Botones de juegos
    public void Game1()
    {
        SceneManager.LoadScene("FakeAR");
    }
    
    public void Game2()
    {
        SceneManager.LoadScene("TrackingImage");
    }
    
    public void Game3()
    {
        SceneManager.LoadScene("TrackingSurface");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
