using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static public GameController Instance;
    void Awake()
    {
        Instance = this;
    }

    public LevelManager[] levels;
    public GameObject[] planets;
    public Color[] skyColors;
    public GameObject[] particles;
    public int levelIndex;
    public TextMeshProUGUI keyInfoText;
    public int keyCount;
    public bool isDone;
    public Color doneColor;
    public GameObject pauseMenu;
    public TextMeshProUGUI levelInfo;

    void Start()
    {
        //test
        //PlayerPrefs.SetInt("levelIndex",0);

        levelIndex = PlayerPrefs.GetInt("levelIndex"); //baslangic leveli ayarliyor
        levelIndex %= levels.Count(); //0-3
        //gezegen ayari
        planets[levelIndex].SetActive(true);
        //gok yuzu rengi ayari
        transform.GetComponent<Camera>().backgroundColor = skyColors[levelIndex];
        //particle ayari
        particles[levelIndex].SetActive(true);
        //level ayari
        levels[levelIndex].gameObject.SetActive(true);
        keyInfoText.SetText(keyCount + " / " + levels[levelIndex].targetKeyCount);
        levelInfo.SetText("LEVEL " + (PlayerPrefs.GetInt("levelIndex")+1)); //indeksin 1 fazlasi level

        
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {   
            PauseMenu();
        }
    }


    public void AddKeyAndCheck()
    {
        keyCount++;
        keyInfoText.SetText(keyCount + " / " + levels[levelIndex].targetKeyCount);

        if(keyCount == levels[levelIndex].targetKeyCount) //hedeflenen anahtar toplandi
        {
            isDone = true;
            keyInfoText.color = doneColor;
            levels[levelIndex].lockSprite.GetComponent<SpriteRenderer>().color = doneColor;
        }
    }

    public void PauseMenu()
    {
        if(PlayerPrefs.GetInt("SoundOpen") == 1) SoundController.Instance.buttonSound.Play();
        PlayerMovement.Instance.isStop = true; // oyun durdu
        pauseMenu.SetActive(true);
    }

    public void Continue()
    {
        if(PlayerPrefs.GetInt("SoundOpen") == 1) SoundController.Instance.buttonSound.Play();
        PlayerMovement.Instance.isStop = false; //oyun devam ediyor
        pauseMenu.SetActive(false);
    }

    public void Restart()
    {
        if(PlayerPrefs.GetInt("SoundOpen") == 1) SoundController.Instance.buttonSound.Play();
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex); //yeniden yukler
    }

    public void Menu()
    {
        if(PlayerPrefs.GetInt("SoundOpen") == 1) SoundController.Instance.buttonSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //menuye don
    }



}
