using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio; 
public class EndScreen : MonoBehaviour
{

    //this is all in one class because I hate myself actually 
    Scene scene;

    [Header("Camera")]
    [SerializeField] CameraTarget camTarget;

    [Tooltip("This refers to the offset variables in the Mouse target and not the actual Camera Position")]
    [SerializeField] private Vector3 initCamera;
    [SerializeField] private Vector3 cam1;
    [SerializeField] private Vector3 cam2; 

    [SerializeField] AnimationCurve curve;
    [SerializeField] float duration = 1f;

    [Header("Menues")]

    [SerializeField] GameObject mainMenueObject;
    [SerializeField] GameObject optionsObject;
    [SerializeField] GameObject startScreenObject; 

    [Header("Buttons")]

    [SerializeField] Button b_ExitGame;
    [SerializeField] Button b_Back;

    [SerializeField] SpriteRenderer title;
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Flash flash; 

    bool startScrren = true;
    bool secondScreen = false; 
    float lastSliderVal; 

    void InitButtons()
    {
        b_Back.onClick.AddListener(() =>
        {
            SoundManager.PlaySound(SoundManager.Sound.MenuButton, SoundManager.Mixer.SFX);
            SceneManager.LoadScene(0);
        }
     );
        b_ExitGame.onClick.AddListener(() =>
        {
            SoundManager.PlaySound(SoundManager.Sound.MenuButton, SoundManager.Mixer.SFX);
            Application.Quit();
        }
      );

    }



 

    private void Start()
    {
        flash.PlayFlash(false);
        SoundManager.LoadMixer();
        scene = SceneManager.GetActiveScene();


        InitButtons();
    }
    private void Update()
    {
        if (Input.anyKeyDown && startScrren)
        {
            SoundManager.PlaySound(SoundManager.Sound.TitleFade, SoundManager.Mixer.SFX); 
            StartCoroutine(MoveCamera(initCamera, cam1, mainMenueObject));
            StartCoroutine(FadeTitle());
            startScreenObject.SetActive(false);
            startScrren = false;
           
        }
        if(Input.anyKeyDown && secondScreen)
        {
            SoundManager.PlaySound(SoundManager.Sound.TitleFade, SoundManager.Mixer.SFX);
            StartCoroutine(MoveCamera(cam1, cam2, optionsObject));
            mainMenueObject.SetActive(false);
          
        }
    }

    IEnumerator FadeTitle()
    {
       float deltaTime = 0;
       float a = 0;

        do
        {
            Color color = new Color(title.color.r, title.color.g, title.color.b, Mathf.Lerp(0, 1, curve.Evaluate(a)));
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


        secondScreen = secondScreen ? false : true; 
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
