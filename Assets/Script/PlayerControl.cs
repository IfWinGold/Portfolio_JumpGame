using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour //, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public static PlayerControl Instance = null;

    Renderer playercolor;
    SmokeWall smokewall;
    public float valuepower;
    float ArrowSpeed = 0.2f;
    public bool isItem = false;
    Vector2 BasePoint;
    Vector2 value;
    Rigidbody rigidbody;
    AudioSource[] bgm;
    Vector3 targetposition;
    Vector2 v_player;
    float f_Gauge = 0f;
    public bool isjump = false;
    bool jumpsound = false;
    int Layer;
    [SerializeField] GameObject ArrowPosition;    
    [SerializeField] GameObject DragPower;
    [SerializeField] GameObject target;
    [SerializeField] Slider Gauge;
    [SerializeField] GameObject player;
    [SerializeField] GameObject rayPosition;
    [SerializeField] GameObject rayposition_2;
    [SerializeField] GameObject rayposition_3;
    [SerializeField] GameObject g_GAMEOVER;
    [SerializeField] Text text_Velocity;
    bool isGameOver = false;
    public float view_y = 0f;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Time.timeScale = 1f;
        Layer = (1 << LayerMask.NameToLayer("Player"));
        isjump = false;
        rigidbody = player.GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(0, 0, 0);
        bgm = GetComponents<AudioSource>();
        playercolor = GetComponent<Renderer>();        
    }

    // Update is called once per frame
    void Update()
    {    
        
        if(isGameOver != true)
        {
            text_Velocity.text = "Velocity : " + rigidbody.velocity;
            Vector3 view = Camera.main.WorldToScreenPoint(transform.position);
            view_y = view.y;
            //게임오버
            if (view_y < 0f)
            {
                GAMEOVER();
            }

            if (rigidbody.velocity == new Vector3(0, 0, 0))
            {
                isjump = false;
                jumpsound = false;
                GameManager.Instance.isBox = false;
            }
            else
            {
                isjump = true;
            }
            if (isjump == true && jumpsound == false)
            {
                jumpsound = true;
                if (rigidbody.velocity.y > 1)
                {
                    bgm[2].Play();
                }
            }

            //아이템을 먹었을경우 효과
            if (isItem == true)
            {
                playercolor.material.color = Color.blue;
            }
            else
            {
                playercolor.material.color = Color.white;
            }

            //부모가 있는 상태에서 점프를 했을경우
            if (transform.parent != null && isjump == true) //플레이어의 부모가 있고 , 점프를 한 상태인경우
            {
                transform.parent = null;
            }
            Ray ray = new Ray(transform.position, Vector3.down);
            Debug.DrawRay(transform.position, Vector3.down, Color.green);
            RaycastHit hitinfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitinfo, Layer) && isjump == false)
            {
                if (hitinfo.collider.tag == "MoveWall")
                {
                    transform.parent = hitinfo.transform;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {       
        if(other.name == "Wall")
        {
            other.GetComponent<BoxCollider>().isTrigger = true;            
            if(other.name == "Trap"&&isItem == false)
            {
                GAMEOVER();
            }//플레이어가 아이템을 먹은상태일경우
            //else if(other.tag == "Trap"&&isItem == true)
            //{
                
            //}
        }
        else if (other.name == "MoveWall" && isjump == true)
        {
            other.GetComponent<BoxCollider>().isTrigger = true;
        }
        else if(other.tag == "BOX")
        {
            GetComponent<BoxCollider>().isTrigger = false;
            
            GameManager.Instance.isBox = true;
        }        
    }    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.name == "MoveWall")
        {
            this.transform.SetParent(collision.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag != "WallBox"&&other.name != "SmokeWall")
        {            
            other.GetComponent<BoxCollider>().isTrigger = false;
        }       
    }
    private void OnCollisionEnter(Collision collision)
    {                
        if(collision.transform.name == "Trap"&&isItem == false)
        {                        
            GAMEOVER();            
        }
        else if(collision.transform.name == "HorizionWall")
        {
            bgm[1].Play();
            GameManager.Instance.isBox = true;
        }
        //else if (collision.transform.name == "MoveWall")
        //{
        //    this.transform.SetParent(collision.transform);
        //}
    }
    public void Restart()
    {
        if(g_GAMEOVER.activeSelf == true)
        {
            Time.timeScale = 1f;
            g_GAMEOVER.SetActive(false);
            
        }        
        Time.timeScale = 1f;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void GAMEOVER()
    {
        isGameOver = true;
        GameManager.Instance.GetComponent<AudioSource>().Stop();
        GameManager.Instance.GetComponent<AudioSource>().Play();
        print(GameManager.Instance.GetComponent<AudioSource>().isPlaying);
        Time.timeScale = 0f;
        g_GAMEOVER.SetActive(true);
        bgm[0].Stop();
    }
}
