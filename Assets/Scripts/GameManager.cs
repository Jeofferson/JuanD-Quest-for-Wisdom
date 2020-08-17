using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static bool startImmediatelyFromPreviousRun;
    private bool hasStartedRunning;

    private PlayerMotor playerMotor;
    private CameraMotor cameraMotor;
    private StatsManager statsManager;


    private void Awake()
    {

        Instance = this;

        playerMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        cameraMotor = FindObjectOfType<CameraMotor>();
        statsManager = FindObjectOfType<StatsManager>();

    }


    private void Update()
    {

        if (!hasStartedRunning && (startImmediatelyFromPreviousRun || MobileInput.Instance.Tap))
        {

            hasStartedRunning = true;

            playerMotor.StartRunning();
            cameraMotor.StartRunning();
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
