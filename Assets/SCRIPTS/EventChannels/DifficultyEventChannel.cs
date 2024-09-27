using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DifficultyEventChannel", menuName = "Events/Difficulty", order = 0)]
public class DifficultyEventChannel : ScriptableObject
{
    public event UnityAction<DifficultySO> onDifficultyEvent;

    public void RaiseEvent(DifficultySO difficultySo)
    {
        onDifficultyEvent?.Invoke(difficultySo);
    }
}