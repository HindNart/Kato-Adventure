using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip menuMusic;
    public AudioClip forestTheme;
    public AudioClip caveTheme;
    public AudioClip swampTheme;
    public AudioClip templeTheme;

    public AudioClip playerWalk;
    public AudioClip playerAttack;
    public AudioClip playerHurt;
    public AudioClip playerDie;
    public AudioClip win;
    public AudioClip coin;
    public AudioClip orb;
    public AudioClip jump;
    public AudioClip meat;
    public AudioClip apple;
    public AudioClip gem;
    public AudioClip healthPotion;
    public AudioClip powerPotion;
    public AudioClip chestOpen;
    public AudioClip doorOpen;

    public AudioClip bombExplosion;
    public AudioClip bossBattleTheme;
    public AudioClip bossWalk;
    public AudioClip bossAttack;
    public AudioClip bossHurt;
    public AudioClip bossDie;

    // [SerializeField] private Button btnSound;
    private bool isMusicPause = false;
    private bool isSfxPause = false;

    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // private void FixedUpdate()
    // {
    //     if (PlayerPrefs.HasKey("music") && PlayerPrefs.HasKey("sfx"))
    //     {
    //         isMusicPause = PlayerPrefs.GetInt("music") == 1;
    //         isSfxPause = PlayerPrefs.GetInt("sfx") == 1;
    //         if (btnSound != null)
    //         {
    //             if (isMusicPause && isSfxPause)
    //             {
    //                 btnSound.image.sprite = soundOffSprite;
    //             }
    //             else
    //             {
    //                 btnSound.image.sprite = soundOnSprite;
    //             }
    //         }
    //     }
    // }

    #region Music Control
    public void PlayMenuMusic()
    {
        musicSource.clip = menuMusic;
        musicSource.Play();
    }

    public void PlayForestMusic()
    {
        musicSource.clip = forestTheme;
        musicSource.Play();
    }

    public void PlayCaveMusic()
    {
        musicSource.clip = caveTheme;
        musicSource.Play();
    }

    public void PlaySwampMusic()
    {
        musicSource.clip = swampTheme;
        musicSource.Play();
    }

    public void PlayTempleMusic()
    {
        musicSource.clip = templeTheme;
        musicSource.Play();
    }
    #endregion

    #region Player Sound Control
    public void PlayPlayerWalkSound()
    {
        sfxSource.PlayOneShot(playerWalk);
    }

    public void PlayPlayerAttackSound()
    {
        sfxSource.PlayOneShot(playerAttack);
    }

    public void PlayPlayerHurtSound()
    {
        sfxSource.PlayOneShot(playerHurt);
    }

    public void PlayPlayerDieSound()
    {
        sfxSource.PlayOneShot(playerDie);
    }

    public void PlayWinSound()
    {
        sfxSource.PlayOneShot(win);
    }

    public void PlayCoinSound()
    {
        sfxSource.PlayOneShot(coin);
    }

    public void PlayOrbSound()
    {
        sfxSource.PlayOneShot(orb);
    }

    public void PlayJumpSound()
    {
        sfxSource.PlayOneShot(jump);
    }

    public void PlayMeatSound()
    {
        sfxSource.PlayOneShot(meat);
    }

    public void PlayAppleSound()
    {
        sfxSource.PlayOneShot(apple);
    }

    public void PlayGemSound()
    {
        sfxSource.PlayOneShot(gem);
    }

    public void PlayHealthPotionSound()
    {
        sfxSource.PlayOneShot(healthPotion);
    }

    public void PlayPowerPotionSound()
    {
        sfxSource.PlayOneShot(powerPotion);
    }

    public void PlayChestOpenSound()
    {
        sfxSource.PlayOneShot(chestOpen);
    }

    public void PlayDoorOpenSound()
    {
        sfxSource.PlayOneShot(doorOpen);
    }

    #endregion

    #region Enemy Sound Control
    public void PlayBombExplosionSound()
    {
        sfxSource.PlayOneShot(bombExplosion);
    }

    public void PlayBossBattleTheme()
    {
        musicSource.clip = bossBattleTheme;
        musicSource.Play();
    }

    public void PlayBossWalkSound()
    {
        sfxSource.PlayOneShot(bossWalk);
    }

    public void PlayBossAttackSound()
    {
        sfxSource.PlayOneShot(bossAttack);
    }

    public void PlayBossHurtSound()
    {
        sfxSource.PlayOneShot(bossHurt);
    }

    public void PlayBossDieSound()
    {
        sfxSource.PlayOneShot(bossDie);
    }
    #endregion

    #region Volume Control
    public void ToggleVolume(Button btnSound)
    {
        // btnSound = btn;
        isMusicPause = !isMusicPause;
        isSfxPause = !isSfxPause;

        if (isMusicPause && isSfxPause)
        {
            btnSound.image.sprite = soundOffSprite;
            musicSource.mute = true;
            sfxSource.mute = true;
        }
        else
        {
            btnSound.image.sprite = soundOnSprite;
            musicSource.mute = false;
            sfxSource.mute = false;
        }

        PlayerPrefs.SetInt("music", isMusicPause ? 1 : 0);
        PlayerPrefs.SetInt("sfx", isSfxPause ? 1 : 0);
    }
    #endregion
}
