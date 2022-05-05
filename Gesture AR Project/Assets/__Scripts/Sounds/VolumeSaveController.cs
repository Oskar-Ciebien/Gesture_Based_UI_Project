using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSaveController : MonoBehaviour
{
    // Slider Object for SFX
    public Slider sfxSlider = null;

    // Slider Object for Music
    public Slider musicSlider = null;

    // Toggle Object for mute
    public Toggle muteButton = null;

    public GameObject optionsMenu;

    private void Start()
    {
        LoadValues();
    }

    private void Update()
    {
        SaveVolumeButton();
    }

    // Save all values to playerprefs when button is pressed
    public void SaveVolumeButton()
    {
        float volumeValue = musicSlider.value;
        float sfxValue = sfxSlider.value;
        int muteValue = muteButton.isOn ? 1 : 0;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        PlayerPrefs.SetFloat("SfxValue", sfxValue);
        PlayerPrefs.SetInt("Mute", muteValue);
        

        LoadValues();
    }

    // Load all the values from playerprefs and assign them to the sliders and toggles
    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        float sfxValue = PlayerPrefs.GetFloat("SfxValue");
        bool muteValue = (PlayerPrefs.GetInt("Mute") != 0);
        musicSlider.value = volumeValue;
        sfxSlider.value = sfxValue;
        muteButton.isOn = muteValue;
        BGMusic.BGInstance._audio.volume = volumeValue;
        SFXManager.sfxInstance.Audio.volume = sfxValue;
        AudioListener.volume = (muteValue ? 0 : 1);
        print(AudioListener.pause);
    }

    // Method for playing an sfx sound, only active whilst options menu is active
    public void PlaySound()
    {
        if (optionsMenu.activeSelf)
        {
            SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Clash);
        }
       
    }
}
