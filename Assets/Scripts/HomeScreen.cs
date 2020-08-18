using UnityEngine;


public class HomeScreen : MonoBehaviour
{


    public Animator animatorHomeScreen;


    private void Awake()
    {

        Instance = this;

    }


    public void StartRunning()
    {

        animatorHomeScreen.SetTrigger("Hide");

    }


    public static HomeScreen Instance { set; get; }


}
