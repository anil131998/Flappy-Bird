using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeMove : MonoBehaviour
{
    [Header("Pipe Movement")]
    [SerializeField] private float moveSpeed;
    private float waittimer = 3; //game will wait x seconds before starting
    private float waitclock = 0;

    [Header("Camera Object for reference")]
    [SerializeField] private Camera cam;

    [Header("Vertical Positioning")]
    [SerializeField] private float maxy;
    [SerializeField] private float miny;
    private float ypos;

    [Header("CountDown")]
    [SerializeField] private GameObject CountDownTimer;
    [SerializeField] Sprite[] CountDownImages;

    private void Start()
    {
        CountDownTimer.SetActive(false);
    }

    private void Update()
    {
        waitclock += Time.deltaTime;
        if (waitclock > waittimer)
        {
            if (waitclock < waittimer+1)
                CountDownTimer.SetActive(false);
            
            
            transform.position += transform.right * -moveSpeed * Time.deltaTime;

            if ((cam.WorldToScreenPoint(transform.position).x) <= 0)
            {
                ypos = Random.Range(miny, maxy);
                transform.position = new Vector3(10f, ypos, transform.position.z);
            }
        }
        else
        {
            //countdown
            CountDownTimer.SetActive(true);
            CountDownTimer.GetComponent<SpriteRenderer>().sprite = CountDownImages[(int)waitclock];
        }
    }

}
