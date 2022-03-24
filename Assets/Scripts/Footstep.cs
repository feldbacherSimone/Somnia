using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    private float currentVol;
    [SerializeField] private float volumeFactor;
    public bool isGrass; 

    
    private void Start()
    {
        SoundManager.LoadMixer();
    }
    public void Step()
    {
        if (isGrass)
            SoundManager.PlaySound(SoundManager.Sound.PlayerWalkGrass, this.transform.position, currentVol * volumeFactor, "SFX");
        else
            SoundManager.PlaySound(SoundManager.Sound.PlayerWalk, this.transform.position, currentVol * volumeFactor, "SFX");
    }

    public void Land()
    {
        SoundManager.PlaySound(SoundManager.Sound.PlayerLand, this.transform.position, "SFX");
    }

    public void Jump()
    {
        SoundManager.PlaySound(SoundManager.Sound.PlayerJump, this.transform.position, "SFX");
    }
    public void setCurretnVol(float vol)
    {
        currentVol = vol; 
    }
    
}
