using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio; 
public class MainMenu : MonoBehaviour
{

    //this is all in one class because I hate myself actually 
    Scene scene;

    [Header("Camera")]
    [SerializeField] CameraTarget camTarget;

    [Tooltip("This refers to the offset variables in the Mouse target and not the actual Camera Position")]
    [SerializeField] private Vector3 initCamera;
    [SerializeField] private Vector3 mainMenuCam;
    [SerializeField] private Vector3 optionsCam; 

    [SerializeField] AnimationCurve curve;
    [SerializeField] float duration = 1f;

    [Header("Menues")]

    [SerializeField] GameObject mainMenueObject;
    [SerializeField] GameObject optionsObject;
    [SerializeField] GameObject startScreenObject; 

    [Header("Buttons")]
   
    [SerializeField] Button b_Options;
    [SerializeField] Button b_Play;
    [SerializeField] Button b_ExitGame;
    [SerializeField] Button b_Back;
    [SerializeField] Button b_Controlls; 

    [Header("Sliders")]
    [SerializeField] Slider s_VolMaster;
    [SerializeField] Slider s_VolSFX;
    [SerializeField] Slider s_VolMusic; 

    [SerializeField] SpriteRenderer title;
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Button[] buttons; 

    [SerializeField] Dropdown dropdown;

    bool startScrren = true; 
    
    void InitButtons()
    {
        b_Options.onClick.AddListener(() =>
        {
            StartCoroutine( MoveCamera(mainMenuCam, optionsCam, optionsObject));
            mainMenueObject.SetActive(false); 
        }
        );

        b_Back.onClick.AddListener(() =>
        {
            StartCoroutine(MoveCamera(optionsCam, mainMenuCam, mainMenueObject));
            optionsObject.SetActive(false);
        }
        );

        b_Play.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(scene.buildIndex + 1);
        }

         );
        b_ExitGame.onClick.AddListener(() =>
        {
            Application.Quit();
        }
        );
        dropdown.onValueChanged.AddListener((value) =>
        {
            QualitySettings.SetQualityLevel(value*2);
            SoundManager.PlaySound(SoundManager.Sound.MenuButton, "SFX");
        });


    }
    
    void InitSliders(Slider slider, string parameter)
    {
        slider.onValueChanged.AddListener((value) =>
        {
            audioMixer.SetFloat(parameter, Mathf.Log10(value) * 20 );
            SoundManager.PlaySound(SoundManager.Sound.MenuButton, "SFX");
        }
        );
    }

    void AssignButtonSounds()
    {
        foreach(Button button in buttons)
        {
            button.onClick.AddListener(() =>
            {
                SoundManager.PlaySound(SoundManager.Sound.MenuButton, "SFX");

            });
        }
    }
 

    private void Start()
    {

        SoundManager.LoadMixer();
        scene = SceneManager.GetActiveScene();
        AssignButtonSounds();
        InitSliders(s_VolMaster, "masterVol");
        InitSliders(s_VolMusic, "musicVol");
        InitSliders(s_VolSFX, "sfxVol"); 

        InitButtons();
    }
    private void Update()
    {
        if (Input.anyKeyDown && startScrren)
        {
            StartCoroutine(MoveCamera(initCamera, mainMenuCam, mainMenueObject));
            StartCoroutine(FadeTitle());
            startScreenObject.SetActive(false);
            startScrren = false; 
        }
    }

    IEnumerator FadeTitle()
    {
       float deltaTime = 0;
       float a = 0;

        do
        {
            Color color = new Color(title.color.r, title.color.g, title.color.b, Mathf.Lerp(1, 0, curve.Evaluate(a)));
            title.color = color;

            deltaTime += Time.deltaTime;
            a = deltaTime / duration;

            print(color);
            yield return null;
        }
        while (a <= 1.1f);

     

        yield return null;
        StopAllCoroutines();
    }

    IEnumerator MoveCamera(Vector3 currentPos, Vector3 destination, GameObject activate)
    {

        print("corutine Called"); 
        float deltaTime = 0;

        float a = 0;

        do
        {
            Vector3 pos = Vector3.Lerp(currentPos, destination, curve.Evaluate(a));
            camTarget.xOffset = pos.x;
            camTarget.yOffset = pos.y;
            camTarget.zDepth = pos.z; 

            deltaTime += Time.deltaTime;
            a = deltaTime / duration;
            print(a);

            yield return null;
        } while (a <= 1.1f);


        activate.SetActive(true); 
        yield return null;
        StopAllCoroutines();
        /*while (currentPos.x < destination.x)
        {
            moveToDestination();
            yield return null; 
        }
        ;*/
    }

}
