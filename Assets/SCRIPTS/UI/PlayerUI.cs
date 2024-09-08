using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Player player;

    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private GameObject driveUI;
    [SerializeField] private GameObject downloadUI;

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