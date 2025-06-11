using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Graphics Buttons")]
    public Button lowGraphicsButton;
    public Button highGraphicsButton;

    [Header("Audio Sources")]
    public AudioSource backgroundMusic;
    public AudioSource[] soundEffects; 

    void Start()
    {

        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);
        int graphicsLevel = PlayerPrefs.GetInt("GraphicsQuality", QualitySettings.names.Length - 1); 


        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;

        ApplyMusicVolume(musicVol);
        ApplySFXVolume(sfxVol);
        QualitySettings.SetQualityLevel(graphicsLevel);

        musicSlider.onValueChanged.AddListener(ApplyMusicVolume);
        sfxSlider.onValueChanged.AddListener(ApplySFXVolume);
        lowGraphicsButton.onClick.AddListener(SetLowGraphics);
        highGraphicsButton.onClick.AddListener(SetHighGraphics);
    }


    public void ApplyMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        if (backgroundMusic != null)
            backgroundMusic.volume = volume;
    }

    public void ApplySFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        foreach (AudioSource sfx in soundEffects)
        {
            if (sfx != null)
                sfx.volume = volume;
        }
    }

    public void SetLowGraphics()
    {
        QualitySettings.SetQualityLevel(0);
        PlayerPrefs.SetInt("GraphicsQuality", 0);
        Debug.Log("Graphics set to LOW");
    }

    public void SetHighGraphics()
    {
        int high = QualitySettings.names.Length - 1;
        QualitySettings.SetQualityLevel(high);
        PlayerPrefs.SetInt("GraphicsQuality", high);
        Debug.Log("Graphics set to HIGH");
    }
}
