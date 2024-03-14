using Meta.Quests.Core.Quests;

namespace Meta.Quests.Core.Generation.Abstract
{
    public interface IQuestGenerator<T> where T : Quest
    {
        public T Generate(int questId);
    }
}