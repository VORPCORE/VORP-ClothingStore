using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;


namespace vorpclothingstore_cl.Utils
{
    class ClearCaches : BaseScript
    {
        public ClearCaches()
        {
            EventHandlers["onResourceStop"] += new Action<string>(OnResourceStop);
        }

        private void OnResourceStop(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            Debug.WriteLine($"{resourceName} cleared blips and NPC's.");

            foreach (int blip in vorpclothingstore.StoreBlips)
            {
                int _blip = blip;
                RemoveBlip(ref _blip);
            }

            foreach (int npc in vorpclothingstore.StorePeds)
            {
                int _ped = npc;
                DeletePed(ref _ped);
            }
        }

    }
}
