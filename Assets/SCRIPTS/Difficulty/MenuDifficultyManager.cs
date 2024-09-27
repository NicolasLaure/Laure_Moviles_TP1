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

    void Start()
    {
        onEasySelected.onDifficultyEvent += HandleDifficultyChange;
        onMediumSelected.onDifficultyEvent += HandleDifficultyChange;
        onHardSelected.onDifficultyEvent += HandleDifficultyChange;
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
    }
}