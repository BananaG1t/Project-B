public class RoleLevelModel
{
    public Int64 Id { get; set; }
    public string Functionalty { get; set; }
    public Int64 LevelNeeded { get; set; }

    public RoleLevelModel(Int64 id, string functionality, Int64 level_Needed)
    {
        Id = id;
        Functionalty = functionality;
        LevelNeeded = level_Needed;
    }

    public RoleLevelModel(string functionalty, int level_Needed)
    {
        Functionalty = functionalty;
        LevelNeeded = level_Needed;
        Id = RoleLevelAccess.Write(this);
    }
}