using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
public class MainMenu : MonoBehaviour
{
    Scene scene;
    [SerializeField] CameraTarget camTarget; 
    
    [SerializeField] private Vector3 initCamera;
    [SerializeField] private Vector3 mainMenuCam;
    [SerializeField] private Vector3 optionsCam; 

    [SerializeField] AnimationCurve curve;
    [SerializeField] float duration = 1f;

    [SerializeField] GameObject mainMenueObject;
    [SerializeField] GameObject optionsObject;
    [SerializeField] GameObject startScreenObject; 

    [SerializeField] Button b_Options;
    [SerializeField] Button b_Play;
    [SerializeField] Button b_ExitGame;
    [SerializeField] Button b_Back;

    [SerializeField] SpriteRenderer title; 

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
    }
    

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
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
