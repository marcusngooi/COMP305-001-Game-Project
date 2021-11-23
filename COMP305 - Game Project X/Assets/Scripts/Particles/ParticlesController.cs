using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    public ParticleSystem deathParticles;
    public ParticleSystem playerDustParticles;
    public void CreateDust()
    {
        playerDustParticles.Play();
    }

    public void DeathEffect()
    {
        deathParticles.Play();
    }

}
