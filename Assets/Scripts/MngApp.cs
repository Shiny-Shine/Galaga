using System.Collections.Generic;
using UnityEngine;

public class MngApp : MonoBehaviour
{
    public static MngApp appInst;
    public string loginUser;

    private string nick;
    private int best, usern;

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
        Load ();
    }

    void Load()
    {
        usern = PlayerPrefs.GetInt(COUNT, 0);
        nick = PlayerPrefs.GetString(USER, "none");

        for (int i = 0; i < usern; i++)
        {
            names.Add(PlayerPrefs.GetString(PLAYER + i, "n/a"));
            int _best = PlayerPrefs.GetInt(names[i], 0);
            dat.Add(names[i], _best);
            if (names[i] == nick) best = _best;
        }

        print(string.Format("{0} players data load complete...", usern));
        /*
        foreach (var x in dat.Keys) print (x);
        */
        string scores = PlayerPrefs.GetString("Scores", defaultScores);
        string snames = PlayerPrefs.GetString("Names", defaultNames);

        // names set
        string[] tmps = snames.Split(',');
        string[] tmpi = scores.Split(',');
        for (int i = 0; i < 10; i++)
        {
            arNames[i] = tmps[i];
            arScores[i] = int.Parse(tmpi[i]);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt(COUNT, usern);
        PlayerPrefs.SetString(USER, nick);
        for (int i = 0; i < usern; i++)
        {
            PlayerPrefs.SetString(PLAYER + i, names[i]);
            PlayerPrefs.SetInt(names[i], dat[names[i]]);
        }

        string _scores = "" + arScores[0];
        string _names = arNames[0];
        for (int i = 1; i < 10; i++)
        {
            _scores += "," + arScores[i];
            _names += "," + arNames[i];
        }

        PlayerPrefs.SetString("Scores", _scores);
        PlayerPrefs.SetString("Names", _names);
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

    public void updateBest(int _score)
    {
        if (best < _score)
        {
            best = _score;
            dat[nick] = best;
        }

        Save();
    }

    public void changeUser(string _name)
    {
        if(_name == "")
            return;
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

        Save();
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