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

    public float tMin = 0;
    public float tMax; 
    float t = 0;

    float intensity;
    float threshhold; 

    VolumeProfile volumeProfile;


    Bloom bloom;


    private void Start()
    {
        volumeProfile = GetComponent<Volume>().sharedProfile;
        volumeProfile.TryGet(out bloom);



        }
    public void PlayFlash(bool reset)
    {
        StartCoroutine(PlayFlash(speed, reset)); 
    }

    IEnumerator PlayFlash(float speed, bool reset)
    {

       
       

       
        while (t <= 1)
        {
            print("isPlaying");
            t += speed;

       

            intensity = Mathf.Lerp(aMin, bMax, t);
            bloom.intensity.value = intensity;
            threshhold = Mathf.Lerp(tMax, tMin, t);
            bloom.threshold.value = threshhold;


            yield return null;



        }
        print("done");
        if (reset)
        {
            yield return new WaitForSeconds(1);

            bloom.intensity.value = aMin;
            bloom.threshold.value = tMax;
        }
    }
}
