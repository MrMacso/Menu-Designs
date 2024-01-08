using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class CardSelectionManager : MonoBehaviour
{
    public static CardSelectionManager Instance;
    public GameObject[] Cards;

    public GameObject LastSelected { get; set; }
    public int LastSelectedIndex { get; set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void OnEnable()
    {
        StartCoroutine(SetSelectedAfterOneFrame());
    }

    void Update()
    {
        //select next card
        if (InputManager.Instance.NavigationInput.y > 0)
        {
            HandleNextCardSelection(1);
        }
        //select prior card
        if (InputManager.Instance.NavigationInput.y < 0)
        { 
            HandleNextCardSelection(-1);
        }

    }

    IEnumerator SetSelectedAfterOneFrame()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(Cards[0]);
    }

    void HandleNextCardSelection(int addition) 
    {
        if (EventSystem.current.currentSelectedGameObject == null && LastSelected != null)
        { 
            int newIndex = LastSelectedIndex + addition;
            newIndex = Mathf.Clamp(newIndex, 0, Cards.Length - 1);
            EventSystem.current.SetSelectedGameObject(Cards[newIndex]);
        }
    }
}
