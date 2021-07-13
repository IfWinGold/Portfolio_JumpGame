using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tema : MonoBehaviour
{    
    [SerializeField] AudioSource[] Buttonsound;
    [SerializeField] Material bgMaterial;
    [SerializeField] Image image;

    //스크롤 속도
    public float scrollSpeed = 0.2f;

    private void Start()
    {
        Buttonsound = GetComponents<AudioSource>();
        Buttonsound[0].Play();
    }

    public void NextScene()
    {
        Buttonsound[0].Stop();
        Buttonsound[1].Play();
        StartCoroutine(NextScene_Next());
           
    }
    IEnumerator NextScene_Next()
    {
        yield return new WaitForSeconds(0.5f);
        if (!Buttonsound[1].isPlaying)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void Update()
    {
        Vector2 direction = Vector2.up;

        bgMaterial.mainTextureOffset += direction * scrollSpeed * Time.deltaTime;
        
    }
}
