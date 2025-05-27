using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public enum Type
    {
        Coin,
        Orb,
        Apple,
        Meat,
        HealthPotion,
        AntidotePotion,
        ForestGem,
        CaveGem,
        SwampGem,
    }

    public Type type;
    private Animator anim;
    [SerializeField] private Sprite gemSprite;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                AudioManager.Instance.PlayCoinSound();
                anim.SetTrigger("collect");
                GameManager.Instance.AddCoin();
                Destroy(gameObject, 0.5f);
                break;
            case Type.Orb:
                AudioManager.Instance.PlayOrbSound();
                anim.SetTrigger("collect");
                GameManager.Instance.AddOrb();
                Destroy(gameObject, 0.5f);
                break;
            case Type.Apple:
                AudioManager.Instance.PlayAppleSound();
                player.GetComponent<Player>().Heal(1);
                Destroy(gameObject);
                break;
            case Type.Meat:
                AudioManager.Instance.PlayMeatSound();
                player.GetComponent<Player>().Heal(3);
                Destroy(gameObject);
                break;
            case Type.HealthPotion:
                GameManager.Instance.AddHealthPotion();
                Destroy(gameObject);
                break;
            case Type.AntidotePotion:
                GameManager.Instance.AddPowerPotion();
                Destroy(gameObject);
                break;
            case Type.ForestGem:
                AudioManager.Instance.PlayGemSound();
                player.GetComponent<Player>().CollectGem(0);
                Destroy(gameObject);
                break;
            case Type.CaveGem:
                AudioManager.Instance.PlayGemSound();
                player.GetComponent<Player>().CollectGem(1);
                Destroy(gameObject);
                break;
            case Type.SwampGem:
                AudioManager.Instance.PlayGemSound();
                player.GetComponent<Player>().CollectGem(2);
                Destroy(gameObject);
                break;
        }
    }
}
