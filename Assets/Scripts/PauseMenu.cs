using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Cinemachine; 


public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    [SerializeField] private GameObject pauseMenuUI;

    [SerializeField] private AudioMixer mixer; 

    [SerializeField] private Button b_Resume;
    [SerializeField] private Button b_BackToMenu;

    [SerializeField] private Slider s_MasterVol;
    [SerializeField] private Slider s_SFXVol;
    [SerializeField] private Slider s_MusicVol;
    [SerializeField] private Slider s_CameraSens;

    [SerializeField] private Cinemachine.CinemachineFreeLook cinemachine;


    [SerializeField] private float minY = 0.5f, maxY = 4;
    [SerializeField] private float minx = 100, maxx = 300;

    private float lastSliderVal; 

    private void Start()
    {
        InitButtons();
        InitSliders(s_MasterVol, "masterVol", SoundManager.Mixer.SFX);
        InitSliders(s_MusicVol, "musicVol", SoundManager.Mixer.Music);
        InitSliders(s_SFXVol, "sfxVol", SoundManager.Mixer.SFX);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
                SoundManager.PlaySound(SoundManager.Sound.TitleFade, SoundManager.Mixer.SFX); 

            }
            else
            {
                Pause();
                SoundManager.PlaySound(SoundManager.Sound.TitleFade, SoundManager.Mixer.SFX);
            }

        }

    }
    void InitSliders(Slider slider, string parameter, SoundManager.Mixer channel)
    {
        slider.onValueChanged.AddListener((value) =>
        {
            mixer.SetFloat(parameter, Mathf.Log10(value) * 20);
            if (value > lastSliderVal + 0.1f || value < lastSliderVal - 0.1f)
            {
                SoundManager.PlaySound(SoundManager.Sound.MenuButton, SoundManager.Mixer.SFX);
                lastSliderVal = value;
            }
        }
        );
    }

    void InitButtons()
    {
        b_Resume.onClick.AddListener(() =>
        {
            Resume();
            SoundManager.PlaySound(SoundManager.Sound.MenuButton, SoundManager.Mixer.SFX);
        });
        b_BackToMenu.onClick.AddListener(() =>
        {
            SoundManager.PlaySound(SoundManager.Sound.MenuButton, SoundManager.Mixer.SFX);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 1f;
            gameIsPaused = false;
            SceneManager.LoadScene(0); 

        });
        s_CameraSens.onValueChanged.AddListener((value) =>
        {
            cinemachine.m_YAxis.m_MaxSpeed = Mathf.Lerp(minY, maxY, value);
            cinemachine.m_XAxis.m_MaxSpeed = Mathf.Lerp(minx, maxx, value);
        });
    }

    void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; 
        gameIsPaused = false; 
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; 
        gameIsPaused = true; 
    }
}
