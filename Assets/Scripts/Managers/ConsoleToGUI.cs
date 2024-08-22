using UnityEngine;

public class ConsoleToGUI : MonoBehaviour
{
    static string myLog = "";
    private string output;
    private string stack;

    private bool isActive = false;

    public static ConsoleToGUI _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            isActive = !isActive;
        }
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
        myLog = output + "\n" + myLog;
        if (myLog.Length > 5000)
        {
            myLog = myLog.Substring(0, 4000);
        }
    }
    void OnGUI()
    {
        if (isActive) //Do not display in editor ( or you can use the UNITY_EDITOR macro to also disable the rest)
        {
            myLog = GUI.TextArea(new Rect(10, 10, Screen.width - 10, Screen.height - 10), myLog);
        }
    }
}
