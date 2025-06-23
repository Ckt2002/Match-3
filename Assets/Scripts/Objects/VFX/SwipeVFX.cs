using System.Collections;
using UnityEngine;

public class SwipeVFX : MonoBehaviour, IVFX
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] LineRenderer lineRenderer;
    //[SerializeField] float moveSpeed = 0.02f;

    Vector3 targetPos0, targetPos1;

    private void Start()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.enabled = false;
    }

    public void SetupVFX(Vector3 fromPos, Vector3 toPos)
    {
        targetPos0 = fromPos;
        targetPos1 = toPos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetupVFX(new Vector3(-6, 0, 0), new Vector3(6, 0, 0));
            RunVFX();
        }
    }

    public void RunVFX()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        StartCoroutine(SwipeAnimation());
    }

    public IEnumerator SwipeAnimation()
    {
        particle.Play();

        lineRenderer.SetPosition(0, targetPos0);
        lineRenderer.SetPosition(1, targetPos1);

        yield return new WaitForSeconds(0.2f);

        HideVFX();
    }

    public void HideVFX()
    {
        lineRenderer.enabled = false;
    }
}
