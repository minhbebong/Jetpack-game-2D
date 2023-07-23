using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenuPanel;
    public ParticleSystem jetpack;
    public AudioClip coinCollectSound;
    public AudioSource jetpackAudio;
    public AudioSource footstepsAudio;
    public TextManager textManager;

    public void Pause()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        textManager = FindObjectOfType<TextManager>();
        Time.timeScale = 1f;
        textManager.score = 0;
        SceneManager.LoadScene("JetpackGame");
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start Scene");
    }
}
