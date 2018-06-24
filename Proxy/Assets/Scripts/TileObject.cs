using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// client side code to highlight tiles
/// </summary>
public class TileObject : MonoBehaviour{
    public MaterialToggle highlightMat;
    public int ID;
    public int HeldColumn;
    public int owningPlayer;

    public void Start(){
        owningPlayer = (int)GetComponentInParent<UnitPlacer>().netId.Value;
        highlightMat = GetComponent<MaterialToggle>();
    }

    public delegate void SelectedEvent(TileObject t, int dir);
    public SelectedEvent Selected;

    public delegate void ConfirmEvent(TileObject t, int ownedBy);
    public ConfirmEvent Confirm;

    void OnMouseEnter(){
        if(Selected!=null) Selected(this, 1);
    }

    void OnMouseDown(){
        if(Confirm != null) Confirm(this, owningPlayer);
    }

    void OnMouseExit() {
        if (Selected != null) Selected(this, 0);
    }
}
