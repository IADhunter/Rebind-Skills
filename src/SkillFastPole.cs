namespace RebindSkills
{
    public class SkillFastPole : ISkill
    {
        private int _dir;
        public int Direction => _dir;
        public int MaxFrames => 10;

        public SkillFastPole(int direction) => _dir = direction;

        public Player.InputPackage Execute(int frame, Player.InputPackage input)
        {
            if (frame <= 2)
            {
                input.jmp = true;
            }
            else if (frame <= 5)
            {
                input.jmp = false;
                input.x = 0;
            }
            else
            {
                input.jmp = true;
                input.x = _dir;
            }

            return input;
        }
    }
}