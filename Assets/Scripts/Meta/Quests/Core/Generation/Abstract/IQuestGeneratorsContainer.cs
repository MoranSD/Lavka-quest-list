using Meta.Quests.Core.Quests;

namespace Meta.Quests.Core.Generation.Abstract
{
    public interface IQuestGeneratorsContainer
    {
        public IQuestGenerator<T> GetGenerator<T>() where T : Quest;
    }
}