using Meta.Quests.Core.Quests;

namespace Meta.Quests.Core.Generation.Abstract
{
    public abstract class BaseQuestGenerator<T> : IQuestGenerator<T> where T : Quest
    {
        protected abstract QuestType questType { get; }
        protected IQuestNamesContainer namesContainer;
        protected IQuestDescriptionContainer descriptionContainer;
        public BaseQuestGenerator(IQuestNamesContainer namesContainer, IQuestDescriptionContainer descriptionContainer)
        {
            this.namesContainer = namesContainer;
            this.descriptionContainer = descriptionContainer;
        }
        public abstract T Generate(int questId);
        protected void InsertName(Quest quest)
        {
            var names = namesContainer.GetNames(questType);
            int nameId = new System.Random().Next(0, names.Length);
            quest.name = names[nameId];
        }
        protected void InsertDescription(Quest quest, string insertArgument) => InsertDescription(quest, new string[] { insertArgument });
        protected void InsertDescription(Quest quest, string[] insertArguments = null)
        {
            var descriptions = descriptionContainer.GetDescriptions(questType);
            int descriptionId = new System.Random().Next(0, descriptions.Length);
            var descriptionData = descriptions[descriptionId];

            quest.description = descriptionData.GenerateDescription(insertArguments);
        }
    }
}
