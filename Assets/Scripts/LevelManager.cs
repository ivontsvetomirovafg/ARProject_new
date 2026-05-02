using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField] 
    private GameObject panelPause;
    [SerializeField]
    private AudioClip gameOver;

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
        AudioManager.instance.FadeOutMusic(1.5f);
        AudioManager.instance.PlaySFX(gameOver, transform.position);
        panelGameOver.SetActive(true);
    }
    public void Pause()
    {
        if (panelPause.activeInHierarchy == false)
        {
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
