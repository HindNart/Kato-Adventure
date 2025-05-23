using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Animator anim;

    [Header("Spawn")]
    public Vector3 beginSpawnPosition;
    public Transform spawnPosition;
    public Transform checkPointPosition;
    public PolygonCollider2D mapBoundary;

    [Header("Hp")]
    private readonly float maxHp = 100f;
    public float currentHp { get; set; }
    [SerializeField] private Image hpBar;

    [Header("Attack")]
    [SerializeField] private float damage = 1f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius = 0.5f;

    private void Start()
    {
        currentHp = maxHp;
        UpdateHpBar();
        anim = GetComponent<Animator>();
        beginSpawnPosition = new(PlayerPrefs.GetFloat("tagetDoorX", 0), PlayerPrefs.GetFloat("tagetDoorY", 0), PlayerPrefs.GetFloat("tagetDoorZ", 0));
    }

    public void TakeDamage(float damage)
    {
        AudioManager.Instance.PlayPlayerHurtSound();
        anim.SetTrigger("hit");

        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        PlayerPrefs.SetFloat("PlayerHp", currentHp);

        UpdateHpBar();

        if (currentHp == 0)
        {
            Die();
        }
    }

    private void UpdateHpBar()
    {
        if (hpBar != null)
        {
            // hpBar.fillAmount = currentHp / maxHp;
            hpBar.fillAmount = PlayerPrefs.GetFloat("PlayerHp", currentHp) / maxHp;
        }
    }

    public void Die()
    {
        GameManager.Instance.playerDead = true;
        gameObject.layer = LayerMask.NameToLayer("Default");
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        AudioManager.Instance.PlayPlayerDieSound();
        anim.SetTrigger("death");
        GameManager.Instance.LoseLife();
    }

    public void Respawn()
    {
        GameManager.Instance.playerDead = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
        GetComponent<PlayerController>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        currentHp = maxHp;
        PlayerPrefs.SetFloat("PlayerHp", currentHp);
        UpdateHpBar();

        Vector3 respawnPosition = beginSpawnPosition;
        if (checkPointPosition != null)
        {
            respawnPosition = checkPointPosition.position;
        }
        else if (spawnPosition != null)
        {
            respawnPosition = spawnPosition.position;
        }
        transform.position = respawnPosition + Vector3.right;
    }

    public void Attack()
    {
        AudioManager.Instance.PlayPlayerAttackSound();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, radius, LayerMask.GetMask("Enemy"));
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit enemy");
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
        Collider2D[] hitEnemiesFly = Physics2D.OverlapCircleAll(attackPoint.position, radius, LayerMask.GetMask("EnemyFly"));
        foreach (Collider2D enemy in hitEnemiesFly)
        {
            Debug.Log("Hit enemy");
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

    public void UsePotion(string potionType)
    {
        switch (potionType.ToLower())
        {
            case "health":
                if (GameManager.Instance.healthPotionQuantity > 0)
                {
                    GameManager.Instance.healthPotionQuantity--;
                    AudioManager.Instance.PlayHealthPotionSound();
                    Debug.Log("Health.");
                    Heal(10);
                }
                else
                {
                    Debug.Log("No health potions available.");
                }
                break;

            case "power":
                if (GameManager.Instance.powerPotionQuantity > 0)
                {
                    GameManager.Instance.powerPotionQuantity--;
                    AudioManager.Instance.PlayPowerPotionSound();
                    DoubleDamage();
                }
                else
                {
                    Debug.Log("No power potions available.");
                }
                break;
            default:
                Debug.Log("Invalid potion type.");
                break;
        }
    }

    public void Heal(int hp)
    {
        currentHp += hp;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        UpdateHpBar();
    }

    public void DoubleDamage()
    {
        StartCoroutine(DoubleDamageForDuration(10f)); // Double damage for 10 seconds
    }

    private IEnumerator DoubleDamageForDuration(float duration)
    {
        GameManager.Instance.doubleDmgIcon.GetComponent<Image>().enabled = true;
        float originalDamage = damage;
        damage *= 2; // Double the damage
        yield return new WaitForSeconds(duration);
        damage = originalDamage; // Revert to original damage
        GameManager.Instance.doubleDmgIcon.GetComponent<Image>().enabled = false;
    }

    public void CollectGem(int gemType)
    {
        if (gemType >= 0 && gemType < GameManager.Instance.gemIcons.Length)
        {
            GameManager.Instance.gemIcons[gemType].enabled = true; // Enable the corresponding gem icon
            GameManager.Instance.collectedGems++;
            Debug.Log($"Collected a gem of type {gemType}! Total gems: {GameManager.Instance.collectedGems}");
        }
        else
        {
            Debug.Log("Invalid gem type.");
        }
    }

    public bool PlaceGemOnPedestal()
    {
        if (GameManager.Instance.collectedGems > 0)
        {
            GameManager.Instance.collectedGems--;
            Debug.Log($"Placed a gem on the pedestal. Remaining gems: {GameManager.Instance.collectedGems}");
            return true; // Successfully placed a gem
        }
        else
        {
            Debug.Log("No gems to place on the pedestal.");
            return false; // No gems to place
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
        {
            mapBoundary = other.GetComponent<PolygonCollider2D>();
        }
    }
}