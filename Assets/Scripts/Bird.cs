using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    [Header("Bird Movement")]
    [SerializeField] private float jumpforce;
    private Rigidbody2D rbody;

    [Header("Score UI items")]
    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject StartObj;
    [SerializeField] private GameObject ReStartObj;
    [SerializeField] private GameObject ClimaxText;
    [SerializeField] private float point;
    private static float score;
    private static float finalscore;

    [Header("Audio")]
    [SerializeField] private AudioClip jumpsound;
    [SerializeField] private AudioClip hitsound;
    [SerializeField] private AudioClip pointsound;
    private new AudioSource audio;
    
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Input.backButtonLeavesApp = true;
        audio = GetComponentInParent<AudioSource>();
    }

    private void Start()
    {
        score = 0;
        GameOver.SetActive(false);
        ReStartObj.SetActive(false);
        ClimaxText.SetActive(false);
        StartObj.SetActive(true);
        finalscore = 0;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            audio.PlayOneShot(jumpsound,.5f);
            rbody.velocity = new Vector2(rbody.velocity.x,jumpforce);
        }
        if(score>=999)
        {
            gameWon();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "pipegap")
        {
            score += point;
            audio.PlayOneShot(pointsound);
        }

        if (collision.tag == "pipe" || collision.tag=="edge")
        {
            Time.timeScale = 0;
            GameOver.SetActive(true);
            finalscore = score;
            ReStartObj.SetActive(true);
            audio.PlayOneShot(hitsound);
        }
    }




    
    public void gameStart()
    {
        Time.timeScale = 1;
        StartObj.SetActive(false);
    }

    public void reStart()
    {
        SceneManager.LoadScene("GAME");
    }

    public float getScore()
    {
        return score;
    }

    public float getFinalScore()
    {
        return finalscore;
    }

    private void gameWon()
    {
        Time.timeScale = 0;
        ClimaxText.SetActive(true);
        GameOver.SetActive(true);
        finalscore = score;
        ReStartObj.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
