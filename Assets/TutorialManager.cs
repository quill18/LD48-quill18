using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VisibleTutorials = new List<GameObject>();
        AlreadyShown = new List<GameObject>();
        Show(TutorialCard);
        Show(TutorialQuest);
    }

    static TutorialManager _Instance;
    public static TutorialManager Instance {
        get {
            if(_Instance == null)
            {
                _Instance = GameObject.FindObjectOfType<TutorialManager>();
            }
            return _Instance;
        }
    }

    public GameObject TutorialCard;
    public GameObject TutorialQuest;
    public GameObject TutorialDrill;
    public GameObject TutorialMorale;
    public GameObject TutorialWorkforce;

    List<GameObject> VisibleTutorials;
    List<GameObject> AlreadyShown;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Hide all tutorial messages on mouse click.
            while(VisibleTutorials.Count > 0)
            {
                VisibleTutorials[0].SetActive(false);
                VisibleTutorials.RemoveAt(0);
            }
        }
    }

    public void Show(GameObject go)
    {
        if(AlreadyShown.Contains(go))
            return;

        AlreadyShown.Add(go);
        StartCoroutine(coShow(go));
    }

    private IEnumerator coShow(GameObject go)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(1f);
        VisibleTutorials.Add(go);
    }
}
