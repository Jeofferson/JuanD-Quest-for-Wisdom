using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static bool startImmediatelyFromPreviousRun;
    private bool hasStartedRunning;

    private PlayerMotor playerMotor;
    private CameraMotor cameraMotor;
    private HomeScreen homeScreen;
    private StatsManager statsManager;


    private void Awake()
    {

        Instance = this;

        playerMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        cameraMotor = FindObjectOfType<CameraMotor>();
        homeScreen = FindObjectOfType<HomeScreen>();
        statsManager = FindObjectOfType<StatsManager>();

    }


    private void Update()
    {

        if (!hasStartedRunning && (startImmediatelyFromPreviousRun || MobileInput.Instance.Tap))
        {

            hasStartedRunning = true;

            playerMotor.StartRunning();
            cameraMotor.StartRunning();
            homeScreen.StartRunning();
            statsManager.StartRunning();

        }

        //if (!hasStartedRunning) { return; }

    }


    public void LoadSceneGame(bool startImmediately)
    {

        startImmediatelyFromPreviousRun = startImmediately;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");

    }


    public static GameManager Instance { set; get; }


}
