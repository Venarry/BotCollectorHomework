using System;
using System.Collections;
using UnityEngine;

public class UserInteractHandler : MonoBehaviour
{
    private PanelClickHandler _panelClickHandler;
    private Camera _camera;
    private Coroutine _activeCoroutine;
    private bool _isClickedBuffer;

    public void Init(
        PanelClickHandler panelClickHandler,
        Camera camera)
    {
        _panelClickHandler = panelClickHandler;
        _camera = camera;

        _panelClickHandler.Clicked += OnClick;
    }

    private void OnClick()
    {
        if(TryGetWorldMousePosition(out RaycastHit hit))
        {
            if(hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact(this);
            }
        }
    }

    public void PrepareToBuild(
        GameObject proectionPrefab,
        Action<Vector3> action)
    {
        _activeCoroutine ??= StartCoroutine(Building(proectionPrefab, action));
    }

    private IEnumerator Building(GameObject proectionPrefab, Action<Vector3> action)
    {
        GameObject proection = Instantiate(proectionPrefab);
        RaycastHit hit;

        _isClickedBuffer = false;
        _panelClickHandler.Clicked += OnActionClick;

        while (_isClickedBuffer == false)
        {
            if (TryGetWorldMousePosition(out hit))
            {
                if(hit.collider.TryGetComponent(out Ground _))
                {
                    proection.transform.position = hit.point;
                }
            }

            yield return null;
        }

        _panelClickHandler.Clicked -= OnActionClick;

        if (TryGetWorldMousePosition(out hit))
        {
            if (hit.collider.TryGetComponent(out Ground _))
            {
                action.Invoke(hit.point);
            }
        }

        _activeCoroutine = null;
        Destroy(proection);
    }

    private void OnActionClick()
    {
        _isClickedBuffer = true;
    }

    private bool TryGetWorldMousePosition(out RaycastHit hit)
    {
        Ray clickRay = _camera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(clickRay, out hit);
    }
}
