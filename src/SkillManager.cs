using UnityEngine;

namespace RebindSkills
{
    public static class SkillManager
    {
        private const int MAX_PLAYERS = 4;

        private static ISkill[] _currentSkill = new ISkill[MAX_PLAYERS];
        private static int[] _counter = new int[MAX_PLAYERS];
        private static int[] _lastInputX = new int[MAX_PLAYERS];

        static SkillManager()
        {
            for (int i = 0; i < MAX_PLAYERS; i++)
                _counter[i] = -1;
        }

        public static void StartSkill(ISkill Skill, int playerNumber)
        {
            if (playerNumber < 0 || playerNumber >= MAX_PLAYERS) return;

            if (_currentSkill[playerNumber] != null &&
                _currentSkill[playerNumber].GetType() == Skill.GetType())
                return;

            _currentSkill[playerNumber] = Skill;
            _counter[playerNumber] = 0;

            _lastInputX[playerNumber] = Skill.Direction * -1;
            
            Debug.Log($"[RebindSkills] START P{playerNumber}: {Skill.GetType().Name}");
        }

        public static void StopSkill(int playerNumber)
        {
            if (playerNumber < 0 || playerNumber >= MAX_PLAYERS) return;
            _currentSkill[playerNumber] = null;
            _counter[playerNumber] = -1;
        }

        public static Player.InputPackage UpdateInput(Player.InputPackage input, int playerNumber)
        {
            if (playerNumber < 0 || playerNumber >= MAX_PLAYERS) return input;
            if (_currentSkill[playerNumber] == null || _counter[playerNumber] < 0) return input;

            int dir = _currentSkill[playerNumber].Direction;

            if (_counter[playerNumber] > 0 && dir != 0)
            {
                bool justPressedOpposite = (input.x == -dir) && (_lastInputX[playerNumber] != -dir);

                if (justPressedOpposite)
                {
                    Debug.Log($"[RebindSkills] CANCELLED P{playerNumber} (reconfirmación)");
                    StopSkill(playerNumber);
                    _lastInputX[playerNumber] = input.x;
                    return input;
                }
            }

            _lastInputX[playerNumber] = input.x;

            var result = _currentSkill[playerNumber].Execute(_counter[playerNumber], input);

            result.jmp  |= input.jmp;
            result.thrw |= input.thrw;
            result.pckp |= input.pckp;
            result.spec |= input.spec;

            _counter[playerNumber]++;

            if (_counter[playerNumber] >= _currentSkill[playerNumber].MaxFrames)
            {
                Debug.Log($"[RebindSkills] FINISH P{playerNumber}");
                StopSkill(playerNumber);
            }

            return result;
        }
    }
}