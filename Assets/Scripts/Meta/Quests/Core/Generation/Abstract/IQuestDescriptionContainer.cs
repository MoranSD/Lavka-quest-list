using Meta.Quests.Core.Quests;

namespace Meta.Quests.Core.Generation.Abstract
{
    public interface IQuestDescriptionContainer
    {
        public QuestDescriptionData[] GetDescriptions(QuestType questType);
    }
}