using System.Collections;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Collider2D triggerCollider = GetComponent<Collider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPos = transform.localPosition;
        Vector3 endPos = transform.localPosition + Vector3.up;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.position = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = endPos;
        triggerCollider.enabled = true;
    }
}
