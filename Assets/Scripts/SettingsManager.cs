using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    public Toggle toggleBgm;
    public Toggle toggleSfx;

    public Animator animatorSettingsManager;

    public void OnButtonSettings()
    {
        if (PlayerPrefs.GetInt(ConstantsPlayerPrefs.IS_BGM_ON) == 1)
        {
            toggleBgm.isOn = true;
        }
        else
        {
            toggleBgm.isOn = false;
        }

        animatorSettingsManager.SetTrigger("Show");
    }

    public void OnButtonClose()
    {
        animatorSettingsManager.SetTrigger("Hide");
    }

    public void ToggleBgm(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt(ConstantsPlayerPrefs.IS_BGM_ON, 1);
            AudioManager.instance.Unmute(Constants.BGM_01);
            AudioManager.instance.Unmute(Constants.BGM_02);
        }
        else
        {
            PlayerPrefs.SetInt(ConstantsPlayerPrefs.IS_BGM_ON, 0);
            AudioManager.instance.Mute(Constants.BGM_01);
            AudioManager.instance.Mute(Constants.BGM_02);
        }
    }

}