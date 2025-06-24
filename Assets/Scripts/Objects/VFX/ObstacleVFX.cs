using UnityEngine;

public class ObstacleVFX : MonoBehaviour, IVFX
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void HideVFX()
    {
    }

    public void RunVFX()
    {
        particle.Play();
    }
}
