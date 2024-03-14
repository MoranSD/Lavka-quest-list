using Meta.Quests.Core.Generation.Abstract;
using Meta.Quests.Core.Quests;

namespace Meta.Quests.Core.Generation
{
    public class QuestGeneratorProvider
    {
        private IQuestGeneratorsContainer _questGeneratorsContainer;
        public QuestGeneratorProvider(IQuestGeneratorsContainer generatorsContainer)
        {
            _questGeneratorsContainer = generatorsContainer;
        }
        public Quest GenerateQuest(QuestType type, int questId)
        {
            return type switch
            {
                QuestType.adventure => GenerateQuest<AdventureQuest>(questId),
                QuestType.collecting => GenerateQuest<CollectingQuest>(questId),
                _ => null,
            };
        }
        public Quest GenerateQuest<T>(int questId) where T : Quest
        {
            return _questGeneratorsContainer.GetGenerator<T>().Generate(questId);
        }
    }
}