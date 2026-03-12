using BepInEx;
using UnityEngine;
using ImprovedInput;

namespace RebindSkills;

[BepInPlugin("com.rebindskills.mod", "RebindSkills", "0.1.0")]
sealed class Plugin : BaseUnityPlugin
{
    private bool _isInit;

    // Fast Pole
    public static PlayerKeybind KeyFastPoleL;
    public static PlayerKeybind KeyFastPoleR;

    // Slide Spin
    public static PlayerKeybind KeySlideSpinL;
    public static PlayerKeybind KeySlideSpinR;

    // Extended Slide
    public static PlayerKeybind KeyExSlideL;
    public static PlayerKeybind KeyExSlideR;

    // Slide Pounce
    public static PlayerKeybind KeySlidePoL;
    public static PlayerKeybind KeySlidePoR;

    private bool[,] _wasPressed = new bool[4, 8];

    public void OnEnable()
    {
        On.RainWorld.OnModsInit += OnModsInit;

        try
        {
            KeyFastPoleL = PlayerKeybind.Register(
                "rebindskills:fastpole_l", "Rebind Skills", "Pole FL",
                KeyCode.None, KeyCode.None);
            KeyFastPoleR = PlayerKeybind.Register(
                "rebindskills:fastpole_r", "Rebind Skills", "Pole FR",
                KeyCode.None, KeyCode.None);

            KeySlideSpinL = PlayerKeybind.Register(
                "rebindskills:slidespin_l", "Rebind Skills", "Slide SL",
                KeyCode.None, KeyCode.None);
            KeySlideSpinR = PlayerKeybind.Register(
                "rebindskills:slidespin_r", "Rebind Skills", "Slide SR",
                KeyCode.None, KeyCode.None);

            KeyExSlideL = PlayerKeybind.Register(
                "rebindskills:extendedslide_l", "Rebind Skills", "Slide EXL",
                KeyCode.None, KeyCode.None);
            KeyExSlideR = PlayerKeybind.Register(
                "rebindskills:extendedslide_r", "Rebind Skills", "Slide EXR",
                KeyCode.None, KeyCode.None);

            KeySlidePoL = PlayerKeybind.Register(
                "rebindskills:slidepo_l", "Rebind Skills", "Slide PL",
                KeyCode.None, KeyCode.None);
            KeySlidePoR = PlayerKeybind.Register(
                "rebindskills:slidepo_r", "Rebind Skills", "Slide PR",
                KeyCode.None, KeyCode.None);
        }
        catch
        {
            Debug.LogWarning("[RebindSkills] Improved Input Config no encontrado.");
        }
    }

    private void OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
    {
        orig(self);
        if (_isInit) return;
        _isInit = true;

        On.RWInput.PlayerInputLogic_int_int += (orig, cat, num) =>
        {
            var result = orig(cat, num);
            if (cat != 0) return result;
            return SkillManager.UpdateInput(result, num);
        };
    }

    private bool JustPressed(PlayerKeybind key, int player, int slot)
    {
        if (key == null) return false;
        bool current = key.CheckRawPressed(player);
        bool triggered = current && !_wasPressed[player, slot];
        _wasPressed[player, slot] = current;
        return triggered;
    }

    private void Update()
    {
        if (!_isInit) return;

        if (RWCustom.Custom.rainWorld?.processManager?.currentMainLoop is not RainWorldGame game || game.paused)
            return;

        for (int i = 0; i < 4; i++)
        {
            // Fast Pole
            if (JustPressed(KeyFastPoleL, i, 0))
                SkillManager.StartSkill(new SkillFastPole(-1), i);
            else if (JustPressed(KeyFastPoleR, i, 1))
                SkillManager.StartSkill(new SkillFastPole(1), i);

            // Slide Spin
            if (JustPressed(KeySlideSpinL, i, 2))
                SkillManager.StartSkill(new SkillSlideSpin(-1), i);
            else if (JustPressed(KeySlideSpinR, i, 3))
                SkillManager.StartSkill(new SkillSlideSpin(1), i);

            // Extended Slide
            if (JustPressed(KeyExSlideL, i, 4))
                SkillManager.StartSkill(new SkillExSlide(-1), i);
            else if (JustPressed(KeyExSlideR, i, 5))
                SkillManager.StartSkill(new SkillExSlide(1), i);

            // Slide Pounce
            if (JustPressed(KeySlidePoL, i, 6))
                SkillManager.StartSkill(new SkillSlidePo(-1), i);
            else if (JustPressed(KeySlidePoR, i, 7))
                SkillManager.StartSkill(new SkillSlidePo(1), i);
        }
    }
}