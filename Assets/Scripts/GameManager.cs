using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //public GameObject[] enemies;
    //public GameObject[] enemies2;
    public GameObject pacman;
    public GameObject camera;
    public GameObject scoreText;
    public GameObject TimeText;
    public GameObject gameOverPanel;
    public GameObject menuButton;
    public Transform mainNodesObject;
    public Transform mainPillObject1;
    public Transform mainPillObject2;
    public GameObject[] level2EnemyPrefab = new GameObject[4];

    //GameObject mainEnemyObject2;
    Animator goverPanelAnim;
    public GameObject[] prefabEnemiess;
    public static Text levelText;

    public float levelStartSpeed=2f;
    public static int levelNumber = 1;
    public static int maxLevelNumber = 1;

    public static int levelEnemyCount = 2;
    public static int levelPillCount = 2;

    void Start()
    {
        print("gmde level:" + GameManager.levelNumber);
        print("gmde maxlevel:" + GameManager.maxLevelNumber);
        levelStartSpeed = pacmanControl.speed;
        langSettings.setGameWritings();
        langSettings.changeGameLang();
        Time.timeScale = 1;
        levelText = scoreText.transform.parent.GetChild(2).GetComponent<Text>();
        StartCoroutine(levelSettingsRestart());
        CreateEnemiesPrefab();
        disabledEnemy.enemiesToArray();
        SetPowerPill();
        //difficulty.saveLevel(levelNumber);
        goverPanelAnim = gameOverPanel.GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void nextLevel()
    {
        //increaseLevel();
        //SetLevelText();
        //goverPanelAnim.SetBool("panel", false);
        StartCoroutine(levelSettingsRestart());
        pacman.GetComponent<Animator>().SetBool("death", false);
        pacmanControl.speed = levelStartSpeed;
        pacman.GetComponent<CircleCollider2D>().enabled = true;
        menuButton.SetActive(true);
    }

    public void restart()
    {
        DestroyEnemiesPrefab();
        CreateEnemiesPrefab();
        disabledEnemy.enemiesToArray();
        StartCoroutine(levelSettingsRestart());
        prisonNpcTimeReset();
        menuButton.SetActive(true);
        //gameOverPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Restart";
    }

    IEnumerator levelSettingsRestart()
    {
        SetLevelText();
        //GetComponent<difficulty>().setDifficulty(GameManager.levelNumber);
        yield return new WaitForSeconds(0f);
        if(goverPanelAnim != null) goverPanelAnim.SetBool("panel", false);
        resetSettings();
        DestroyEnemiesPrefab();
        CreateEnemiesPrefab();
        disabledEnemy.enemiesToArray();

        if (levelNumber % 2 != 0)
        {
            print("Level ONESettings");
            levelOneStartPositon();
            levelOneSettings();
        }
        else
        {
            print("Level TWOSettings");
            GetComponent<AudioSource>().Play();
            levelTwoStartPositon();
            //timeSettings();
            TimeText.GetComponent<timeManagment>().startTime();
            levelTwoSettings();
        }

        StartCoroutine(dotimeScale1());
    }

    IEnumerator dotimeScale1()
    {
        yield return new WaitForSeconds(2.5f);
        Time.timeScale = 1;
        //yield return new WaitForSeconds(0.8f);
        //Time.timeScale = 1f;
    }

    public void levelOneSettings()
    {
        Debug.LogWarning("levelOneSetting");
        //mainEnemyObject.SetActive(true);
        //mainEnemyObject2.SetActive(false);

        activeNode();
        //SetPowerPill();

        //pacman.transform.position = new Vector3(-2.17999959f, -2.37152624f, 0f);

        scoreText.SetActive(true);
        TimeText.SetActive(false);

        camera.transform.position = new Vector3(0, 0, -10);
    }

    public void levelTwoSettings()
    {
        Debug.LogWarning("levelTwoSetting");
        //mainEnemyObject.SetActive(false);
        //mainEnemyObject2.SetActive(true);

        //activePill();

        //pacman.transform.position = new Vector3(-2.17999959f, 7.96000004f, 0);

        scoreText.SetActive(false);
        TimeText.SetActive(true);

        camera.transform.position = new Vector3(0.25f, 10.0699997f, -10);
    }

    void levelOneStartPositon()
    {
        //print("l1 mainobject:" + mainEnemyObject.name);
        //mainEnemyObject.transform.position = new Vector2(0.0423643589f, -0.0615963936f);
        //mainEnemyObject.transform.GetChild(0).position = new Vector2(-0.282364279f, 0.358596355f);
        //mainEnemyObject.transform.GetChild(1).position = new Vector2(-0.662364304f, 0.358596355f);
        //mainEnemyObject.transform.GetChild(2).position = new Vector2(0.505999982f, 0.358596355f);
        //mainEnemyObject.transform.GetChild(3).position = new Vector2(0.115999997f, 0.358596355f);
        
        pacman.transform.position = new Vector3(-2.17999959f, -2.37152624f, 0f);
    }

    void levelTwoStartPositon()
    {
        //print("l2 mainobject:" + mainEnemyObject.name);
        //mainEnemyObject.transform.position = new Vector2(0.0423643589f, -0.0615963936f);
        //
        
        pacman.transform.position = new Vector3(-2.17999959f, 7.96000004f, 0f);
    }

    /*void doNpcStartState()
    {
        for (int i = 0; i < disabledEnemy.enemies.Count; i++)
        {
            disabledEnemy.enemies[i].GetComponent<npcFollow>().isGhost = false;
            StartCoroutine(waitInPrison(disabledEnemy.enemies[i].GetComponent<npcFollow>()));
        }
    levelOneStartPositon();
    }*/

    public IEnumerator waitInPrison(npcFollow npcScript)
    {
        //print("NPCLER");
        npcScript.inPrison = true;
        yield return new WaitForSeconds(npcScript.prisonTime);
        npcScript.inPrison = false;
    }

    void resetSettings()
    {
        food.eatenFood = 0;
        food.score = 0;
        scoreText.GetComponent<Text>().text = langSettings.writings[8 +langSettings.lang] + ":0";
        pacman.GetComponent<Animator>().SetBool("death", false);
        pacmanControl.speed = levelStartSpeed;
        pacman.GetComponent<CircleCollider2D>().enabled = true;
        SetPowerPill();
    }

    public void timeSettings()
    {
        timeManagment.time += 5f;
        if(timeManagment.time > timeManagment.maxTime) timeManagment.maxTime = timeManagment.time;
        Debug.LogWarning("timeSetting");
    }

    void prisonNpcTimeReset()
    {
        print("npc prison reset");
        for (int i = 0; i < disabledEnemy.enemies.Count; i++)
        {
            StartCoroutine(waitInPrison(disabledEnemy.enemies[i]));
        }
        
    }

    public IEnumerator waitInPrison(GameObject enemy)
    {
        npcFollow enemyScript = enemy.GetComponent<npcFollow>();
        enemyScript.inPrison = true;
        yield return new WaitForSeconds(enemyScript.prisonTime);
        enemyScript.inPrison = false;
    }

    public void activeNode()
    {
        foreach (Transform node in mainNodesObject)
            node.gameObject.SetActive(true);
    }

    public void CreateEnemiesPrefab()
    {
        GameObject meo;
        if (levelNumber % 2 != 0)
        {
            meo = Instantiate(prefabEnemiess[0]);
        }
        else
        {
            meo = Instantiate(prefabEnemiess[1]);
        }
        SetEnemyCount(meo);
        SetEnemySpeed(meo);
    }

    public void DestroyEnemiesPrefab()
    {
        Destroy(GameObject.Find("4enemies(Clone)"));
        Destroy(GameObject.Find("4enemies2(Clone)"));
    }

    public void increaseLevel()
    {
        print("increase level");
        levelNumber++;
        //if (levelNumber % 2 == 0) timeSettings();
        if (maxLevelNumber < levelNumber) { maxLevelNumber = levelNumber; difficulty.saveLevel(levelNumber); print("max = level"); }
    }

    static void SetLevelText()
    {
        string levelName = langSettings.writings[6 + langSettings.lang] + " " + levelNumber;
        levelText.text = levelName;
    }

    //public static void SetxLevelText(int level)
    //{
    //    string levelName = "Level " + level;
    //    levelText.text = levelName;
    //}

    void SetEnemyCount(GameObject mainEnemy)
    {
        for(int i=0; i<levelEnemyCount; i++)
        {
            if (i < 4) mainEnemy.transform.GetChild(i).gameObject.SetActive(true);
            else
            {
                GameObject newEnemy;
                if (levelNumber % 2 == 0) 
                {
                    newEnemy = Instantiate(level2EnemyPrefab[i % 4].gameObject);
                }
                else
                {
                    newEnemy = Instantiate(mainEnemy.transform.GetChild(i % 4).gameObject);
                }
                newEnemy.transform.parent = mainEnemy.transform;
            }
        }
    }

    void SetPowerPill()
    {
        Transform mainPill;
        if (levelNumber % 2 != 0) mainPill = mainPillObject1;
        else mainPill = mainPillObject2;

        for (int i = 0; i < levelPillCount; i++) //levelPillCount yerine childCount koydum
        {
            mainPill.GetChild(i).gameObject.SetActive(true);
        }
    }

    void SetEnemySpeed(GameObject mainEnemy)
    {
        for(int i=0;i<levelEnemyCount;i++)
        mainEnemy.transform.GetChild(i).GetComponent<NavMeshAgent>().speed = npcFollow.npcSpeed;
    }
}
