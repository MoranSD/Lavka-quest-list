using Meta.Quests.Core.Quests;

namespace Meta.Quests.Core.Generation
{
    [System.Serializable]
    public struct QuestDescriptionData
    {
        public QuestType questType;
        public string descriptionFormat;

        public string GenerateDescription() => GenerateDescription(null);
        public string GenerateDescription(string[] insertArguments)
        {
            return insertArguments == null ? descriptionFormat : string.Format(descriptionFormat, insertArguments);
        }
    }
}