using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobinHoodTarget : MonoBehaviour {

    private Animator animator;
    public GameObject touchEffect;
    private bool touched = false;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
    }
	
    public void Touch()
    {
        if (touched)
            return;
        touched = true;

        if (animator != null)
        {
            //animator.SetTrigger("Touch");
            animator.speed = 0;
        }

        if(touchEffect != null)
        {
            GameObject effect = GameObject.Instantiate(touchEffect, transform);
            effect.transform.localPosition = new Vector3(-0.03f, -0.13f, -0.31f);
        }

        ScoreManager.Instance.Score();
    }
}
