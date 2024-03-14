using Meta.Quests.Core.Quests;
using TMPro;
using UnityEngine;

namespace Meta.UI.Quests.Widgets
{
    public class CollectingWidget : QuestWidget
    {
        [SerializeField] private TextMeshProUGUI _progressText;
        public override void SetStats(Quest quest)
        {
            base.SetStats(quest);

            _progressText.text = $"0/{((CollectingQuest)quest).targetCount}"; 
        }
    }
}
