using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    private float currentVol;
    [SerializeField] private float volumeFactor; 
    public void Step()
    {
        SoundManager.PlaySound(SoundManager.Sound.PlayerWalk, this.transform.position, currentVol*volumeFactor);
    }

    public void Land()
    {
        SoundManager.PlaySound(SoundManager.Sound.PlayerLand, this.transform.position);
    }

    public void Jump()
    {
        SoundManager.PlaySound(SoundManager.Sound.PlayerJump, this.transform.position);
    }
    public void setCurretnVol(float vol)
    {
        currentVol = vol; 
    }
    
}
