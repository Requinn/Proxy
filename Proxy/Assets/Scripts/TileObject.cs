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
    public int owningPlayer;
    public void Start(){
        highlightMat = GetComponent<MaterialToggle>();
    }

    public delegate void SelectedEvent();
    public SelectedEvent Selected;

    public delegate void ConfirmEvent(int tileID, int ownedBy);
    public ConfirmEvent Confirm;

    void OnMouseEnter(){
        highlightMat.Toggle(1);
        if(Selected!=null) Selected();
    }

    void OnMouseDown(){
        if()
        Confirm(ID, owningPlayer);
    }

    void OnMouseExit() {
        highlightMat.Toggle(0);
    }
}
