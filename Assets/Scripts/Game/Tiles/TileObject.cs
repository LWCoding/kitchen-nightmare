using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TileObject : ScriptableObject
{

    [Header("Base Tile Properties")]
    public TileBase Tile;
    public bool IsInteractable = false;
    public bool IsPurchaseable = true;
    [ShowIf("IsPurchaseable")]
    public int CostToBuy;

    public abstract void Initialize(Vector3Int position);
    public abstract void OnEnterInteractRange(Vector3Int position);
    public abstract void OnExitInteractRange(Vector3Int position);
    public abstract void Interact(Vector3Int position);

}
