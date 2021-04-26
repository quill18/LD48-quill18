using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class RestartButton : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("SceneManager.LoadScene");
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("SceneManager.LoadScene");
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
        
    }


}
