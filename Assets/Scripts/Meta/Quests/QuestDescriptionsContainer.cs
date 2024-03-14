using Meta.Quests.Core.Generation;
using Meta.Quests.Core.Generation.Abstract;
using Meta.Quests.Core.Quests;
using System.Linq;
using UnityEngine;

namespace Meta.Quests
{
    [CreateAssetMenu(menuName = "Meta/Quests/DescriptionsContainer")]
    public class QuestDescriptionsContainer : ScriptableObject, IQuestDescriptionContainer
    {
        [SerializeField] private QuestDescriptionData[] _descriptions;

        public QuestDescriptionData[] GetDescriptions(QuestType questType)
        {
            return _descriptions.Where(x => x.questType == questType).ToArray();
        }
    }
}