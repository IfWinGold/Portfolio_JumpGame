using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Test : MonoBehaviour , IPointerDownHandler,IPointerUpHandler,IDragHandler
{     
    bool isjump;
    float JumpPower = 7f;
    Rigidbody rigidbody;
    public float speed ;
    
    Vector2 Downposition;
    [SerializeField] Slider Gauge;
    [SerializeField] GameObject ArrowPosition;
    [SerializeField] GameObject DragPower;
    [SerializeField] GameObject player;
    [SerializeField] GameObject target;
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject TopArrow;
    [SerializeField] InputField sensitivity;
    [SerializeField] GameObject TopArrow_target;
    

    public void OnDrag(PointerEventData eventData)
    {
        if(PlayerControl.Instance.isjump!=true)
        {            
            ArrowPosition.SetActive(true);
            DragPower.SetActive(true);
            Vector3 mPosition = eventData.position; //마우스의 좌표
            Vector3 oPosition = Downposition;//게임 오브젝트 좌표
                                    //Vector2 Downposition = eventData.position;
            float f_sensitivity = float.Parse(sensitivity.text);

            float dis = oPosition.y - mPosition.y;
            float disvalue = dis / f_sensitivity;
            //print(disvalue);
            Gauge.value = disvalue;
            float MaxArrowValue = 0.2f;
            float ArrowScaleValue = 0.05f;
            
            if(Gauge.value <1f)
            {
                ArrowScaleValue += Gauge.value * 0.15f;               
            }
            if(Gauge.value == 1)
            {
                ArrowScaleValue = MaxArrowValue;                
            }                       
            Vector3 ArrowScale = new Vector3(ArrowScaleValue,Arrow.transform.localScale.y, Arrow.transform.localScale.z);            
            //target.transform.localPosition = targetTrans;
            Arrow.transform.localScale = ArrowScale;
            //print(TopArrow.transform.lossyScale);
          
            
            
            //카메라가 앞면에서 뒤로 보고 있기 때문에, 마우스 position의 z측 정보에 
            //게임 오브젝트와 카메라와의 z축의 차이를 입력시켜줘야 된다.
            //mPosition.z = oPosition.z - Camera.main.transform.position.z;

            //화면의 픽셀별로 변화되는 마우스의 좌표를 유니티의 좌표로 변화해 줘야 한다.
            //그래야, 위치를 찾아갈 수 있겠습니다.
            //Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

            //다음은 아크탄젠트로 게임 오브젝트의 좌표와 마우스 포인트의 좌표를
            //이용하여 각도를 구한 후, 오일러(Euler)회전 함수를 사용하여 게임 오브젝트를
            //회전시키기 위해, 각 축의 거리차를 구한 후 오일러 회전함수에 적용시킨다.

            //우선 각 축의 거리를 계산하여, dy, dx에 저장해 둡니다.
            float dy = mPosition.y - oPosition.y;
            float dx = mPosition.x - oPosition.x;

            //오일러 회전 함수를 0에서 180또는 0에서 -180의 각도를 입력 받는데 반하여
            //(물론 270과 같은 값의 입력도 전혀 문제없습니다.) 아크 탄젠트 Atan2() 함수
            //의 결과 값은 라디안 값(180도가 파이)으로 출력되므로
            //라디안 값을 각도로 변화하기 위해 Rad2Deg를 곱해주어야 각도가 된다.
            float rotateDegree = Mathf.Atan2(-dy, -dx) * Mathf.Rad2Deg;
            ArrowPosition.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
        }     
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(PlayerControl.Instance.isjump!=true)
        {
            Downposition = eventData.position;
        }
        //print("down!");                
    }

    public void OnPointerUp(PointerEventData eventData)
    {    
        if(PlayerControl.Instance.isjump!=true)
        {            
            Vector3 targetposition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);


            Vector3 disXYZ = targetposition - transform.position;
            Vector3 disXY = disXYZ;
            disXY.z = 0f;//z값은 필요없습니다.




            Vector3 result_normal = disXY.normalized;

            float temp = Gauge.value + 1;            
            rigidbody.AddForce((targetposition - player.transform.position).normalized * JumpPower * temp, ForceMode.Impulse);
            //점프           
            ArrowPosition.transform.eulerAngles = new Vector3(0, 0, 0);
            print(speed);
            Gauge.value = 0f;

            ArrowPosition.SetActive(false);

            DragPower.SetActive(false);
            //print(rigidbody.velocity);            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = player.GetComponent<Rigidbody>();
        sensitivity.text = "500";
    }

    // Update is called once per frame
    void Update()
    {
        TopArrow.transform.position = new Vector3(TopArrow_target.transform.position.x, TopArrow_target.transform.position.y, TopArrow_target.transform.position.z);
    }
}
