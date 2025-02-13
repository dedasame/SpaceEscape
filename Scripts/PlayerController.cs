using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Collider ile icine girdigi nesnenin tag -> gore bir takim olaylar
    //key -> topla 
    //trap -> yeniden basla

    public GameObject completedScreen;
    public GameObject failedScreen;

    public bool isDone;
    public bool isCompleted;
    public bool shouldStop;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("key"))
        {
            if(!other.GetComponent<KeyController>().isCollected) //anahtari sadece 1 kere almak icin
            {
                //bir key topladi:
                other.GetComponent<KeyController>().isCollected = true;
                other.GetComponent<Animator>().SetTrigger("collect");
                GameController.Instance.AddKeyAndCheck();
                if(PlayerPrefs.GetInt("SoundOpen") == 1) SoundController.Instance.coinSound.Play();
            }
            
        }

        else if(other.CompareTag("gate") && !shouldStop)
        {
            //gate geldi
            PlayerMovement.Instance.isStop = true; //player hareketini durdurduk
            shouldStop = true;

            if(GameController.Instance.isDone) //Anahtar sayisi tamamlanmissa
            {
                //COMPLETED
                completedScreen.SetActive(true);
                isDone = true;
                isCompleted = true;
                if(PlayerPrefs.GetInt("SoundOpen") == 1) SoundController.Instance.completeSound.Play();
                transform.GetComponent<Animator>().SetTrigger("gate");
            }
            else //yetersiz anahtar
            {
                //FAILED
                failedScreen.SetActive(true);
                isDone = true;

                if(PlayerPrefs.GetInt("SoundOpen") == 1) SoundController.Instance.failSound.Play();
                transform.GetComponent<Animator>().SetTrigger("trap");
            }
        }

        else if(other.CompareTag("trap") && !shouldStop)
        {
            PlayerMovement.Instance.isStop = true;
            shouldStop = true;
            //FAILED
            failedScreen.SetActive(true);
            isDone = true;
            if(PlayerPrefs.GetInt("SoundOpen") == 1) SoundController.Instance.failSound.Play();
            transform.GetComponent<Animator>().SetTrigger("trap");
        }
    }

    void Update()
    {
        if(isDone)
        {
            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) 
            {
                if(isCompleted) PlayerPrefs.SetInt("levelIndex",PlayerPrefs.GetInt("levelIndex")+1); //levelIndex++
                SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex); //su anki aktif scene tekrar yukle
            }
        }
    }
    






}
