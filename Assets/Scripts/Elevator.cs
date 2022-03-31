using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform tragetPoint;
    private Animator animator; 

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (startPoint == null)
            startPoint = gameObject.transform; 

    }

    public void Activate()
    {
        SoundManager.PlaySound(SoundManager.Sound.AvtivateElevator, transform.position, SoundManager.Mixer.SFX);
        StartCoroutine(WaitAndAnimate(2f));
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = this.transform; 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null; 
        }
    }

   
    
    IEnumerator WaitAndAnimate(float time)
    {
        yield return new WaitForSeconds(time);
        animator.enabled = true;
        Debug.Log("Elevator Activated");
        StopAllCoroutines();
    }
}
