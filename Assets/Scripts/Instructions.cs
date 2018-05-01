using UnityEngine;

public class Instructions : MonoBehaviour {

    public Animator animator;

    void Update()
    {
        // This code is so sexy.
        if (Input.GetAxis("Fire") > 0.5f
            || Input.GetAxis("FireBR1") > 0.5f
            || Input.GetAxis("FireKR1") > 0.5f
            || Input.GetAxis("FireBL1") > 0.5f
            || Input.GetAxis("FireKL1") > 0.5f

            ||

            Input.GetButton("Fire")
            || Input.GetButton("FireBR1")
            || Input.GetButton("FireKR1")
            || Input.GetButton("FireBL1")
            || Input.GetButton("FireKL1")
            )
        {
            animator.SetTrigger("Off");
        }
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

}
