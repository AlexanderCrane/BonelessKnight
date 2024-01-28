using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Timeline;

public class Explode : MonoBehaviour
{
    [SerializeField] float explosionForce = 10;
    [SerializeField] float explosionRadius = 10;
    Collider[] colliders = new Collider[20];
    public float explosionTimer = 3f;
    public float destroyTimer = 3.1f;
    public GameObject Explosion;
    void Update()
    {
        explosionTimer -= Time.deltaTime;
        destroyTimer -= Time.deltaTime;

        if (explosionTimer <= 0.0f)
        {
            
            ExplodeNonAlloc();
            if (destroyTimer <= 0.0f)
            {
                Destroy(gameObject);
            }
                
        }
    }
    void ExplodeNonAlloc()
    {
        GameObject.Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, colliders);
        if (numColliders > 0)
        {
            for (int i = 0; i < numColliders; i++)
            {
                if (colliders[i].TryGetComponent(out Rigidbody rb))
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius,0.75f);
                }
            }
        }
       
    }
}
