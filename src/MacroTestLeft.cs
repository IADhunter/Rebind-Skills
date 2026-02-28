namespace RebindSkills
{
    public class MacroTestLeft : IMacro
    {
        public int MaxFrames => 40; // 1 segundo aproximadamente

        public Player.InputPackage Execute(int frame, Player.InputPackage input)
        {
            input.x = -1; // Forzamos ir a la izquierda
            return input;
        }
    }
}