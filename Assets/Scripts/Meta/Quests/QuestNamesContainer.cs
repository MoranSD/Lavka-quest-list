using Meta.Quests.Core.Generation.Abstract;
using Meta.Quests.Core.Quests;
using System.Linq;
using UnityEngine;

namespace Meta.Quests
{
    [CreateAssetMenu(menuName = "Meta/Quests/NamesContainer")]
    public class QuestNamesContainer : ScriptableObject, IQuestNamesContainer
    {
        [SerializeField] QuestName[] _names;

        public string[] GetNames(QuestType questType)
        {
            return _names.Where(x => x.questType == questType).Select(x => x.name).ToArray();
        }

        [System.Serializable]
        private struct QuestName
        {
            public QuestType questType;
            public string name;
        }
    }
}
