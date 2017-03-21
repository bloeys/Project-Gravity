using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParticlesManager : MonoBehaviour
{
    [SerializeField]
    Transform explosions;
    static List<ParticleSystem> explosionParticles = new List<ParticleSystem>();

    // Use this for initialization
    void Awake()
    {
        if (explosions)
            for (int i = 0; i < explosions.childCount; i++)
                explosionParticles.Add(explosions.GetChild(i).GetComponent<ParticleSystem>());
    }

    public static void PlaySystem(Vector3 pos, int particles = 50, float startSpd = 300)
    {
        //Use a particle system if found
        for (int i = 0; i < explosionParticles.Count; i++)
        {
            if (explosionParticles[i].isPlaying) continue;

            explosionParticles[i].transform.position = pos;

            var m = explosionParticles[i].main;
            m.startSpeed = startSpd % 251 + 250;
            explosionParticles[i].Emit(particles % 451 + 50);
            return;
        }
    }
}