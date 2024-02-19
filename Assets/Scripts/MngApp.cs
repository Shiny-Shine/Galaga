using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MngApp : MonoBehaviour
{
    public static MngApp appInst;

    private string nick;
    public int bestF, bestA, bestG;
    private int best, usern;

    public string nowScene;
    private const string PLAYER = "Player";
    private const string COUNT = "Count";
    private const string USER = "User";
    private List<string> names = new List<string>();
    private Dictionary<string, int> dat = new Dictionary<string, int>();

    public string Name
    {
        get { return nick; }
        set { nick = value; }
    }

    public int Best
    {
        get { return best; }
        set { best = value; }
    }

    private int[] arScores = new int[10];
    private string[] arNames = new string[10];

    private string defaultScores = "0,0,0,0,0,0,0,0,0,0";
    private string defaultNames = "N/A,N/A,N/A,N/A,N/A,N/A,N/A,N/A,N/A,N/A";

    private void Awake()
    {
        if (appInst != null && appInst != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            appInst = this;
            DontDestroyOnLoad(gameObject);
        }
        
        Load("F");
        Load("A");
        Load("G");
    }

    void Load(string nowScene)
    {
        // 기록된 유저들이 몇명인지 로드
        usern = PlayerPrefs.GetInt(nowScene + COUNT, 0);
        nick = PlayerPrefs.GetString(nowScene + USER, "none");

        // 저장되있는 유저들의 최고점수를 로딩
        for (int i = 0; i < usern; i++)
        {
            // Player1, Player2 ...
            names.Add(PlayerPrefs.GetString(nowScene + PLAYER + i, "n/a"));
            int _best = PlayerPrefs.GetInt(nowScene + names[i], 0);
            dat.Add(nowScene + names[i], _best);
            if (names[i] == nick)
            {
                best = _best;
                if (nowScene == "F")
                    bestF = best;
                else if (nowScene == "A")
                    bestA = best;
                else if (nowScene == "G")
                    bestG = best;
            }
        }

        print(string.Format("{0} players data load complete...", usern));

        // 결과창 데이터 로드
        string scores = PlayerPrefs.GetString(nowScene + "Scores", defaultScores);
        string snames = PlayerPrefs.GetString(nowScene + "Names", defaultNames);

        // names set
        string[] tmps = snames.Split(',');
        string[] tmpi = scores.Split(',');
        for (int i = 0; i < 10; i++)
        {
            arNames[i] = tmps[i];
            arScores[i] = int.Parse(tmpi[i]);
        }
    }

    public void Save(string nowScene)
    {
        PlayerPrefs.SetInt(nowScene + COUNT, usern);
        PlayerPrefs.SetString(nowScene + USER, nick);
        for (int i = 0; i < usern; i++)
        {
            // 각 플레이어의 최고 점수를 저장
            PlayerPrefs.SetString(nowScene + PLAYER + i, names[i]);
            PlayerPrefs.SetInt(nowScene + names[i], dat[names[i]]);
            if (names[i] == nick)
            {
                if (nowScene == "F")
                    bestF = best;
                else if (nowScene == "A")
                    bestA = best;
                else if (nowScene == "G")
                    bestG = best;
            }
        }

        // 점수판 데이터 저장
        string _scores = "" + arScores[0];
        string _names = arNames[0];
        for (int i = 1; i < 10; i++)
        {
            _scores += "," + arScores[i];
            _names += "," + arNames[i];
        }

        PlayerPrefs.SetString(nowScene + "Scores", _scores);
        PlayerPrefs.SetString(nowScene + "Names", _names);
    }

    public void SceneUpdate()
    {
        nowScene = SceneManager.GetActiveScene().name[0].ToString();
    }

    public void SetData(int index, string name, int score)
    {
        arNames[index] = name;
        arScores[index] = score;
    }

    public void GetData(int index, out string out_name, out int out_score)
    {
        out_name = arNames[index];
        out_score = arScores[index];
    }

    public void updateBest(int _score, string nowScene)
    {
        if (best < _score)
        {
            best = _score;
            dat[nick] = best;
        }

        Save(nowScene);
    }

    public void changeUser(string _name)
    {
        if (dat.ContainsKey(_name))
        {
            nick = _name;
            best = dat[nick];
        }
        else
        {
            usern++;
            nick = _name;
            best = 0;
            names.Add(nick);
            dat.Add(nick, best);
        }

        string nowScene = SceneManager.GetActiveScene().name[0].ToString();
        if (nowScene == "T")
        {
            Save("F");
            Save("A");
            Save("G");
            return;
        }

        Save(nowScene);
    }

    public string getRankString()
    {
        string res = "";
        for (int i = 0; i < 10; i++)
        {
            res += string.Format("{0:D2}. {1} ({2:#,0})\n",
                i + 1, arNames[i], arScores[i]);
        }

        return res;
    }
}