using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject blackPanel;
    public GameObject gameOverPanel;
    public GameObject menuButon;
    public GameObject player;
    public GameObject GameManagerObj;
    public GameObject[] buttons;

    GameManager gameManager;
    Text gameOverText;
    Animator panelAnim;
    Animator blackPanelAnim;
    soundsManager sm;
    bool win = false;


    void Start()
    {
        gameManager = GameManagerObj.GetComponent<GameManager>();
        player = GameObject.Find("pacman_0");
        panelAnim = pausePanel.GetComponent<Animator>();
        blackPanelAnim = blackPanel.GetComponent<Animator>();
        gameOverText = gameOverPanel.transform.GetChild(0).GetComponent<Text>();
        sm = GameManagerObj.GetComponent<soundsManager>();
        gameOverText.text = "";
        blackPanelAnim.SetBool("blackPanel",false);
        StartCoroutine(removeBp());
    }

    void Update()
    {
        
    }

    void menuButton()
    {
        DisableClick(buttons[5]);
        sm.playSound(0);
        panelAnim.SetTrigger("panel");
        print("1.");
        player.GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(menuButtonSettings());
    }

    IEnumerator menuButtonSettings()
    {
        player.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.35f);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        DisableClick(buttons[0]);
        sm.playSound(0);
        Time.timeScale = 1;
        panelAnim.SetTrigger("panel");
       // print("3."); //ilk StartCoroutine(TimeScale(1)); yazdim ama bu kod niye calismadi, anlamadim
        //player.GetComponent<CircleCollider2D>().enabled = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void turnMainMenu()
    {
        DisableClick(buttons[1],buttons[2]);
        sm.playSound(0);
        StartCoroutine(openBp(1));
    }

    public void Restart() //Restart-NextLevel Butonu
    {
        DisableClick(buttons[3], buttons[4]);
        sm.playSound(0);
        food.score = 0;
        food.eatenFood = 0;

        if (win)
        {
            Time.timeScale = 1;
            gameManager.nextLevel();
        }
        else
        {
            //Debug.LogWarning("Lost");
            Time.timeScale = 1;
            gameManager.restart();
        }
        win = false;
    }

    public void GameOver()
    {
        if(menuButon!=null)
            menuButon.SetActive(false);
        player.GetComponent<Animator>().SetBool("death", true);
        pacmanControl.speed=0;
        player.GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(gameOver());

        if(food.score!=0)
            gameOverText.text = langSettings.writings[10 + langSettings.lang] +"\n\n"+ langSettings.writings[8 + langSettings.lang] + ":"+food.score;
        else gameOverText.text = langSettings.writings[10 + langSettings.lang];
    }

    IEnumerator gameOver()
    {
        gameOverPanel.GetComponent<Animator>().SetBool("panel", true);
        yield return new WaitForSeconds(1.50f);
        //for (int i = 0; i < disabledEnemy.enemies.Count; i++) disabledEnemy.enemies[i].GetComponent<BoxCollider2D>().isTrigger = false;
        //Time.timeScale = 0f;
       // print("0dan sonra");
        //for (int i = 0; i < disabledEnemy.enemies.Count; i++) disabledEnemy.enemies[i].GetComponent<BoxCollider2D>().isTrigger = true;
    }

    IEnumerator removeBp()
    {
        yield return new WaitForSeconds(1f);
        blackPanel.SetActive(false);
    }

    IEnumerator openBp(int sceneIndex)
    {
        blackPanel.SetActive(true);
        Time.timeScale = 1;
        blackPanelAnim.SetInteger("bp", 1);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }

    public void Win()
    {
        win = true;
        sm.playSoundWithStop(6);
        menuButon.SetActive(false);
        pacmanControl.speed=0;
        player.GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(gameWin());

        gameOverText.text = langSettings.writings[14 + langSettings.lang];
        if (GameManager.levelNumber == 100) {
            gameOverText.text = "";
            sm.playSoundWithStop(7);
            if (langSettings.lang==0)
            gameOverText.text += "\nCongratulations, You Have Finished the Game";
            else
                gameOverText.text += "\nTebrikler, Oyunu Bitirdiniz.";
        }
        food.score = 0;
        gameOverPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = langSettings.writings[16 + langSettings.lang];

        if (GameManager.levelNumber >= 100)
        {
            buttons[4].SetActive(false);
        }
        else
        {
            buttons[4].SetActive(true);
        }

        gameManager.increaseLevel();
        GameManagerObj.GetComponent<difficulty>().setDifficulty(GameManager.levelNumber);
    }

    IEnumerator gameWin()
    {
        //GetComponent<disabledEnemy>().enemiesPassive();
        //yield return new WaitForSeconds(1.50f);
        gameOverPanel.GetComponent<Animator>().SetBool("panel",true);
        yield return new WaitForSeconds(1.1f);
        Time.timeScale = 0f;
    }

    void DisableClick(GameObject buton)
    {
        print("disableClick1");
        StartCoroutine(IDisableClick(buton));
    }

    IEnumerator IDisableClick(GameObject buton)
    {
        buton.GetComponent<Button>().enabled = false;
        yield return new WaitForSeconds(1f);
        buton.GetComponent<Button>().enabled = true;
    }

    void DisableClick(GameObject buton, GameObject buton2)
    {
        StartCoroutine(IDisableClick(buton,buton2));
    }

    IEnumerator IDisableClick(GameObject buton, GameObject buton2)
    {
        buton.GetComponent<Button>().enabled = false;
        buton2.GetComponent<Button>().enabled = false;
        yield return new WaitForSeconds(1f);
        buton.GetComponent<Button>().enabled = true;
        buton2.GetComponent<Button>().enabled = true;
    }

    public void restartText()
    {
        buttons[4].SetActive(true);
        gameOverPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = langSettings.writings[12 + langSettings.lang];
    } 
}