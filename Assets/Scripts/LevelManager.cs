using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField] 
    private GameObject panelPause;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        //AudioManager.Instance.FadeOutMusic(1.5f);
        panelGameOver.SetActive(true);
    }
    public void Pause()
    {
        if (panelPause.activeInHierarchy == false)
        {
            //musicSource.volume = 0.1f;
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            //musicSource.volume = 0.6f;
            panelPause.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Reiniciar()
    {
        
    }
}
