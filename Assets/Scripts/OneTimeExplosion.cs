using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeExplosion : MonoBehaviour
{

    [SerializeField] private ParticleSystem particle = default;
    private void Awake()
    {
        particle.Play();
        Destroy(gameObject, particle.main.duration);
    }
}
