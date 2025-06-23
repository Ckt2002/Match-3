using System.Collections;
using UnityEngine;

public class PotionDestroyVFX : MonoBehaviour, IVFX
{
    [SerializeField] ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

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
        yield return null;
    }
}
