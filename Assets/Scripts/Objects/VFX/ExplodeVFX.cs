using System.Collections;
using UnityEngine;

public class ExplodeVFX : MonoBehaviour, IVFX
{
    [SerializeField] ParticleSystem particle;

    public void HideVFX()
    {
        particle.Stop();
        gameObject.SetActive(false);
    }

    public void RunVFX()
    {
        StartCoroutine(VFX());
    }

    public IEnumerator VFX()
    {
        particle.Play();
        while (particle.isPlaying)
        {
            yield return new WaitForSeconds(0.02f);
        }
    }
}