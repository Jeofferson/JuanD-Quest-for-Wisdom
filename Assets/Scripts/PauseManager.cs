using UnityEngine;

public class PauseManager : MonoBehaviour
{


    private GameManager gameManager;

    public Animator animatorPauseManager;


    private void Awake()
    {

        gameManager = FindObjectOfType<GameManager>();

    }


    public void OnButtonPause()
    {

        Time.timeScale = 0f;
        animatorPauseManager.SetTrigger("Show");

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
