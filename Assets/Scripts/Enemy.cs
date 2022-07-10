using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] int hitpoints = 1;
    [SerializeField] ParticleSystem HitVFX;
    [SerializeField] ParticleSystem DeathVFX;
    [SerializeField] Transform DeathVFXParent;
    [SerializeField] int deathScore = 10;
    [SerializeField] float explosionsSizeFactor = 1f;

    Scoring scoring;

    void Start()
    {
        scoring = FindObjectOfType<Scoring>();
    }


    void OnParticleCollision(GameObject other)
    {



        if ((--hitpoints) > 0)
        {
            ParticleSystem newParticle = Instantiate(HitVFX, transform);
            newParticle.transform.localScale *= explosionsSizeFactor;
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


