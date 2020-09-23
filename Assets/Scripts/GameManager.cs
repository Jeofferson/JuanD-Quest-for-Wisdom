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

        // This block of code will only run on the very first time the app is opened...
        if (PlayerPrefs.GetInt(ConstantsPlayerPrefs.IS_APP_OPENED_BEFORE) == 0)
        {

            PlayerPrefs.SetInt(ConstantsPlayerPrefs.IS_BGM_ON, 1);
            PlayerPrefs.SetInt(ConstantsPlayerPrefs.IS_SFX_ON, 1);

            PlayerPrefs.SetInt(ConstantsPlayerPrefs.IS_APP_OPENED_BEFORE, 1);

        }
        // This block of code will run every time the app is opened except for the very first time...
        else
        {

            if (PlayerPrefs.GetInt(ConstantsPlayerPrefs.IS_BGM_ON) == 1)
            {

                AudioManager.instance.Unmute(Constants.BGM_01);
                AudioManager.instance.Unmute(Constants.BGM_02);

            }
            else
            {

                AudioManager.instance.Mute(Constants.BGM_01);
                AudioManager.instance.Mute(Constants.BGM_02);

            }

        }

    }


    private void Start()
    {

        int bgmNumber = new System.Random().Next(0, 2);

        switch (bgmNumber)
        {

            case 0:
                SuperGlobals.currentBgm = Constants.BGM_01;
                AudioManager.instance.Play(Constants.BGM_01);
                break;

            case 1:
                SuperGlobals.currentBgm = Constants.BGM_02;
                AudioManager.instance.Play(Constants.BGM_02);
                break;

        }

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

        AudioManager.instance.Stop(SuperGlobals.currentBgm);

        startImmediatelyFromPreviousRun = startImmediately;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");

    }


    public static GameManager Instance { set; get; }


}
