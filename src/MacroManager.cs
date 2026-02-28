using UnityEngine;

namespace RebindSkills
{
    public static class MacroManager
    {
        public static IMacro CurrentMacro { get; private set; }
        public static int Counter { get; private set; } = -1;
        private static int lastFrameProcessed = -1;

        public static void StartMacro(IMacro macro)
        {
            CurrentMacro = macro;
            Counter = 0;
            lastFrameProcessed = Time.frameCount;
            Debug.Log($"[RebindSkills] START: {macro.GetType().Name}");
        }

        public static void StopMacro()
        {
            CurrentMacro = null;
            Counter = -1;
        }

        public static Player.InputPackage UpdateInput(Player.InputPackage input, int playerNumber)
        {
            if (playerNumber != 0 || CurrentMacro == null || Counter < 0)
                return input;

            var result = CurrentMacro.Execute(Counter, input);

            if (Time.frameCount != lastFrameProcessed)
            {
                lastFrameProcessed = Time.frameCount;
                Counter++;

                if (Counter >= CurrentMacro.MaxFrames)
                {
                    StopMacro();
                }
            }

            return result;
        }
    }
}