using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    private GameManager gameManager;

    public Toggle toggleBgm;
    public Toggle toggleSfx;

    public Animator animatorPauseManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnButtonPause()
    {
        Time.timeScale = 0f;

        if (PlayerPrefs.GetInt(ConstantsPlayerPrefs.IS_BGM_ON) == 1)
        {
            toggleBgm.isOn = true;
        }
        else
        {
            toggleBgm.isOn = false;
        }

        animatorPauseManager.SetTrigger("Show");
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

    public void OnButtonHome()
    {
        Time.timeScale = 1f;
        gameManager.LoadSceneGame(false);
    }

    public void OnButtonResume()
    {
        Time.timeScale = 1f;
        animatorPauseManager.SetTrigger("Hide");
    }

}
