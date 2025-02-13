using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    void Awake()
    {
            Instance = this;
    }


    public float forwardSpeed = 10.0f;
    public float xPos;
    public float yPos;
    public bool jump;
    public float counter;
    public float acc=1;
    public bool isStop;
    public float jumpCounter = 1;
    public float timeScale;
    public float maxJumpHeigh = 2.5f;
    Vector3 firstMousePos;
    Vector3 lastMousePos;
    Vector3 diffMousePos;
    public bool swipeLeft, swipeRight, swipeUp;

    void Start() 
    {
        
    }


    void Update()
    {
        if (isStop)
        {
            return;
        }


        if (Input.GetMouseButtonDown(0))
        {
            firstMousePos = Input.mousePosition;
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            lastMousePos= Input.mousePosition;
            diffMousePos = lastMousePos - firstMousePos;
            if (diffMousePos.sqrMagnitude > 1) //ufak bir elini bas cek yaptiginda hareket etmemesi icin
            {

                if (Mathf.Abs(diffMousePos.x) > Mathf.Abs(diffMousePos.y))
                {
                    if (diffMousePos.x < 0) swipeLeft = true;
                    if (diffMousePos.x > 0) swipeRight= true;
                }
                else
                {
                    if (diffMousePos.y > 0) swipeUp = true;
                }
            }
        }

        acc = Mathf.Clamp(Time.deltaTime * .2f + acc, 0, 4);
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, xPos, .3f), Mathf.Clamp(yPos * counter * jumpCounter, 0, maxJumpHeigh), transform.position.z + forwardSpeed * acc * Time.deltaTime);
  
        if (swipeRight || Input.GetKeyDown(KeyCode.RightArrow))
        {
            swipeRight = false;
            if (xPos == 1) { }
            else xPos += 1;
            diffMousePos = new Vector3(0, 0, 0);
        }


        if (swipeLeft || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            swipeLeft = false;
            if (xPos == -1) { }
            else xPos -= 1;
            diffMousePos = new Vector3(0, 0, 0);
        }
        
        if (swipeUp || Input.GetKeyDown(KeyCode.UpArrow))
        {
            swipeUp = false;
            if (jump == false && transform.position.y == 0) //yerde ise zipla
            {
                if(PlayerPrefs.GetInt("SoundOpen") == 1) SoundController.Instance.jumpSound.Play();
                jump = true;
                yPos = 2f; //ziplama miktari
                counter = 0;
            }
            if (jump) return;
            diffMousePos = new Vector3(0, 0, 0);
        }

        if (diffMousePos.y<0 || Input.GetKeyDown(KeyCode.DownArrow)) //asagi kayinca player -> asagi duser hemen
        { 
            jumpCounter = 5;
            var currentCounter = counter / jumpCounter;
            jump = false;
            if (counter * jumpCounter > 1) // -> 1
            {
                counter = currentCounter;
            }
            diffMousePos = new Vector3(0, 0, 0);
        }

        if (jump)
        {
            counter += Time.deltaTime * acc * 1.6f; //counter artis miktari ->> havada kalma miktari
            if (counter > 1) jump = false;
        }

        if (counter > 0 && !jump)
        {
            counter -= Time.deltaTime * acc *1f; //counter azalis miktari ->> yavas dussun
            if (counter < 0)
            {
                counter = 0;
                jumpCounter = 1.5f; // -> 1
            }
        }
    }

}

