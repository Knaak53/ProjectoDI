using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalizarTurno : MonoBehaviour {
    public CellGrid cellGrid;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void finalizarTurno()
    {
        cellGrid.EndTurn();
    }


}
