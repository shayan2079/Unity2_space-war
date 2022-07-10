using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem HitVFX;
    [SerializeField] ParticleSystem DeathVFX;

    [SerializeField] Transform DeathVFXParent;

    [SerializeField] int deathScore = 10;
    [SerializeField] float explosionsSizeFactor = 1f;
    [SerializeField] int hitpoints = 1;


    List<ParticleCollisionEvent> collisionEvents;

    Scoring scoring;

    void Start()
    {
        scoring = FindObjectOfType<Scoring>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }


    void OnParticleCollision(GameObject other)
    {
        other.GetComponent<ParticleSystem>().GetCollisionEvents(this.gameObject, collisionEvents);

        foreach (var collision in collisionEvents)
        {
            if ((--hitpoints) > 0)
            {
                ParticleSystem newParticle = Instantiate(HitVFX, collision.intersection, Quaternion.identity);
                newParticle.transform.SetParent(DeathVFXParent);
            }
            else if (hitpoints == 0)
            {
                ParticleSystem newParticle = Instantiate(DeathVFX, transform.position, Quaternion.identity);
                newParticle.transform.localScale *= explosionsSizeFactor;
                newParticle.transform.SetParent(DeathVFXParent);
                scoring.UpdateScore(deathScore);
                Destroy(this.gameObject);
            }
        }
    }
}


