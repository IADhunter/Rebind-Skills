using BepInEx;
using UnityEngine;

namespace RebindSkills;

[BepInPlugin("com.rebindskills.mod", "RebindSkills", "0.1.0")]
sealed class Plugin : BaseUnityPlugin
{
    private bool _isInit;

    public void OnEnable() => On.RainWorld.OnModsInit += OnModsInit;

    private void OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
    {
        orig(self);
        if (_isInit) return;
        _isInit = true;

        // EL MOTOR: Sigue siendo el Hook a RWInput para máxima precisión física
        On.RWInput.PlayerInputLogic_int_int += (orig, cat, num) => 
            MacroManager.UpdateInput(orig(cat, num), num);
    }

    // Usamos el Update de BepInEx (BaseUnityPlugin)
    // Este corre SIEMPRE, 60 veces por segundo, sin fallar
    private void Update()
    {
        if (!_isInit) return;

        if (Input.GetKeyDown(KeyCode.U))
            MacroManager.StartMacro(new MacroFastPole(-1));
        
        else if (Input.GetKeyDown(KeyCode.I))
            MacroManager.StartMacro(new MacroFastPole(1));
        
        if (Input.GetKeyDown(KeyCode.K))
           MacroManager.StartMacro(new MacroSlideSpin(-1)); // Slide-Spin Izquierda

        else if (Input.GetKeyDown(KeyCode.L))
            MacroManager.StartMacro(new MacroSlideSpin(1));  // Slide-Spin Derecha
    }
}