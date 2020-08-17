using UnityEngine;


public class DeathMenu : MonoBehaviour
{


    private GameManager gameManager;

    public Animator animatorDeathMenu;


    private void Awake()
    {

        Instance = this;

        gameManager = FindObjectOfType<GameManager>();

    }


    public void Die()
    {

        animatorDeathMenu.SetTrigger("Show");

    }


    public void OnButtonHome()
    {

        gameManager.LoadSceneGame(false);

    }


    public void OnButtonPlayAgain()
    {

        gameManager.LoadSceneGame(true);

    }


    public static DeathMenu Instance { set; get; }


}
