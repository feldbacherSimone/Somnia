using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    [SerializeField] Flash flash; 
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(LoadScene());
        }
    }
    IEnumerator LoadScene()
    {
        flash.PlayFlash();
        SoundManager.PlaySound(SoundManager.Sound.TouchPortal, SoundManager.Mixer.SFX);
        Cursor.lockState = CursorLockMode.None;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(3);
    }
}
