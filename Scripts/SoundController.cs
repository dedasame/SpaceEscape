using UnityEngine;

public class SoundController : MonoBehaviour
{

    static public SoundController Instance;
    void Awake()
    {
        Instance = this;
    }
    
    public AudioSource coinSound;
    public AudioSource completeSound;
    public AudioSource failSound;
    public AudioSource jumpSound;
    public AudioSource bgMusicSound;
    public AudioSource buttonSound;

    void Start()
    {
        if(PlayerPrefs.GetInt("MusicOpen") == 1) bgMusicSound.Play();
        else bgMusicSound.Stop();
    }

}
