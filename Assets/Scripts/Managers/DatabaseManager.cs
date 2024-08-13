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
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _names = new List<string>();
        _scores = new List<int>();
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

    public void SaveLeaderboardEntry(string name, int score)
    {
        bool success = false;
        for (int i = 0; i < MAX_ENTRIES; i++)
        {
            if (score > _scores[i])
            {
                _names.Insert(i, name);
                _scores.Insert(i, score);
                success = true;
            }
        }

        if (success)
        {
            _names.RemoveAt(_names.Count - 1);
            _scores.Remove(_scores.Count - 1);
        }
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

    public void DEBUG_ClearDatabase()
    {
        PlayerPrefs.DeleteAll();
    }
}
