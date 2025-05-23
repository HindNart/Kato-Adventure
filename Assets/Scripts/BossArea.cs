using System;
using UnityEngine;

public class BossArea : MonoBehaviour
{
    [SerializeField] private GameObject stone;
    [SerializeField] private GameObject bossHpBar;
    [SerializeField] private GameObject bossInfor;

    private void Start()
    {
        bossHpBar.SetActive(false);
        bossInfor.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance.playerDead)
        {
            if (stone != null)
            {
                stone.SetActive(false);
            }
            bossHpBar.SetActive(false);
            bossInfor.SetActive(false);
            GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossHpBar.SetActive(true);
            bossInfor.SetActive(true);
            if (stone != null)
            {
                stone.SetActive(true);
            }
            GetComponent<Collider2D>().enabled = false;
            Invoke(nameof(HideBossInfo), 2f);

            AudioManager.Instance.PlayBossBattleTheme();
        }
    }

    private void HideBossInfo()
    {
        bossInfor.SetActive(false);
    }
}
