using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vorpclothingstore_cl.Utils;

namespace vorpclothingstore_cl
{
    public class vorpclothingstore : BaseScript
    {
        public static List<int> StoreBlips = new List<int>();
        public static List<int> StorePeds = new List<int>();
        public vorpclothingstore()
        {
            Tick += onStore;
        }

        public static async Task InitStores()
        {
            await Delay(10000);
            string ped = "S_M_M_Tailor_01";
            uint HashPed = (uint)API.GetHashKey(ped);
            await Miscellanea.LoadModel(HashPed);

            foreach (var store in GetConfig.Config["Stores"])
            {
                int blipIcon = int.Parse(store["BlipIcon"].ToString());
                float x = float.Parse(store["EnterStore"][0].ToString());
                float y = float.Parse(store["EnterStore"][1].ToString());
                float z = float.Parse(store["EnterStore"][2].ToString());
                float Pedx = float.Parse(store["NPCStore"][0].ToString());
                float Pedy = float.Parse(store["NPCStore"][1].ToString());
                float Pedz = float.Parse(store["NPCStore"][2].ToString());
                float Pedheading = float.Parse(store["NPCStore"][3].ToString());

                int _blip = Function.Call<int>((Hash)0x554D9D53F696D002, 1664425300, x, y, z);
                Function.Call((Hash)0x74F74D3207ED525C, _blip, blipIcon, 1);
                Function.Call((Hash)0x9CB1A1623062F402, _blip, store["name"].ToString());
                StoreBlips.Add(_blip);

                int _PedShop = API.CreatePed(HashPed, Pedx, Pedy, Pedz, Pedheading, false, true, true, true);
                Function.Call((Hash)0x283978A15512B2FE, _PedShop, true);
                StorePeds.Add(_PedShop);
                API.SetEntityNoCollisionEntity(API.PlayerPedId(), _PedShop, false);
                API.SetEntityCanBeDamaged(_PedShop, false);
                API.SetEntityInvincible(_PedShop, true);
                await Delay(1000);
                API.FreezeEntityPosition(_PedShop, true);

                await Delay(100);
            }


        }

        [Tick]
        private async Task onStore()
        {
            if (StorePeds.Count() == 0) { return; }

            int pid = API.PlayerPedId();
            Vector3 pCoords = API.GetEntityCoords(pid, true, true);

            for (int i = 0; i < GetConfig.Config["Stores"].Count(); i++)
            {
                float x = float.Parse(GetConfig.Config["Stores"][i]["EnterStore"][0].ToString());
                float y = float.Parse(GetConfig.Config["Stores"][i]["EnterStore"][1].ToString());
                float z = float.Parse(GetConfig.Config["Stores"][i]["EnterStore"][2].ToString());
                float radius = float.Parse(GetConfig.Config["Stores"][i]["EnterStore"][3].ToString());

                if (API.GetDistanceBetweenCoords(pCoords.X, pCoords.Y, pCoords.Z, x, y, z, false) <= radius)
                {
                    await DrawTxt(GetConfig.Langs["PressToOpen"], 0.5f, 0.9f, 0.7f, 0.7f, 255, 255, 255, 255, true, true);
                    if (API.IsControlJustPressed(2, 0xD9D0E1C0))
                    {
                        await Commands.MoveToCoords(i);
                    }
                }

            }

        }

        public async Task DrawTxt(string text, float x, float y, float fontscale, float fontsize, int r, int g, int b, int alpha, bool textcentred, bool shadow)
        {
            long str = Function.Call<long>(Hash._CREATE_VAR_STRING, 10, "LITERAL_STRING", text);
            Function.Call(Hash.SET_TEXT_SCALE, fontscale, fontsize);
            Function.Call(Hash._SET_TEXT_COLOR, r, g, b, alpha);
            Function.Call(Hash.SET_TEXT_CENTRE, textcentred);
            if (shadow) { Function.Call(Hash.SET_TEXT_DROPSHADOW, 1, 0, 0, 255); }
            Function.Call(Hash.SET_TEXT_FONT_FOR_CURRENT_COMMAND, 1);
            Function.Call(Hash._DISPLAY_TEXT, str, x, y);
        }

    }
}
