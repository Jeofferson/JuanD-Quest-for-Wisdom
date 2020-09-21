using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static bool startImmediatelyFromPreviousRun;
    private bool didPlayerTapStart;
    private bool hasStartedRunning;

    private TerrainManager terrainManager;
    private PlayerMotor playerMotor;
    private CameraMotor cameraMotor;
    private HomeScreen homeScreen;
    private StatsManager statsManager;


    private void Awake()
    {

        Instance = this;

        terrainManager = FindObjectOfType<TerrainManager>();
        playerMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        cameraMotor = FindObjectOfType<CameraMotor>();
        homeScreen = FindObjectOfType<HomeScreen>();
        statsManager = FindObjectOfType<StatsManager>();

    }


    private void Start()
    {

        SoundManager.PlaySound(SoundManager.Sound.bgm01);

    }


    private void Update()
    {

        if (!hasStartedRunning && (startImmediatelyFromPreviousRun || didPlayerTapStart))
        {

            hasStartedRunning = true;

            terrainManager.IsScrolling = true;
            playerMotor.StartRunning();
            cameraMotor.StartRunning();
            homeScreen.StartRunning();
            statsManager.StartRunning();

        }

        //if (!hasStartedRunning) { return; }

    }


    public void PlayerTappedStart()
    {

        didPlayerTapStart = true;

    }


    public void LoadSceneGame(bool startImmediately)
    {

        startImmediatelyFromPreviousRun = startImmediately;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");

    }


    public static GameManager Instance { set; get; }


}
