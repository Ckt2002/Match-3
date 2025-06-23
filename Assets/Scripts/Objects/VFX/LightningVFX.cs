using System.Collections;
using UnityEngine;

public class LightningVFX : MonoBehaviour, IVFX
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject[] lights;
    [SerializeField] Texture2D[] lightningTextures;
    [SerializeField] float fps = 10f;

    public float endVFXTime = 2f;

    Material material;
    int currentInd = 0;
    float process = 0f;

    private void Start()
    {
        material = lineRenderer.materials[0];
    }

    public void SetupVFX(Vector3 toPos)
    {
        Debug.Log(toPos);
        lights[0].transform.position = transform.position;
        lights[1].transform.position = toPos;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    public void RunVFX()
    {
        StartCoroutine(VFX());
    }

    public IEnumerator VFX()
    {
        var particle0 = lights[0].GetComponent<ParticleSystem>();
        var particle1 = lights[1].GetComponent<ParticleSystem>();
        particle0.Play();
        particle1.Play();
        lineRenderer.SetPosition(0, lights[0].transform.position);
        lineRenderer.SetPosition(1, lights[1].transform.position);

        float delay = 1f / fps;
        while (process < endVFXTime)
        {
            process += Time.deltaTime;
            material.SetTexture("_MainTex", lightningTextures[currentInd]);
            currentInd = (currentInd + 1) % lightningTextures.Length;
            yield return new WaitForSeconds(delay);
        }
        particle0.Stop();
        particle1.Stop();
        yield return null;
        HideVFX();
    }

    public void HideVFX()
    {
        process = 0;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }
}
