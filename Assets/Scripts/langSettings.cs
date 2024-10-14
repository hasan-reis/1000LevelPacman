using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class langSettings : MonoBehaviour
{
    public GameObject bp1;
    public Transform menuButtonObject;
    Animator bp1Anim;
    public static int lang = 0; //1=Tr, 0=En
    public static string[] writings;
    static Transform canvas;
    static Text[] menuWritings;
    static Text[] GameWritings;

    private void Awake()
    {
        lang = PlayerPrefs.GetInt("Language");
        menuWritings = new Text[5];
        GameWritings = new Text[9];

        writings = new string[26];
        writings[0] = "Language";
        writings[1] = "Dil";
        writings[2] = "Play";
        writings[3] = "Başla";
        writings[4] = "Exit";
        writings[5] = "Çıkış";
        writings[6] = "Level";
        writings[7] = "Seviye";
        writings[8] = "Score";
        writings[9] = "Skor";
        writings[10] = "Game Over";
        writings[11] = "Oyun Bitti";
        writings[12] = "Restart";
        writings[13] = "Tekrarla";
        writings[14] = "You Won";
        writings[15] = "Kazandın";
        writings[16] = "Next Level";
        writings[17] = "Sıradaki Bölüm";
        writings[18] = "Resume";
        writings[19] = "Devam Et";
        writings[20] = "Main Menu";
        writings[21] = "Ana Menu";
        writings[22] = "Credits";
        writings[23] = "Yapımcılar";
        writings[24] = "2023 Digital Talent Center Student";
        writings[25] = "2023 Dijital Yetenek Merkezi Öğrencisi";
    }

    private void Start()
    {
        print("lang: " + PlayerPrefs.GetInt("Language"));
        setMenuWritings();
        changeMenuLang();
        bp1Anim = bp1.GetComponent<Animator>();
    }

    public void trButton()
    {
        lang = 1;
        PlayerPrefs.SetInt("Language", 1);
        StartCoroutine(fadeEffect());
    }

    public void enButton()
    {
        lang = 0;
        PlayerPrefs.SetInt("Language", 0);
        StartCoroutine(fadeEffect());
    }

    IEnumerator fadeEffect()
    {
        bp1.SetActive(true);
        bp1Anim.SetInteger("bp", 1);
        yield return new WaitForSeconds(1f);
        changeMenuLang();
        bp1Anim.SetInteger("bp", 0);
        yield return new WaitForSeconds(1f);
        bp1.SetActive(false);
    }

    void setMenuWritings()
    {
        menuWritings[0] = menuButtonObject.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
        menuWritings[1] = menuButtonObject.GetChild(1).GetChild(0).GetComponent<Text>();
        menuWritings[2] = menuButtonObject.GetChild(2).GetChild(0).GetComponent<Text>();
        menuWritings[3] = menuButtonObject.GetChild(3).GetChild(0).GetComponent<Text>();
        menuWritings[4] = GameObject.Find("Canvas").transform.GetChild(6).GetChild(0).GetComponent<Text>();
    }

    void changeMenuLang()
    {
        menuWritings[0].text = writings[2 + lang];
        menuWritings[1].text = writings[22 + lang];
        menuWritings[2].text = writings[4 + lang];
        menuWritings[3].text = writings[lang];
        menuWritings[4].text = writings[24 + lang];
    }

    public static void setGameWritings()
    {
        canvas = GameObject.Find("Canvas1").transform;
        GameWritings[0] = canvas.GetChild(0).GetComponent<Text>();
        GameWritings[1] = canvas.GetChild(2).GetComponent<Text>();
        GameWritings[2] = canvas.GetChild(4).GetChild(0).GetChild(0).GetComponent<Text>();
        GameWritings[3] = canvas.GetChild(4).GetChild(2).GetChild(0).GetComponent<Text>();
        GameWritings[4] = canvas.GetChild(4).GetChild(3).GetChild(0).GetComponent<Text>();
        GameWritings[5] = canvas.GetChild(5).GetChild(0).GetComponent<Text>();
        GameWritings[6] = canvas.GetChild(5).GetChild(1).GetChild(0).GetComponent<Text>();
        GameWritings[7] = canvas.GetChild(5).GetChild(2).GetChild(0).GetComponent<Text>();
        GameWritings[8] = canvas.GetChild(5).GetChild(3).GetChild(0).GetComponent<Text>();
    }

    public static void changeGameLang()
    {
        print("changeLang");
        GameWritings[0].text = writings[8 + lang];
        GameWritings[1].text = writings[6 + lang];
        GameWritings[2].text = writings[18 + lang];
        GameWritings[3].text = writings[20 + lang];
        GameWritings[4].text = writings[4 + lang];
        GameWritings[5].text = writings[10 + lang];
        GameWritings[6].text = writings[12 + lang];
        GameWritings[7].text = writings[20 + lang];
        GameWritings[8].text = writings[4 + lang];
    } 
}
