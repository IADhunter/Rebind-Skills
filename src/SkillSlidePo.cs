namespace RebindSkills
{
    public class SkillSlidePo : ISkill
    {
        private int _dir;
        public int Direction => _dir;
        public int MaxFrames => 33;

        public SkillSlidePo(int direction) => _dir = direction;

        public Player.InputPackage Execute(int frame, Player.InputPackage input)
        {
            if (frame <= 5)
            {
                input.x = _dir;
                input.y = -1;
                input.downDiagonal = _dir;
                input.jmp = true;
            }
            else if (frame <= 13)
            {
                input.x = _dir;
                input.y = -1;
                input.downDiagonal = _dir;
                input.jmp = false;
            }
            else if (frame == 14)
            {
                input.x = _dir;
                input.y = 0;
                input.downDiagonal = 0;
                input.jmp = true;
            }
            else if (frame == 15)
            {
                input.x = _dir;
                input.y = 1;
                input.downDiagonal = 0;
                input.jmp = true;
            }
            else if (frame <= 19)
            {
                input.x = 0;
                input.y = 1;
                input.downDiagonal = 0;
                input.jmp = true;
            }
            else if (frame <= 31)
            {
                input.x = 0;
                input.y = 1;
                input.downDiagonal = 0;
                input.jmp = false;
            }
            else
            {
                input.x = 0;
                input.y = 0;
                input.jmp = false;
            }

            return input;
        }
    }
}