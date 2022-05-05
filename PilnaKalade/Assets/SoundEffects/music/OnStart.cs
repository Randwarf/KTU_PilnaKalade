using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
            
        }
    }

}
