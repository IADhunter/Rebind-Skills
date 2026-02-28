namespace RebindSkills
{
    public interface IMacro
    {
        int MaxFrames { get; }
        Player.InputPackage Execute(int frame, Player.InputPackage input);
    }
}