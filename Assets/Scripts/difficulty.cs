using System.Collections;
using UnityEngine;

public class difficulty : MonoBehaviour
{
    static bool reducePill = false;
    public static float pSpeedIncreaseAmount = 0.1058823529411765f;
    static float npcSpeedIncreaseAmount = 0.0741176470588235f;
    public static int j = 6;

    void Awake()
    {
        //deletePlayerPrefs();
    }

    private void Start()
    {
    }

    void Update()
    {
        
    }

    public void setDifficulty(int level)
    {
        //StartCoroutine(IloadLevelControl());

        difficultySettings(level);
            food.eatenFood = 0;
            GameManager.levelNumber = level;
        //loadLevel(level);
        
        print("dif-- enemy:" + GameManager.levelEnemyCount + " pill" + GameManager.levelPillCount);
    }

    void difficultySettings(int level)
    {
        GameManager.levelEnemyCount = 2;
        GameManager.levelPillCount = 2;
        npcFollow.npcSpeed = 1.4f;
        pacmanControl.speed = 2f;
        timeManagment.time = 15f;

        for (int i = 1; i <= level; i++)
        {
            if (i % 4 == 0)
            {
                timeManagment.time += 5;
            }

            if (i % 6 == 0)
            {
                GameManager.levelEnemyCount++;
                if (!reducePill) GameManager.levelPillCount++;
            }

            if (i % j == 0)
            {

                if (pacmanControl.speed < 3.8 && npcFollow.npcSpeed < 2.66f)
                {
                    npcFollow.npcSpeed += npcSpeedIncreaseAmount;
                    pacmanControl.speed += pSpeedIncreaseAmount;
                    print("amount:" + pacmanControl.speed);
                }
                else if(j==6)
                {
                    npcFollow.npcSpeed = 2.66f;
                    pacmanControl.speed = 3.8f;
                }
            }

            if(i == 12) reducePill = true;

            if (i % 30 == 0)
                GameManager.levelPillCount--;

            print(i + ".level pill:" + GameManager.levelPillCount);
            print(i + ".level enemy:" + GameManager.levelEnemyCount);
            print(i + ".level time:" + timeManagment.time);
            print(i + ".levelSpeed enemy:" + npcFollow.npcSpeed + " pacmann:" + pacmanControl.speed);
        }

        if (GameManager.levelPillCount > 4) { GameManager.levelPillCount = 4; print("istisna"); }
        else if (GameManager.levelPillCount < 1) {GameManager.levelPillCount = 1; print("istisnam"); }
        print("enemySpeed: " + npcFollow.npcSpeed);
        print("dif Speed:" + pacmanControl.speed);
    }

    IEnumerator timeOption(int level)
    {
        yield return new WaitForSeconds(0.1f);
        timeManagment.time += 5;
        if (level % 2 == 0) GetComponent<GameManager>().timeSettings();
    }

    public static void saveLevel(int level)
    {
       //string keyEnemyCount = key + level + " Enemy";
       //string keyPillCount = key + level +" Pill";
       //string keyGameSpeed = key + level +" Speed";
        //string keyLevelTime = key + level +" Time";
        //PlayerPrefs.SetInt(key, level);
        PlayerPrefs.SetInt("maxLevel", GameManager.maxLevelNumber);
        //PlayerPrefs.SetInt(keyEnemyCount, GameManager.levelEnemyCount);
        //PlayerPrefs.SetInt(keyPillCount, GameManager.levelPillCount);
        //PlayerPrefs.SetFloat(keyGameSpeed, GameManager.levelSpeed);
        //PlayerPrefs.SetFloat(keyLevelTime, timeManagment.time);
        //if (GameManager.maxLevelNumber == GameManager.levelNumber) timeManagment.time = timeManagment.maxTime;
        print("keyLevelTime"+ timeManagment.time);
        print("saved maxLevel: "+ GameManager.maxLevelNumber);
    }

    static void loadLevel(int level)
    {
        GameManager.maxLevelNumber = PlayerPrefs.GetInt("maxLevel");
        //string key = "Level";
        GameManager.levelNumber = level;
        print("loading level:" + GameManager.levelNumber);
        print("tiklanan level:" + level);
        //GameManager.levelEnemyCount = PlayerPrefs.GetInt(key + level + " Enemy");
        //GameManager.levelPillCount = PlayerPrefs.GetInt(key + level + " Pill");
        //GameManager.levelSpeed = PlayerPrefs.GetFloat(key + level + " Speed");
        //timeManagment.time = PlayerPrefs.GetFloat(key + level + " Time");
    }

    //IEnumerator IsetLevelText(int level)
    //{
    //    print("gecikmeli leveltext");
    //    yield return new WaitForSeconds(1f);
    //    GameManager.SetLevelText(level);
    //    print("gecikmeli leveltext bitti");
    //}

    //IEnumerator IloadLevelControl()
    //{
    //    print("load level control1");
    //    yield return new WaitForSeconds(1f);
    //    print("load level control2");
    //    if (GameManager.levelNumber % 2 == 0)
    //        GetComponent<GameManager>().levelTwoSettings();
    //    else
    //        GetComponent<GameManager>().levelOneSettings();
    //}

    void deletePlayerPrefs()
    {
        print("silindi");
        PlayerPrefs.DeleteAll();
    }
}
