using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// client side code to highlight tiles
/// </summary>
public class SelectHighlight : MonoBehaviour{
    public MaterialToggle highlightMat;

    public void Start(){
        highlightMat = GetComponent<MaterialToggle>();
    }
    void OnMouseEnter(){
        highlightMat.Toggle(1);
    }

    void OnMouseExit() {
        highlightMat.Toggle(0);
    }
}
