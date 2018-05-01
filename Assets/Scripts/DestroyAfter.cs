using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {
    public float life = 1.0f;
	
	// Update is called once per frame
	void Update () {
        life -= Time.deltaTime;
        if(life <= 0)
        {
            Destroy(gameObject);
        }
	}
}
