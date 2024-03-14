using Meta.Quests.Core.Quests;

namespace Meta.Quests.Core.Generation.Abstract
{
    public interface IQuestNamesContainer
    {
        public string[] GetNames(QuestType questType);
    }
}