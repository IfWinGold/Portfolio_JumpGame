using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public static WallManager Instance = null;    
    [SerializeField] GameObject[] _1stPatterns;
    [SerializeField] GameObject[] _2stPatterns;
    [SerializeField] GameObject[] _3stPatterns;
    [SerializeField] GameObject[] _4stPatterns;
    [SerializeField] GameObject[] _5stPatterns;
    [SerializeField] GameObject[] _6stPatterns;    
    
    List<GameObject> PatternList = new List<GameObject>();
    float Height = 73f;
    float n_PlayerHeight;

    bool b_1st = false;
    bool b_2st = false;
    bool b_3st = false;
    bool b_4st = false;
    bool b_5st = false;
    bool b_6st = false;
    enum Round
    {
    _1st, //초기
    _2st, //140
    _3st, //200
    _4st, //250
    _5st, //300
    _6st, //350
    }
    Round round = Round._1st;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        n_PlayerHeight = PlayerControl.Instance.transform.position.y;        
        if (n_PlayerHeight >= (Height - 20)) //처음은 53f
        {
            CreateWall();
        } 
        
        if(n_PlayerHeight > 140f&& n_PlayerHeight<200f) 
        {
            //140보다 크고 200보다 작은경우
            round = Round._2st;
        }
        else if(n_PlayerHeight > 200f && n_PlayerHeight < 250f) //200보다 클경우
        {
            //200보다 크고 250보다 작은경우
            round = Round._3st;
        }
        else if(n_PlayerHeight > 250f && n_PlayerHeight < 300f) 
        {
            //250보다 크고 300보다 작은경우
            round = Round._4st;
        }
        else if(n_PlayerHeight > 300f&&n_PlayerHeight <350f) //300보다 클경우
        {
            //300보다 크고 350보다 작은경우
            round = Round._5st;
        }
        else if(n_PlayerHeight > 350f) 
        {
            // 300보다 클 경우
            round = Round._6st;
        }
    }
    public void CreateWall()
    {
        print("****************round : "+round);
        print("PatternListCount : " + PatternList.Count);

        switch (round)
        {
            case Round._1st:                
                if(b_1st != true) //추가를 하지 않았을 경우에는
                {
                    AddWall(_1stPatterns,ref b_1st);
                }                
                break;
            case Round._2st:
                if(b_2st != true)
                {
                    AddWall(_2stPatterns,ref b_2st);
                }
                break;
            case Round._3st:
                if(b_3st != true)
                {
                    AddWall(_3stPatterns,ref b_3st);
                }
                break;
            case Round._4st:
                if(b_4st != true)
                {
                    AddWall(_4stPatterns,ref b_4st);
                }
                break;
            case Round._5st:                
                if(b_5st != true)
                {
                    AddWall(_5stPatterns,ref b_5st);
                }
                break;
            case Round._6st:
                if(b_6st != true)
                {
                    AddWall(_6stPatterns,ref b_6st);
                }
                break;
        }

        for (int i = 0; i <10;i++)
        {
            int Random = UnityEngine.Random.Range(0, PatternList.Count);
            print("Random : " + Random);
            GameObject temp = Instantiate(PatternList[Random]);
            temp.transform.position = new Vector3(0, Height, 0);
            Height += 6f;
            print("NoteName : " + temp.transform.name);
        }
    }

    public void AddWall(GameObject[] patterns,ref bool b_st)
    {
        for(int i =0; i < patterns.Length; i++)
        {
            PatternList.Add(patterns[i]);
            b_st = true;
        }
    }
}
