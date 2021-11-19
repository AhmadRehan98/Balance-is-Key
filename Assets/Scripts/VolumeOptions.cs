using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeOptions : MonoBehaviour
{
    public Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        // volumeSlider = GameObject.Find("volumeSlider").GetComponent<Slider>();

        Load();
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("volumeLevel"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volumeLevel");
        }
        else
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("volumeLevel", volumeSlider.value);
    }

}
