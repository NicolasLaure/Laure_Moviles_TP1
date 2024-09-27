using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDifficultyManager : MonoBehaviour
{
    [SerializeField] private DifficultyHandlerSO difficultyHandlerSo;

    [SerializeField] private DifficultyEventChannel onEasySelected;
    [SerializeField] private DifficultyEventChannel onMediumSelected;
    [SerializeField] private DifficultyEventChannel onHardSelected;

    [SerializeField] private GameObject easySelectedGameObject;
    [SerializeField] private GameObject mediumSelectedGameObject;
    [SerializeField] private GameObject hardSelectedGameObject;

    void Start()
    {
        onEasySelected.onDifficultyEvent += HandleDifficultyChange;
        onMediumSelected.onDifficultyEvent += HandleDifficultyChange;
        onHardSelected.onDifficultyEvent += HandleDifficultyChange;

        ToggleSelectedDifficulty();
    }

    private void OnDestroy()
    {
        if (onEasySelected != null)
            onEasySelected.onDifficultyEvent -= HandleDifficultyChange;

        if (onMediumSelected != null)
            onMediumSelected.onDifficultyEvent -= HandleDifficultyChange;

        if (onHardSelected != null)
            onHardSelected.onDifficultyEvent -= HandleDifficultyChange;
    }

    private void HandleDifficultyChange(DifficultySO difficultySo)
    {
        difficultyHandlerSo.SetDifficulty(difficultySo);
        ToggleSelectedDifficulty();
    }

    private void ToggleSelectedDifficulty()
    {
        switch (difficultyHandlerSo.GetCurrentDifficulty().name)
        {
            case "Easy":
                easySelectedGameObject.SetActive(true);
                mediumSelectedGameObject.SetActive(false);
                hardSelectedGameObject.SetActive(false);
                break;
            case "Medium":
                easySelectedGameObject.SetActive(false);
                mediumSelectedGameObject.SetActive(true);
                hardSelectedGameObject.SetActive(false);
                break;
            case "Hard":
                easySelectedGameObject.SetActive(false);
                mediumSelectedGameObject.SetActive(false);
                hardSelectedGameObject.SetActive(true);
                break;
        }
    }
}