using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuStart : MonoBehaviour
{
    public GameObject blackPanel;
    public GameObject blackPanel2;
    public GameObject blackPanel3;
    public GameObject map;
    public GameObject creditObject;
    public GameObject[] levelButtons;
    public GameObject[] backMenuObjects;
    public Animator muteAnim;
    //public static int levelSize;
    //int lsizeMinCount;
    Animator bpAnim;
    Animator bpAnim2;
    Animator bpAnim3;

    bool canclick = true;
    Animator level1Animator;
    Animator level2Animator;
    GameObject backwardBtn, forwardBtn;
    GameObject levels1Obj, levels2Obj;
    soundsManager sm;

    void Start()
    {
        //GameManager.maxLevelNumber = 1;
        muteSettings();
        bpAnim = blackPanel.GetComponent<Animator>();
        bpAnim2 = blackPanel2.GetComponent<Animator>();
        bpAnim3 = blackPanel3.GetComponent<Animator>();
        levels1Obj = map.transform.GetChild(1).gameObject;
        levels2Obj = map.transform.GetChild(2).gameObject;
        level1Animator = levels1Obj.GetComponent<Animator>();
        level2Animator = levels2Obj.GetComponent<Animator>();
        sm = GetComponent<soundsManager>();
        if (PlayerPrefs.HasKey("maxLevel")) GameManager.maxLevelNumber = PlayerPrefs.GetInt("maxLevel");
        AddButtonEvents();

        print("level:" + GameManager.levelNumber);
        print("maxlevel:" + GameManager.maxLevelNumber);

        backwardBtn = map.transform.GetChild(0).GetChild(1).gameObject;
        forwardBtn = map.transform.GetChild(0).GetChild(2).gameObject;
        StartCoroutine(closeBp());
        getButonPrefs();
        getMapPrefs();
        mapLevelNumberSettings();
    }
    
    void Update()
    {
    }

    IEnumerator closeBp()
    {
        bpAnim.SetInteger("bp", 0);
        yield return new WaitForSeconds(1f);
        blackPanel.SetActive(false);
    }

    public void Play()
    {
        sm.playSound(0);
        StartCoroutine(openMap());
    }

    public void Credits()
    {
        sm.playSound(0);
        StartCoroutine(Icredits());
    }

    public void CreditExit()
    {
        sm.playSound(0);
        StartCoroutine(IcreditsExit());
    }

    public void Exit()
    {
        Application.Quit();
    }

    static int mute=0;
    public void muteAudio()
    {
        DisableClick(muteAnim.transform.parent.gameObject);
        print("mute1:" + PlayerPrefs.GetInt("mute"));
        muteAnim.SetInteger("bp", -1);
        mute = (mute == 1 ? 0 : 1);
        if (mute==1)
        {
            muteAnim.gameObject.SetActive(true);
            muteAnim.SetInteger("bp", 1);
            AudioListener.volume = 0;
        }
        else
        {
            muteAnim.SetInteger("bp", 0);
            StartCoroutine(muteObjeFalse());
            AudioListener.volume = 1;
        }
        PlayerPrefs.SetInt("mute", mute);
    }
    IEnumerator muteObjeFalse() { yield return new WaitForSeconds(0.26f); muteAnim.gameObject.SetActive(false); }
    void muteSettings()
    {
        muteAnim.SetInteger("bp", -1);
        print("mute2:"+ PlayerPrefs.GetInt("mute") );
        if (PlayerPrefs.HasKey("mute"))
        {
            if (PlayerPrefs.GetInt("mute") == 1) { muteAnim.gameObject.SetActive(true); AudioListener.volume = 0;}
            else { muteAnim.gameObject.SetActive(false); AudioListener.volume = 1; }
        }
    }

    IEnumerator IcreditsExit()
    {
        blackPanel3.SetActive(true);
        bpAnim3.SetInteger("bp", 1);
        yield return new WaitForSeconds(1f);
        bpAnim3.SetInteger("bp", 0);
        blackPanel.SetActive(false);
        creditObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        blackPanel3.SetActive(false);
    }

    IEnumerator Icredits()
    {
        blackPanel.SetActive(true);
        bpAnim.SetInteger("bp", 1);
        yield return new WaitForSeconds(1f);
        blackPanel3.SetActive(true);
        bpAnim3.SetInteger("bp", 0);
        creditObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        blackPanel3.SetActive(false);
    }
    int x = 0; int y = 0; GameObject[] names;
    public void threeName()
    {
        x++;
        names = new GameObject[3];
        names[0] = creditObject.transform.GetChild(2).gameObject;
        names[1] = creditObject.transform.GetChild(3).gameObject;
        names[2] =  creditObject.transform.GetChild(4).gameObject;
        if (x % 3 == 0)
        {
            names[y].SetActive(false);
            y++;
            if(y==2) names[2].SetActive(true);
        }
        if (y == 3)
        {
            creditObject.transform.GetChild(0).GetComponent<Text>().text = "Hız Hilesi (Fast Run Cheat) Aktif";
            creditObject.transform.GetChild(1).GetComponent<Text>().text = "Best Regards From The Developer Mhasangecit";
            difficulty.pSpeedIncreaseAmount = 4;
            difficulty.j = 1;
        }
    }

    void AddButtonEvents()
    {
        for (int i = 0; i < 10; i++)
        {
            int buttonIndex = i;
            levelButtons[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(ClickedButton(buttonIndex)));
        }
    }

    IEnumerator ClickedButton(int buttonIndex)
    {
        sm.playSound(0);
        print("butonIndex:"+buttonIndex);
        blackPanel.SetActive(true);
        bpAnim.SetInteger("bp", 1);
        string buttonText = levelButtons[buttonIndex].transform.GetChild(0).GetComponent<Text>().text;
        print("butonText:" + buttonText);
        int level = int.Parse(buttonText);
        blackPanel2.SetActive(true);
        bpAnim2.SetInteger("bp", 1);
        yield return new WaitForSeconds(1f);
        GetComponent<difficulty>().setDifficulty(level);
        SceneManager.LoadScene(2);
        yield return new WaitForSeconds(0.4f);
        //blackPanel.SetActive(false);
        GameManager.levelText.text = "Level " + level;
    }

    IEnumerator openMap()
        {
            blackPanel.SetActive(true);
            //bpAnim.SetInteger("bp", 0);
            bpAnim.SetInteger("bp", 1);
            blackPanel2.SetActive(true);
            //bpAnim2.SetInteger("bp", 0);
            bpAnim2.SetInteger("bp", 1);
            Time.timeScale = 1f;
            yield return new WaitForSeconds(1f);
            map.SetActive(true);
            foreach (GameObject i in backMenuObjects) i.SetActive(false);
            bpAnim2.SetInteger("bp", 0);
            yield return new WaitForSeconds(1f);
            blackPanel2.SetActive(false);
            //SceneManager.LoadScene(sceneIndex);
        }

        public void backButton()
        {
             sm.playSound(0);
             StartCoroutine(IbackButton());
        }
        IEnumerator IbackButton()
        {
            blackPanel2.SetActive(true);
            bpAnim2.SetInteger("bp", 1);
            yield return new WaitForSeconds(1f);
            map.SetActive(false);
            foreach (GameObject i in backMenuObjects) i.SetActive(true);
            blackPanel2.SetActive(false);
            bpAnim.SetInteger("bp", 0);
            yield return new WaitForSeconds(1f);
            blackPanel.SetActive(false);
        }

        public void forwardButton()
        {
            if(canclick) DisableClick(forwardBtn);
            directionButton();
            forwardTextDesign();
            wardButtonsControl();
        }
        public void backwardButton()
        {
            if(canclick) DisableClick(backwardBtn);
            directionButton();
            backwardTextDesign();
            wardButtonsControl();
        }

        void forwardTextDesign()
        {
            IforwardTextDesign();
        }

        void IforwardTextDesign()
        {
            bool whileCondition = true;
            int i;
            int j;

            if (levels1Obj.transform.position.x < 0.38)
            {
                int l1 = int.Parse(levelButtons[0].transform.GetChild(0).GetComponent<Text>().text);
                int l2 = int.Parse(levelButtons[5].transform.GetChild(0).GetComponent<Text>().text);
                if (l1 < l2) return;
                i = 5;
                j = 9;
            }
            else
            {
                int l1 = int.Parse(levelButtons[0].transform.GetChild(0).GetComponent<Text>().text);
                int l2 = int.Parse(levelButtons[5].transform.GetChild(0).GetComponent<Text>().text);
                if (l2 < l1) return;
                i = 0;
                j = 4;
            }

            do
            {
                string ltext = levelButtons[i].transform.GetChild(0).GetComponent<Text>().text;
                int lvlno = int.Parse(ltext);
                lvlno += 10;
                levelButtons[i].transform.GetChild(0).GetComponent<Text>().text = lvlno.ToString();
                saveButonPrefs(i,lvlno);
                i++;
                if (i > j) whileCondition = false;
            } while (whileCondition);

        mapLevelNumberSettings();
        }

        void backwardTextDesign()
        {
            IbackwardTextDesign();
        }
        void IbackwardTextDesign()
        {
            bool whileCondition = true;
            int i;
            int j;

            if (levels1Obj.transform.position.x < 0.38) // meydanda LEVEL1
            {
                int l1 = int.Parse(levelButtons[0].transform.GetChild(0).GetComponent<Text>().text);
                int l2 = int.Parse(levelButtons[5].transform.GetChild(0).GetComponent<Text>().text);
                if (l1 > l2) return;
                i = 5;
                j = 9;
            }
            else  // meydanda LEVEL2
            {
                int l1 = int.Parse(levelButtons[0].transform.GetChild(0).GetComponent<Text>().text);
                int l2 = int.Parse(levelButtons[5].transform.GetChild(0).GetComponent<Text>().text);
                if (l2 > l1) return;
                i = 0;
                j = 4;
            }

            do
            {
                string ltext = levelButtons[i].transform.GetChild(0).GetComponent<Text>().text;
                int lvlno = int.Parse(ltext);
                lvlno -= 10;
                levelButtons[i].transform.GetChild(0).GetComponent<Text>().text = lvlno.ToString();
                saveButonPrefs(i, lvlno);
                i++;
                if (i > j) whileCondition = false;
                //yield return new WaitForSeconds(0f);
            } while (whileCondition);
            saveMapPrefs();
            mapLevelNumberSettings();
        }

       //public void levelButton()
       //{
       //    StartCoroutine(ILoadLevel());
       //}
       //IEnumerator ILoadLevel()
       //{
       //    yield return new WaitForSeconds(1f);
       //}

        void directionButton()
        {
            sm.playSound(0);
            level1Animator.SetTrigger("move");
            level2Animator.SetTrigger("move");
        }

        void DisableClick(GameObject buton)
        {
            //backwardBtn = map.transform.GetChild(0).GetChild(1).gameObject;
            //forwardBtn = map.transform.GetChild(0).GetChild(2).gameObject;
            StartCoroutine(IDisableClick(buton));
        }

        IEnumerator IDisableClick(GameObject buton)
        {
            buton.GetComponent<Button>().enabled = false;
            print("buton kapali");
            yield return new WaitForSeconds(1f);
            print("buton acik");
            buton.GetComponent<Button>().enabled = true;
        //if ((int.Parse(levelButtons[0].transform.GetChild(0).GetComponent<Text>().text) <= 1 && levels1Obj.transform.position.x < 0.38) || (int.Parse(levelButtons[9].transform.GetChild(0).GetComponent<Text>().text) < 100 && levels2Obj.transform.position.x < 0.38))
        //    StartCoroutine(IbutonOpacityControl(buton, 0.15f));
    }

        void mapLevelNumberSettings()
        {
            for (int i = 0; i < 10; i++)
            {
                string LevelButonText = levelButtons[i].transform.GetChild(0).GetComponent<Text>().text;
                int butonLevelNo = int.Parse(LevelButonText);

                Image image = levelButtons[i].GetComponent<Image>();
                Color tempColor = image.color;

                Text text = levelButtons[i].transform.GetChild(0).GetComponent<Text>();
                Color temptxtColor = text.color;

                if (GameManager.maxLevelNumber >= butonLevelNo)
                {
                    tempColor.a = 1f;
                    image.color = tempColor;
                    temptxtColor.a = 1f;
                    text.color = temptxtColor;

                    levelButtons[i].GetComponent<Button>().enabled = true;
                }
                else
                {
                    tempColor.a = 0.15f;
                    image.color = tempColor;
                    temptxtColor.a = 0.15f;
                    text.color = temptxtColor;

                    levelButtons[i].GetComponent<Button>().enabled = false;
                }
            }
        }

        void buttonOpacity(GameObject buton, float opacity)
        {
            Image image = buton.GetComponent<Image>();
            Color tempColor = image.color;

            Text text = buton.transform.GetChild(0).GetComponent<Text>();
            Color temptxtColor = text.color;

            tempColor.a = opacity;
            image.color = tempColor;
            temptxtColor.a = opacity;
            text.color = temptxtColor;

            if (opacity == 1f)
                buton.GetComponent<Button>().enabled = true;
            else
                buton.GetComponent<Button>().enabled = false;

            StartCoroutine(IbutonOpacityControl(buton, opacity));
        }

        IEnumerator IbutonOpacityControl(GameObject buton, float opacity)
        {
            //print("gecikmeli buton onleme");
            yield return new WaitForSeconds(0.8f); //bunu bir dene, olmazsan bu fonku disable fonkunun icine yaz

            if (opacity == 1f)
                buton.GetComponent<Button>().enabled = true;
            else
                buton.GetComponent<Button>().enabled = false;
        }

        void wardButtonsControl()
        {
        if (int.Parse(levelButtons[4].transform.GetChild(0).GetComponent<Text>().text) >= 100 && levels2Obj.transform.position.x < 0.38)
        {
            forwardBtn.SetActive(false);
            buttonOpacity(forwardBtn, 0.15f);
        }
        else
        {
            forwardBtn.SetActive(true);
            buttonOpacity(forwardBtn, 1f);
        }

        if (int.Parse(levelButtons[0].transform.GetChild(0).GetComponent<Text>().text) <= 1 && levels2Obj.transform.position.x < 0.38)
        {
            backwardBtn.SetActive(false);
            buttonOpacity(backwardBtn, 0.15f); 
        }
        else
        {
            backwardBtn.SetActive(true);
            buttonOpacity(backwardBtn, 1f);
        }
        saveMapPrefs();
        getMapPrefs();
    }

    void saveMapPrefs()
    {
            PlayerPrefs.SetFloat("l1ox", levels1Obj.transform.position.x);
            PlayerPrefs.SetFloat("l1oy", levels1Obj.transform.position.y);
            PlayerPrefs.SetFloat("l2ox", levels2Obj.transform.position.x);
            PlayerPrefs.SetFloat("l2oy", levels2Obj.transform.position.y); 
    }

    void saveButonPrefs(int butonIndex, int levelNo)
    {
        for(int i=0; i < 10; i++)
        {
            string key = "btn" + butonIndex;
            PlayerPrefs.SetInt(key, levelNo);
        }
    }

    void getMapPrefs()
    {
            levels1Obj.transform.position = new Vector2(PlayerPrefs.GetFloat("l1ox"), levels1Obj.transform.position.y);
            levels2Obj.transform.position = new Vector2(PlayerPrefs.GetFloat("l2ox"), levels2Obj.transform.position.y);
            print("getmapprefs");
    }

    void getButonPrefs()
    {
        for (int i = 0; i < 10; i++)
        {
            string key = "btn" + i;
            print("key:" + key);
            print("prefs:" + PlayerPrefs.GetInt(key));
            levelButtons[i].transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt(key).ToString();
        }
    }
}