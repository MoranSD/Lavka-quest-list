using Meta.Quests.Core.Generation.Abstract;
using Meta.Quests.Core.Quests;
using Zenject;

namespace Meta.Quests
{
    public class QuestGeneratorsContainer : IQuestGeneratorsContainer
    {
        private DiContainer _container;
        public QuestGeneratorsContainer(DiContainer container)
        {
            _container = container;
        }
        public IQuestGenerator<T> GetGenerator<T>() where T : Quest
        {
            return _container.Resolve<IQuestGenerator<T>>();
        }
    }
}