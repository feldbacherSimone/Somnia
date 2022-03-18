using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;


public class Flash : MonoBehaviour
{

    public float speed; 

    bool isFlashing;
    public float aMin;
    public float bMax;
    float t = 1;

    float intensity;

    VolumeProfile volumeProfile;


    Bloom bloom;

    private void Start()
    {
        volumeProfile = GetComponent<Volume>().sharedProfile;
        volumeProfile.TryGet(out bloom);
        }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("click");
            isFlashing = true; 
            StartCoroutine(PlayFlash(speed));
        }
    }

    IEnumerator PlayFlash(float speed)
    {

        while(isFlashing)
        {
            print("isPlaying");

            intensity = Mathf.Lerp(aMin, bMax, t);
            bloom.threshold.value = intensity;
            t -= speed;
            yield return null;

            if (t <= 0)
                isFlashing = false; 

        }
       

       
        while (!isFlashing)
        {
            print("isPlaying");
            t += speed;

            if (t >= 1)
            {

                bloom.threshold.value = bMax; 
                StopAllCoroutines();
            }

            intensity = Mathf.Lerp(aMin, bMax, t);
            bloom.threshold.value = intensity;
           
            yield return null;

            if (t >= 1)
                StopAllCoroutines();
        }
    }
}
