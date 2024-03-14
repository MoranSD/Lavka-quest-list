using Meta.Quests.Core;
using Meta.Quests.Core.Quests;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Meta.UI.Quests
{
    public class QuestAdderPanel : MonoBehaviour
    {
        [SerializeField] private Button _adventureButton;
        [SerializeField] private Button _collectingButton;

        [Inject] private QuestManager _questManager;
        [Inject] private QuestList _questList;

        private void Awake()
        {
            _adventureButton.onClick.AddListener(() => OnPressAddButton(QuestType.adventure));
            _collectingButton.onClick.AddListener(() => OnPressAddButton(QuestType.collecting));
        }
        private void OnDestroy()
        {
            _adventureButton.onClick.RemoveAllListeners();
            _collectingButton.onClick.RemoveAllListeners();
        }
        private void OnPressAddButton(QuestType questType)
        {
            if (_questList.isAnimating) return;

            _questManager.Add(questType);
        }
    }
}