using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public int scenenumber = 2;
    [SerializeField] Slider Bar;
    [SerializeField] Text LoadingNum;

    void Start()
    {
        StartCoroutine(TransitionNextScene(scenenumber));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator TransitionNextScene(int num)
    {
        //지정된 씬을 비동기 형식으로 로딩
        AsyncOperation ao = SceneManager.LoadSceneAsync(num);

        //로드되는 씬 모습 안보이게
        ao.allowSceneActivation = false;

        while(!ao.isDone)
        {
            //로딩 진행률 슬라이더 바와 텍스트로 표시
            Bar.value = ao.progress;
            LoadingNum.text = (ao.progress * 100f).ToString("F0") + "%";

            if(ao.progress >= 0.9f)
            {
                ao.allowSceneActivation = true;
            }

            //다음프레임까지 대기
            yield return null;
        }
    }
}
