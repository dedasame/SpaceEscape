using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public AudioSource bgMusic;
    public AudioSource buttonSound;
    public GameObject howToPlay;
    public TextMeshProUGUI levelText;

    public void PlayGame()
    {
        if(PlayerPrefs.GetInt("SoundOpen") == 1) buttonSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        if(PlayerPrefs.GetInt("SoundOpen") == 1) buttonSound.Play();
        Application.Quit();
    }

    public void MusicButton()
    {
        if(PlayerPrefs.GetInt("SoundOpen") == 1) buttonSound.Play();
        
        if(PlayerPrefs.GetInt("MusicOpen")==1)
        {
            PlayerPrefs.SetInt("MusicOpen",0);
            bgMusic.Stop();
        }
        else 
        {
            PlayerPrefs.SetInt("MusicOpen",1);
            bgMusic.Play();
        }
    }

    public void SoundButton()
    {
        if(PlayerPrefs.GetInt("SoundOpen") == 1) buttonSound.Play();
        if(PlayerPrefs.GetInt("SoundOpen")==1) PlayerPrefs.SetInt("SoundOpen",0);
        else PlayerPrefs.SetInt("SoundOpen",1);
    }

    public void HowToPlayButton()
    {
        if(PlayerPrefs.GetInt("SoundOpen") == 1) buttonSound.Play();
        howToPlay.SetActive(true);
    }
    public void CloseButton()
    {
        if(PlayerPrefs.GetInt("SoundOpen") == 1) buttonSound.Play();    
        howToPlay.SetActive(false);
    }

    void Start()
    {
        //PlayerPrefs.SetInt("levelIndex",0);

        //PlayerPrefs -> Default = 1
        if (!PlayerPrefs.HasKey("MusicOpen"))
        {
            PlayerPrefs.SetInt("MusicOpen", 1);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("SoundOpen"))
        {
            PlayerPrefs.SetInt("SoundOpen", 1);
            PlayerPrefs.Save();
        }
        

        if(PlayerPrefs.GetInt("MusicOpen") == 1) bgMusic.Play();
        else bgMusic.Stop();

        levelText.SetText("LEVEL " + (PlayerPrefs.GetInt("levelIndex")+1)); 
    }



}
