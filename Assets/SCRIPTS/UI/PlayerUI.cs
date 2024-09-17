using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Player player;

    public PlayerConfigSO config;

    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private GameObject driveUI;
    [SerializeField] private GameObject downloadUI;

    [Header("Necessary Instances")]
    [SerializeField] private Image inventario;

    [SerializeField] private TextMeshProUGUI dinero;
    [SerializeField] private Transform volante;

    //BONO DE DESCARGA
    [SerializeField] private GameObject bonusRoot;
    [SerializeField] private Image bonusFill;
    [SerializeField] private TextMeshProUGUI bonusText;

    //CALIBRACION MAS TUTO BASICO
    [SerializeField] private GameObject tutoCalibrando;
    [SerializeField] private GameObject tutoDescargando;
    [SerializeField] private GameObject tutoFinalizado;

    public Image Inventario => inventario;
    public TextMeshProUGUI Dinero => dinero;
    public Transform Volante => volante;
    public GameObject BonusRoot => bonusRoot;
    public Image BonusFill => bonusFill;
    public TextMeshProUGUI BonusText => bonusText;
    public GameObject TutoCalibrando => tutoCalibrando;
    public GameObject TutoDescargando => tutoDescargando;
    public GameObject TutoFinalizado => tutoFinalizado;

    private void Awake()
    {
        config.ui = this;
    }

    void OnEnable()
    {
        player.onTutorialEnter += HandleToggleTutorialUI;
        player.onDriveEnter += HandleToggleDriveUI;
        player.onDownloadEnter += HandleToggleDownloadUI;
    }

    void OnDisable()
    {
        if (player != null)
        {
            player.onTutorialEnter -= HandleToggleTutorialUI;
            player.onDriveEnter -= HandleToggleDriveUI;
            player.onDownloadEnter -= HandleToggleDownloadUI;
        }
    }

    public void ToggleOnOff(bool value)
    {
        gameObject.SetActive(value);
    }

    private void HandleToggleTutorialUI()
    {
        tutorialUI.SetActive(true);
        driveUI.SetActive(false);
        downloadUI.SetActive(false);
    }

    private void HandleToggleDriveUI()
    {
        tutorialUI.SetActive(false);
        driveUI.SetActive(true);
        downloadUI.SetActive(false);
    }

    private void HandleToggleDownloadUI()
    {
        tutorialUI.SetActive(false);
        driveUI.SetActive(false);
        downloadUI.SetActive(true);
    }
}