using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelectionHandler : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] float _verticalMoveAmount = 30f;
    [SerializeField] float _moveTime = 0.1f;
    [Range(0f, 2f), SerializeField] float _scaleAmount = 1.1f;

    Vector3 _startPos;
    Vector3 _startScale;
    void Start()
    {
        _startPos= transform.position;
        _startScale= transform.localScale;
    }


    IEnumerator MoveCard(bool startingAnimation)
    {
        Vector3 endPosition;
        Vector3 endScale;

        float elapsedTime = 0f;
        while (elapsedTime < _moveTime)
        {
            elapsedTime+= Time.deltaTime;

            if (startingAnimation)
            {
                endPosition = _startPos + new Vector3(0f, _verticalMoveAmount, 0f);
                endScale = _startScale * _scaleAmount;
            }
            else
            {
                endPosition = _startPos;
                endScale= _startScale;
            }

            Vector3 lerpedPos = Vector3.Lerp(transform.position, endPosition, (elapsedTime / _moveTime));
            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / _moveTime));

            transform.position= lerpedPos;
            transform.localScale= lerpedScale;

            yield return null;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //select the button
        eventData.selectedObject = gameObject;
    }

    /*public void OnPointerExit(PointerEventData eventData)
    {
        //deselect the button
        eventData.selectedObject = null;
    }*/

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(true));

        CardSelectionManager.Instance.LastSelected = gameObject;

        //find the index
        for (int i = 0; i < CardSelectionManager.Instance.Cards.Length; i++)
        { 
            CardSelectionManager.Instance.LastSelectedIndex = i;
            return;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(false));
    }
}
