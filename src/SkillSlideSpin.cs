namespace RebindSkills
{
    public class SkillSlideSpin : ISkill
    {
        private int _dir;
        public int Direction => _dir;
        public int MaxFrames => 22;

        public SkillSlideSpin(int direction) => _dir = direction;

        public Player.InputPackage Execute(int frame, Player.InputPackage input)
        {
            if (frame <= 2)
            {
                input.x = _dir;
                input.y = -1;
                input.downDiagonal = _dir;
                input.jmp = true;
            }
            else if (frame <= 7)
            {
                input.x = _dir;
                input.y = -1;
                input.downDiagonal = _dir;
                input.jmp = false;
            }
            else if (frame <= 13)
            {
                input.x = _dir;
                input.y = 0;
                input.downDiagonal = 0;
                input.jmp = false;
            }
            else if (frame == 14)
            {
                input.x = 0;
                input.y = 0;
                input.downDiagonal = 0;
                input.jmp = false;
            }
            else if (frame <= 16)
            {
                input.x = -_dir;
                input.y = 0;
                input.downDiagonal = 0;
                input.jmp = true;
            }
            else
            {
                input.x = -_dir;
                input.y = 0;
                input.downDiagonal = 0;
                input.jmp = false;
            }

            return input;
        }
    }
}