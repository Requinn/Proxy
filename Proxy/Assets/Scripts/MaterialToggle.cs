using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//toggles the material of an object
public class MaterialToggle : MonoBehaviour{
    public Material[] materialList;
    private Renderer _render;

	// Use this for initialization
	void Start (){
	    _render = GetComponent<Renderer>();
	}

    /// <summary>
    /// toggle a material to the specified index, default 0
    /// </summary>
    /// <param name="index"></param>
    public void Toggle(int index = 0){
        _render.material = materialList[index];
    }
}
