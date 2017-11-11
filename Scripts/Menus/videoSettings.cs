using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class videoSettings : MonoBehaviour {

    public Toggle fullscreenToggle;
    public Dropdown resolutionDropDown;
    public Dropdown textureQualityDD;
    public Dropdown aaDD;
    public Dropdown vSyncDD;

    //public Slider musicVolumeS;

    public Resolution[] resolutions;
    
    
    void OnEnable()
    {
        

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        resolutionDropDown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDD.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        aaDD.onValueChanged.AddListener(delegate { OnAAChange(); });
        vSyncDD.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        
        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutionDropDown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
    }
    public void OnFullScreenToggle()
    {
        Screen.fullScreen = fullscreenToggle.isOn; //Chain making sure fullscreen is on
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropDown.value].width, resolutions[resolutionDropDown.value].height, Screen.fullScreen);
    }
    public void OnAAChange()
    {
        QualitySettings.antiAliasing = (int)Mathf.Pow(2f, aaDD.value);
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit =  textureQualityDD.value; //Chain making sure resolution is changed and equal to the right settings
        
    }
    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = vSyncDD.value;
    }
              
}

