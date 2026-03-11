namespace RebindSkills
{
    public interface ISkill
    {
        int Direction { get; }
        
        int MaxFrames { get; }
        Player.InputPackage Execute(int frame, Player.InputPackage input);
    }
}