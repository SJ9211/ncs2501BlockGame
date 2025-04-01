using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TMP_InputField inputField;

    private const string HIGHSCORE_KEY = "HIGHSCORE";
    private const string BESTPLAYER_KEY = "BESTPLAYER";
    private const string NONAME = "NoName";
    private string userName;

    public string UserName
    {
        get { return userName; }
        set
        {
            // if (value.Length == 0)
            // {
            //     userName = NONAME;
            // }
            userName = (value.Length == 0) ? NONAME : value;
        }
    }

    public string BestPlayer
    {
        get { return PlayerPrefs.GetString(BESTPLAYER_KEY, NONAME); }
        set { PlayerPrefs.SetString(BESTPLAYER_KEY, value); }

    }

    public int BestScore
    {
        get
        {
            return PlayerPrefs.GetInt(BESTPLAYER_KEY, 0);
        }
        set
        {
            if (BestScore <= value)
            {

                PlayerPrefs.SetInt(BESTPLAYER_KEY, value);
                BestPlayer = userName;
            }

        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        bestScoreText.text = GetBestText();
    }

    public string GetBestText()
    {
        return $"BestPlayer : {BestPlayer} : {BestScore}";
    }

    public void UpdateName()
    {
        name = inputField.text;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    [MenuItem("BlockGame/Reset score")]
    public static void ResetBestScore()
    {
        PlayerPrefs.SetInt(BESTPLAYER_KEY, 0);
        PlayerPrefs.SetString(BESTPLAYER_KEY, NONAME);
    }
}

