using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MiGUI : MonoBehaviour {
    public CellGrid CellGrid;
    public Transform UnitsParent;
    public GameObject HudHeroes;
    public GameObject HudVillanos;
    public Text agilidadH;
    public Text fuerzaH;
    public Text resistenciaH;
    public Text saludH;
    public Text agilidadV;
    public Text fuerzaV;
    public Text resistenciaV;
    public Text saludV;
    public Image imagenH;
    public Image imagenV;
    public Text info;
    public Sprite finalizarDesactivado;
    public Sprite finalizarActivado;
    //public Image ImagenHeroe;
    //public Image EmptyHPBar;
    public Button NextTurnButton;

    private void Start()
    {
        HudHeroes.SetActive(false);
        HudVillanos.SetActive(false);
        CellGrid.GameStarted += OnGameStarted;
        CellGrid.TurnEnded += OnTurnEnded;
        CellGrid.GameEnded += OnGameEnded;
    }

    private void OnUnitAttacked(object sender, AttackEventArgs e)
    {
        if (!(CellGrid.CurrentPlayer is HumanPlayer)) return;

        OnUnitDehighlighted(sender, e);

        if ((sender as SampleUnit).HitPoints <= 0) return;

        OnUnitHighlighted(sender, e);
    }
    private void OnGameStarted(object sender, EventArgs e)
    {
        foreach (Transform unit in UnitsParent)
        {
            unit.GetComponent<SampleUnit>().UnitHighlighted += OnUnitHighlighted;
            unit.GetComponent<SampleUnit>().UnitDehighlighted += OnUnitDehighlighted;
            unit.GetComponent<SampleUnit>().UnitAttacked += OnUnitAttacked;
        }
        OnTurnEnded(sender, e);
        NextTurnButton.image.sprite = finalizarActivado;
    }
    private void OnGameEnded(object sender, EventArgs e)
    {
        //TODO FInalizar juego
        //InfoText.text = "Player " + ((sender as CellGrid).CurrentPlayerNumber + 1) + " wins!";
        NextTurnButton.interactable = false;
        info.enabled = true;
        if ((sender as CellGrid).CurrentPlayerNumber == 0)
        {
            info.text = "¡Tú Ganas!";
        }else
        {
            info.text = "¡Has Perdido!";
        }
        

    }
    private void OnTurnEnded(object sender, EventArgs e)
    {
        NextTurnButton.interactable = ((sender as CellGrid).CurrentPlayer is HumanPlayer);
        NextTurnButton.image.sprite = finalizarDesactivado;
        //InfoText.text = "Player " + ((sender as CellGrid).CurrentPlayerNumber + 1);
    }

    private void OnUnitDehighlighted(object sender, EventArgs e)
    {
        if ((sender as SampleUnit).PlayerNumber == 0)
        {
            HudHeroes.SetActive(false);
        }
        else
        {
            HudVillanos.SetActive(false);
        }
    }
    private void OnUnitHighlighted(object sender, EventArgs e)
    {
        var attack = (sender as SampleUnit).AttackFactor;
        var defence = (sender as SampleUnit).DefenceFactor;
        var agility = (sender as SampleUnit).MovementPoints;
        var health = (sender as SampleUnit).HitPoints;

        if ((sender as SampleUnit).PlayerNumber == 0)
        {
            imagenH.sprite = (sender as SampleUnit).gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
            agilidadH.text = attack+"";
            resistenciaH.text = defence+"";
            agilidadH.text = agility+"";
            saludH.text = health+"";
            HudHeroes.SetActive(true);
        }else
        {
            imagenV.sprite = (sender as SampleUnit).gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
            agilidadV.text = attack + "";
            resistenciaV.text = defence + "";
            agilidadV.text = agility + "";
            saludV.text = health + "";
            HudVillanos.SetActive(true);
        }


    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
