using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private tileManager tileManager;
    [SerializeField] private GameManager gameManager;
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

        if (_CurrentPreviewTower == null )
        {
            _CurrentPreviewTower = tileManager.Place(_tower._Tower,tileManager.RoundToCell(_currentMousePosition));
            _CurrentPreviewTower.GetComponent<StructBase>().enabled = false;
            _CurrentPreviewTower.GetComponentInChildren<SpriteRenderer>().enabled = true;
            _CurrentPreviewTower.transform.GetChild(0).localScale =
                Vector3.one * _CurrentPreviewTower.GetComponent<StructBase>()._Range *2;
            if (_CurrentPreviewTower == null)return;
            _CurrentPreviewTower.GetComponentInChildren<Collider2D>().enabled = false;
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

            if (!tileManager.CanPlace(mousePos,_CurrentPreviewTower))
            {
                ResetPreview();
                return;
            }
            
            if (_tower._Tower != null &&  gameManager._Money >= _CurrentPreviewTower.GetComponent<StructBase>()._price)
            {
                //_characterData._Inventory.TryRemoveItems(itemStructure, 1);
                gameManager._Money -= _CurrentPreviewTower.GetComponent<StructBase>()._price;
                gameManager.updateMoneyText();
                GameObject structure = tileManager.Place(_tower._Tower,mousePos);
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

            RaycastHit2D hit = Physics2D.Raycast(mousePos + Vector3.back * 10 ,
                Camera.main.transform.forward, 11f);
            if (hit.collider == null)
            {
                ResetPreview();
                return;
            }

            StructBase @struct = hit.collider.GetComponentInParent<StructBase>();
            if (@struct == null)
            {
                ResetPreview();
                return;
            }
            gameManager._Money += @struct._price/2;
            gameManager.updateMoneyText();
            Destroy(@struct.gameObject);
            ResetPreview();
        }
    }

    private void ResetPreview()
    {
        _PlacedTower = null;
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
        Vector2 currentMousePositionRounded = tileManager.RoundToCell(Camera.main.ScreenToWorldPoint(_currentMousePosition));
        if (_lastMousePosition != currentMousePositionRounded)
        {
            _lastMousePosition = currentMousePositionRounded + Vector2.one/4;
        }
        _CurrentPreviewTower.transform.position = _lastMousePosition;
    }

    private bool CheckUIInTheWay()
    {
        PointerEventData customEventData = new PointerEventData(EventSystem.current);
        customEventData.position = _currentMousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(customEventData, results);

        if (results.Where(x => x.gameObject.GetComponentInParent<StructBase>() == null).Count() > 0)
        {
            return true;
        }
        return false;
    }
}
