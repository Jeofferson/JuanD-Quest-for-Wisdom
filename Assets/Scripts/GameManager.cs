using UnityEngine;

public class GameManager : MonoBehaviour
{


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

        if (!hasStartedRunning && MobileInput.Instance.Tap)
        {

            hasStartedRunning = true;

            playerMotor.StartRunning();
            cameraMotor.StartRunning();
            statsManager.StartRunning();

        }

        //if (!hasStartedRunning) { return; }

    }


    public static GameManager Instance { set; get; }


}
