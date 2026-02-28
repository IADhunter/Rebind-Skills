namespace RebindSkills
{
    public class MacroFastPole : IMacro
    {
        private int _dir;
        public int MaxFrames => 7;

        public MacroFastPole(int direction) => _dir = direction;

        public Player.InputPackage Execute(int frame, Player.InputPackage input)
        {
            // Salto inicial — 1 frame
            if (frame == 0)
            {
                input.jmp = true;
            }
            // Pausa — 3 frames para resetear jmp
            else if (frame <= 3)
            {
                input.jmp = false;
                input.x = 0;
            }
            // Salto final + Dirección — frames 4, 5 y 6
            else
            {
                input.jmp = true;
                input.x = _dir;
            }

            return input;
        }
    }
}