public class RoleLevelModel
{
    public int Id { get; set; }
    public string Functionalty { get; set; }
    public int LevelNeeded { get; set; }

    public RoleLevelModel(Int64 id, string functionality, Int64 level_Needed)
    {
        Id = (int)id;
        Functionalty = functionality;
        LevelNeeded = (int)level_Needed;
    }

    public RoleLevelModel(string functionalty, int level_Needed)
    {
        Functionalty = functionalty;
        LevelNeeded = level_Needed;
        Id = RoleLevelAccess.Write(this);
    }
}