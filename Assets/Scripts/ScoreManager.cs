using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("Scoresheet")]
    [SerializeField] private GameObject Oneth;
    [SerializeField] private GameObject Tenth;
    [SerializeField] private GameObject Hundredth;

    [Header("Final Scoresheet")]
    [SerializeField] private GameObject FOneth;
    [SerializeField] private GameObject FTenth;
    [SerializeField] private GameObject FHundredth;

    [Header("Final Scoresheet")]
    [SerializeField] private GameObject HOneth;
    [SerializeField] private GameObject HTenth;
    [SerializeField] private GameObject HHundredth;

    [Header("Sprite Numbers")]
    [SerializeField] private Sprite[] numbers;

    private float score;
    private int[] intList = { 0, 0, 0, 0, 0 };
    private char[] charList;

    private Bird bird;

    private static float highscore = 0;

    private void Awake()
    {
        bird = GetComponentInParent<Bird>();
        FOneth.SetActive(false);
        FTenth.SetActive(false);
        FHundredth.SetActive(false);

        highscore = PlayerPrefs.GetInt("highscore", (int)highscore);
        score = highscore;


        charList = (highscore.ToString().PadLeft(3, '0')).ToCharArray();
        for (int i = charList.Length - 1; i >= 0; i--)
        {
            intList[i] = (int)char.GetNumericValue(charList[i]);
        };
        ShowHighScore(true, true, true, intList[2], intList[1], intList[0]);
    }


    private void Update()
    {
        score = bird.getScore();
        charList = (score.ToString().PadLeft(3,'0')).ToCharArray();
        for (int i = charList.Length-1; i >= 0 ; i--)
        {
            intList[i] = (int)char.GetNumericValue(charList[i]);
        };

        Oneth.GetComponent<SpriteRenderer>().sprite = numbers[intList[2]];
        Tenth.GetComponent<SpriteRenderer>().sprite = numbers[intList[1]];
        Hundredth.GetComponent<SpriteRenderer>().sprite = numbers[intList[0]];

        if(bird.getFinalScore()>0)
        {
            ShowFinalScore(true,true,true, intList[2], intList[1], intList[0]);

            //saving highscore
            if (score > highscore)
            {
                highscore = score;
                PlayerPrefs.SetInt("highscore", (int)highscore);
            }
        }

    }

    
    public void ShowFinalScore(bool _a,bool _b,bool _c,int _p,int _q,int _r)
    {
        FOneth.SetActive(_a);
        if (_a) FOneth.GetComponent<SpriteRenderer>().sprite = numbers[_p];
        FTenth.SetActive(_b);
        if (_b) FTenth.GetComponent<SpriteRenderer>().sprite = numbers[_q];
        FHundredth.SetActive(_c);
        if (_c) FHundredth.GetComponent<SpriteRenderer>().sprite = numbers[_r];
    }

    public void ShowHighScore(bool _a, bool _b, bool _c, int _p, int _q, int _r)
    {
        HOneth.SetActive(_a);
        if (_a) HOneth.GetComponent<SpriteRenderer>().sprite = numbers[_p];
        HTenth.SetActive(_b);
        if (_b) HTenth.GetComponent<SpriteRenderer>().sprite = numbers[_q];
        HHundredth.SetActive(_c);
        if (_c) HHundredth.GetComponent<SpriteRenderer>().sprite = numbers[_r];
    }

}
