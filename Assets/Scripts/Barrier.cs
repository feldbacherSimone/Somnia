using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
  
    public void DisableBarrier()
    {
        Destroy(this.gameObject, SoundManager.PlayAndWait(SoundManager.Sound.DisableBarrier, this.transform.position, SoundManager.Mixer.SFX)-1.5f); 
    }

}
