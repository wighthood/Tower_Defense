using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TowerManager : MonoBehaviour
{
    private Vector2 _currentMousePosition = Vector2.zero;
    private Vector2 _lastMousePosition = Vector2.zero;
    private GameObject _CurrentPreviewTower;
    private Tower _tower;
    public Tower _PlacedTower;
    
    public void MouseMovementAction(InputAction.CallbackContext context)
    {
        _currentMousePosition = context.ReadValue<Vector2>();
        if (_currentMousePosition == Vector2.zero)
        {
            return;
        }

        _tower = _PlacedTower;
        if (_tower == null || _tower._Tower == null)
        {
            return;
        }

        if (_CurrentPreviewTower == null)
        {
            _CurrentPreviewTower = TileManager._Instance.Place(_tower._Tower,TileManager._Instance.RoundToCell(_currentMousePosition));
            _CurrentPreviewTower.GetComponent<TowerScript>().enabled = false;
            if (_CurrentPreviewTower == null)return;
        }
        UpdatePreview();
    }

  public void LeftClickAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _tower = _PlacedTower;
            if (_currentMousePosition == Vector2.zero || _tower == null) 
            {
                ResetPreview();
                return; 
            }

            if (CheckUIInTheWay())
            {
                ResetPreview();
                return;
            }

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(_currentMousePosition);

            if (!TileManager._Instance.CanPlace(mousePos))
            {
                ResetPreview();
                return;
            }
            
            if (_tower._Tower != null)
            {
                //_characterData._Inventory.TryRemoveItems(itemStructure, 1);
                GameObject structure = TileManager._Instance.Place(_tower._Tower,mousePos);
                if (structure == null)
                {
                    ResetPreview();
                }
            }
        }
    }

    public void RigthClickAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_currentMousePosition == Vector2.zero)
            {
                return;
            }

            if (CheckUIInTheWay())
            {
                ResetPreview();
                return;
            }

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(_currentMousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos + Vector3.back * 10,
                Camera.main.transform.forward, 11f);
            if (hit.collider == null)
            {
                ResetPreview();
                return;
            }

            TowerScript tower = hit.collider.GetComponentInParent<TowerScript>();
            if (tower == null)
            {
                ResetPreview();
                return;
            }
            Destroy(tower.gameObject);
            ResetPreview();
        }
    }

    private void ResetPreview()
    {
        _tower = null;
        if (_CurrentPreviewTower!= null)
        {
            Destroy(_CurrentPreviewTower);
            _CurrentPreviewTower = null;
        }
    }

    private void UpdatePreview()
    {
        if (_CurrentPreviewTower == null || _tower == null || _tower._Tower == null || _currentMousePosition == Vector2.zero)
        {
            ResetPreview();
            return;
        }
        Vector2 currentMousePositionRounded = TileManager._Instance.RoundToCell(Camera.main.ScreenToWorldPoint(_currentMousePosition));
        
        if (_lastMousePosition != currentMousePositionRounded)
        {
            _lastMousePosition = currentMousePositionRounded;
        }
        _CurrentPreviewTower.transform.position = _lastMousePosition;
    }

    private bool CheckUIInTheWay()
    {
        PointerEventData customEventData = new PointerEventData(EventSystem.current);

        customEventData.position = _currentMousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(customEventData, results);

        if (results.Where(x => x.gameObject.GetComponentInParent<TowerScript>() == null).Count() > 0)
        {
            return true;
        }
        return false;
    }
}
