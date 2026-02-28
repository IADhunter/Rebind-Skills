using UnityEngine;

namespace RebindSkills
{
    public class MacroSlideSpin : IMacro
    {
        private int _dir;
        public int MaxFrames => 32; // 800ms

        public MacroSlideSpin(int direction) => _dir = direction;

        public Player.InputPackage Execute(int frame, Player.InputPackage input)
        {
            // 1. EL ANCLA (Dirección constante)
            input.x = _dir;

            // 2. EL COMBO TÉCNICO (Abajo + DownDiagonal)
            // Lo mantenemos durante 400ms (16 frames)
            if (frame <= 16)
            {
                input.y = -1;
                input.downDiagonal = _dir; // Le decimos al juego: "Estamos en diagonal"
            }
            else
            {
                input.y = 0;
                input.downDiagonal = 0;
            }

            // 3. EL DISPARADOR (Salto con el delay de 3 frames que descubrimos)
            // Solo un toque rápido para activar el slide
            if (frame >= 3 && frame <= 6)
            {
                input.jmp = true;
            }
            else
            {
                input.jmp = false;
            }

            return input;
        }
    }
}