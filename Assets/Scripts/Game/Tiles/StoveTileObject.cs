using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stove Tile", menuName = "Tiles/Stove Tile")]
public class StoveTile : TileObject
{

    [Header("Object Assignments")]
    [SerializeField] private WorldTimer _stoveTimerPrefab;
    [SerializeField] private PanHandler _stovePanPrefab;

    private Vector3 _popupOffset = new(0, 0.5f);
    private bool _isCooking = false;

    private PanHandler _panHandler = null;  // If no pan on stove, null, else PanHandler

    public override void Interact(Vector3Int position)
    {
        // If we have a pan, interact with the pan instead!
        if (_panHandler != null)
        {
            Player.Instance.HoldItem(_panHandler.gameObject);
            _panHandler = null;
            return;
        }

        // Interact with the stove and cook some food
        _isCooking = true;
        Vector3 stovePosition = (Vector3)position + new Vector3(0.5f, 0.5f, 0);

        // Create a pan to show at the stove
        _panHandler = Instantiate(_stovePanPrefab, stovePosition, Quaternion.identity);

        // Spawn the popup above the stove
        WorldTimer stoveTimer = Instantiate(_stoveTimerPrefab);
        stoveTimer.TickForSeconds(8);
        PopupManager.Instance.MovePopupThen(stoveTimer.gameObject, stovePosition + _popupOffset, 8, () =>
        {
            _isCooking = false;
            _panHandler.FinishCooking();
        });
    }

    public override void OnEnterInteractRange(Vector3Int position)
    {
        IsInteractable = !Player.Instance.IsHoldingItem() && !_isCooking;
    }

    public override void OnExitInteractRange(Vector3Int position)
    {
        
    }
}
