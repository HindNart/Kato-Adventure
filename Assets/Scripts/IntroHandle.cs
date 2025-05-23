using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroHandle : MonoBehaviour
{
    [SerializeField] private Image village;
    [SerializeField] private Image cursedForest1;
    [SerializeField] private Image cursedForest2;
    [SerializeField] private Image temple;
    [SerializeField] private Image battle;
    [SerializeField] private TextMeshProUGUI villageText;
    [SerializeField] private TextMeshProUGUI cursedForest1Text;
    [SerializeField] private TextMeshProUGUI cursedForest2Text1;
    [SerializeField] private TextMeshProUGUI cursedForest2Text2;
    [SerializeField] private TextMeshProUGUI templeText;
    [SerializeField] private TextMeshProUGUI battleText;

    private void Start()
    {
        StartCoroutine(LoadIntro());
    }

    private IEnumerator LoadIntro()
    {
        // village
        if (villageText != null)
        {
            yield return new WaitForSeconds(0.5f);
            villageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(6f);
            villageText.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);
        if (village != null)
        {
            while (village.fillAmount > 0)
            {
                village.fillAmount -= Time.deltaTime / 2f;
                yield return null;
            }
        }

        // cursedForest1
        if (cursedForest1Text != null)
        {
            yield return new WaitForSeconds(0.5f);
            cursedForest1Text.gameObject.SetActive(true);
            yield return new WaitForSeconds(6f);
            cursedForest1Text.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);
        if (cursedForest1 != null)
        {
            while (cursedForest1.fillAmount > 0)
            {
                cursedForest1.fillAmount -= Time.deltaTime / 2f;
                yield return null;
            }
        }

        // cursedForest2
        if (cursedForest2Text1 != null)
        {
            yield return new WaitForSeconds(0.5f);
            cursedForest2Text1.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            cursedForest2Text1.gameObject.SetActive(false);
        }
        if (cursedForest2Text2 != null)
        {
            yield return new WaitForSeconds(0.5f);
            cursedForest2Text2.gameObject.SetActive(true);
            yield return new WaitForSeconds(6f);
            cursedForest2Text2.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);
        if (cursedForest2 != null)
        {
            while (cursedForest2.fillAmount > 0)
            {
                cursedForest2.fillAmount -= Time.deltaTime / 2f;
                yield return null;
            }
        }

        // temple
        if (templeText != null)
        {
            yield return new WaitForSeconds(0.5f);
            templeText.gameObject.SetActive(true);
            yield return new WaitForSeconds(6f);
            templeText.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);
        if (temple != null)
        {
            while (temple.fillAmount > 0)
            {
                temple.fillAmount -= Time.deltaTime / 2f;
                yield return null;
            }
        }

        // battle
        if (battleText != null)
        {
            yield return new WaitForSeconds(0.5f);
            battleText.gameObject.SetActive(true);
            yield return new WaitForSeconds(6f);
            battleText.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");
    }

    public void SkipIntro()
    {
        StopAllCoroutines();
        SceneManager.LoadScene("Menu");
    }
}
