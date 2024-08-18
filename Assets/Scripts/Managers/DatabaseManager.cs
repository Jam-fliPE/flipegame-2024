using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private List<string> _names;
    private List<int> _scores;
    private const int MAX_ENTRIES = 5;

    public static DatabaseManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        InitDatabase();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _names = new List<string>();
        _scores = new List<int>();
        LoadData();
    }

    public bool IsScoreSuitable(int score)
    {
        LoadData();
        bool result = false;
        for (int i = 0; i < MAX_ENTRIES; i++)
        {
            if (score > _scores[i])
            {
                result = true;
                break;
            }
        }

        return result;
    }

    public List<string> GetNames()
    {
        return _names;
    }

    public List<int> GetScores()
    {
        return _scores;
    }

    public void SaveLeaderboardEntry(string name, int score)
    {
        for (int i = 0; i < MAX_ENTRIES; i++)
        {
            if (score > _scores[i])
            {
                _names.Insert(i, name);
                _scores.Insert(i, score);
                break;
            }
        }

        if (_names.Count > MAX_ENTRIES)
        {
            _names.RemoveAt(_names.Count - 1);
            _scores.Remove(_scores.Count - 1);
        }

        SaveData();
    }

    private void SaveData()
    {
        for (int i = 0; i < MAX_ENTRIES; i++)
        {
            string key = string.Format("name_{0}", i);
            PlayerPrefs.SetString(key, _names[i]);

            key = string.Format("score_{0}", i);
            PlayerPrefs.SetInt(key, _scores[i]);
        }
    }

    private void LoadData()
    {
        _names.Clear();
        for (int i = 0; i < MAX_ENTRIES; i++)
        {
            string key = string.Format("name_{0}", i);
            _names.Add(PlayerPrefs.GetString(key));

            key = string.Format("score_{0}", i);
            _scores.Add(PlayerPrefs.GetInt(key));
        }
    }

    private void InitDatabase()
    {
        if (!PlayerPrefs.HasKey("leaderboardInitialized"))
        {
            for (int i = 0; i < MAX_ENTRIES; i++)
            {
                string key = string.Format("name_{0}", i);
                PlayerPrefs.SetString(key, "");

                key = string.Format("score_{0}", i);
                PlayerPrefs.SetInt(key, 0);
            }

            PlayerPrefs.SetInt("leaderboardInitialized", 1);
            PlayerPrefs.Save();
        }
    }

    [ContextMenu("Clear Player Prefs")]
    public void DEBUG_ClearDatabase()
    {
        PlayerPrefs.DeleteAll();
    }

    [ContextMenu("Print Player Prefs")]
    public void DEBUG_PrintPlayerPrefs()
    {
        Debug.Log("=== Leaderboard Data ===");
        if (PlayerPrefs.HasKey("leaderboardInitialized"))
        {
            for (int i = 0; i < MAX_ENTRIES; i++)
            {
                string key = string.Format("name_{0}", i);
                string name = PlayerPrefs.GetString(key);

                key = string.Format("score_{0}", i);
                int score = PlayerPrefs.GetInt(key);

                Debug.Log(string.Format("{0}: {1}", name, score));
            }
        }
    }
}
