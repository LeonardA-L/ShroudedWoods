using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebVRSpawner : MonoBehaviour {

    public float cooldownMax = 0.2f;
    private float cooldown = 0.0f;
    public GameObject spawnPrefab;
    public string axisName = "FireBR1";
    public string altAxisName = "FireKR1";
    public float force = 100.0f;
    public GameObject effectPrefab;

    // Update is called once per frame
    void Update() {
        cooldown -= Time.deltaTime;
        cooldown = Mathf.Clamp(cooldown, 0.0f, 1000);

        if (cooldown <= 0.0f && (Input.GetAxis(axisName) > 0.5f || Input.GetButton(altAxisName)))
        {
            GameObject projectile = GameObject.Instantiate(spawnPrefab);
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;

            if (effectPrefab)
            {
                GameObject effect = GameObject.Instantiate(effectPrefab);
                effect.transform.position = transform.position - transform.forward + new Vector3(0, 0, 0);
            }

            cooldown = cooldownMax;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward.normalized * force);
        }
    }
}
