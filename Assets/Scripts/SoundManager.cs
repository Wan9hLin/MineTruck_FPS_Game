using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource bgm;

    public AudioSource[] soudEffects;
    public bool isplaying;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stopBgm()
    {
        bgm.Stop();
    }

    

   public void PlaySFX(int sfxnumber)
   {
        
        soudEffects[sfxnumber].Play();
        isplaying = soudEffects[sfxnumber].isPlaying;

    }
   
   public void StopSFX(int sfxnumber)
   {
       soudEffects[sfxnumber].Stop();
   }

}
