using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private static ScoreManager instance;
    public int score = 0;
    public List<Animator> scoreBars = new List<Animator>();
    public Text finalTimeText;
    public Animator finalTimeAnimator;
    public float time = 0;
    private bool done = false;
    public AudioSource boardSound;
    public AudioSource hitSound;
    public AudioSource victorySound;
    public static ScoreManager Instance
    {
        get
        {
            return instance;
        }
    }

	// Use this for initialization
	void Start () {
        instance = this;
    }

    private void Update()
    {
        if(!done)
        {
            time += Time.deltaTime;
        }
    }

    public void Score()
    {
        scoreBars[score].SetTrigger("Touch");

        boardSound.Play();
        hitSound.Play();

        score++;

        if(score == scoreBars.Count)
        {
            done = true;
            finalTimeText.text = "Final Time: " + time.ToString("0.00") + "s";
            finalTimeAnimator.SetTrigger("End");
            victorySound.Play();
        }
    }
}
