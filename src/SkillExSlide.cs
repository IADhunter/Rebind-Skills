namespace RebindSkills
{
    public class SkillExSlide : ISkill
    {
        private int _dir;
        public int Direction => _dir;
        public int MaxFrames => 20;

        public SkillExSlide(int direction) => _dir = direction;

        public Player.InputPackage Execute(int frame, Player.InputPackage input)
        {
            if (frame <= 3)
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
            else if (frame <= 15)
            {
                input.x = -_dir; 
                input.y = 0;
                input.thrw = true; 
                input.jmp = false;
            }
            else if (frame <= 17)
            {
                input.x = _dir;
                input.y = 0;
                input.thrw = false;
                input.jmp = false;
            }
            else
            {
                input.x = 0; 
                input.thrw = false;
                input.jmp = false;
            }

            return input;
        }
    }
}