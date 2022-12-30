using HarmonyLib;
using Kitchen;
using KitchenMods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity.Entities;

namespace PasteSeed
{
    public class ClipboardInjectionSystem : GenericSystemBase, IModSystem
    {
        protected override void Initialise()
        {
            base.Initialise();
            Enabled=false;
            DoPatching();
        }
        protected override void OnUpdate()
        {

        }
        public static void DoPatching()
        {
            string t = "";
            var a = new Harmony("PasteSeed");
            a.Patch(typeof(TextInputView).GetMethod(nameof(TextInputView.RequestSeedInput)), new HarmonyMethod(SymbolExtensions.GetMethodInfo(() => PrefillSeed(ref t))));
        }

        public static void PrefillSeed(ref string text)
        {
            if (text != "")
                return;
            string clip = Clipboard.GetText();
            clip = clip.Trim().ToLower();
            if (clip.Length>8)
                return;
            if (!clip.All((c) => Seed.Characters.Contains(c)))
                return;
            text = clip;
        }
    }
}
