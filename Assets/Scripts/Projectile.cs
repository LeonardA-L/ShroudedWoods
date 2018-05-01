using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float lifeTime = 5.0f;
    public List<Texture2D> splashTextures;

    private Vector3 lastPosition;
	
	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
        lastPosition = transform.position;
    }
    
    void OnCollisionEnter(Collision collision)
    {

        RaycastHit hit = new RaycastHit();

        Vector3 lastPoint = lastPosition;
        Vector3 newPoint = collision.contacts[0].point;

        Ray ray = new Ray(lastPoint, (newPoint - lastPoint).normalized);

        if (Physics.Raycast(ray, out hit, 5.0f, ~(1 << 8)))
        {
            // Has hit a paintable object
            RevealedElement script = collision.collider.gameObject.GetComponent<RevealedElement>();
            if (null != script)
            {
                script.PaintOn(hit.textureCoord, splashTextures[(int)Mathf.Floor(Random.value * splashTextures.Count)]);
            }

            // Has hit a target
            RobinHoodTarget target = collision.collider.gameObject.GetComponent<RobinHoodTarget>();
            if (null != target)
            {
                target.Touch();
            }
        }

        // Destroy anyway
        Destroy(this.gameObject);
    }
}
