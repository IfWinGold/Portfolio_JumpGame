using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] Text text_Score;
    [SerializeField] Text Height;
    [SerializeField] Text text_Max_Score;
    [SerializeField] InputField if_gravity;
    

    public int Score = 0;
    float currentPosition = 0f;
    float MaxPosition = 0f;
   
    bool playerjump = false;
    public bool isBox = false;

    void Awake()
    {
       if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        text_Score.text = "점수 : ";
        if_gravity.text = "-9";
    }

    // Update is called once per frame
    void Update()
    {
        float gravity = float.Parse(if_gravity.text);
        Physics.gravity = new Vector3(0, gravity, 0);

        Height.text = "Height : " + PlayerControl.Instance.transform.position.y;
        //캐릭터가 점프를 하지 않은 상태일경우
        if (PlayerControl.Instance.isjump == false)
        {
            
            if(MaxPosition<=PlayerControl.Instance.transform.position.y)
            {
                MaxPosition = PlayerControl.Instance.transform.position.y;
                text_Score.text = "점수 : " + (int)MaxPosition;
            }
        }
        //Physics.gravity.y = 
       // if()
    }
    public void GameReset()
    {
        currentPosition = 0f;
        MaxPosition = 0f;
        Score = 0;
    }
    public void PlayerPositionUp()
    {
        
    }
}
