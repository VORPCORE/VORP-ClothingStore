using CitizenFX.Core;
using CitizenFX.Core.Native;
using MenuAPI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace vorpclothingstore_cl.Utils
{
    class Commands : BaseScript
    {
        public static int PedShop = new int();
        public static JObject ClothesDB = new JObject();
        public static JObject SkinsDB = new JObject();
        public static double priceTotal = 0.0;
        public static string model = "";
        public static bool isBuy = false;
        public static int CamWardrove;
        public static int ActuallyShop;
        public static Dictionary<int, Tuple<string, string>> MyOutfits = new Dictionary<int, Tuple<string, string>>();

        public Commands()
        {
            RegisterCommand("cc", new Action(getCamCoords), false);
            EventHandlers[$"{GetCurrentResourceName()}:LoadYourCloths"] += new Action<string, string>(LoadYourCloths);
            EventHandlers[$"{GetCurrentResourceName()}:LoadYourOutfits"] += new Action<dynamic>(LoadYourOutfits);
            EventHandlers[$"vorpclothingstore:startBuyCloths"] += new Action<bool>(startBuyCloths); 
        }

        internal static async Task SetOutfit(int index)
        {
            TriggerServerEvent("vorpclothingstore:setOutfit", MyOutfits.ElementAt(index).Value.Item2);
            MenuController.CloseAllMenus();
            completeBuy(false);
        }

        public static Dictionary<string, object> clothesPlayer = new Dictionary<string, object>() {
            { "Hat", 0 },
            { "Mask", 0 },
            { "EyeWear", 0 },
            { "NeckWear", 0 },
            { "NeckTies", 0 },
            { "Shirt", 0 },
            { "Suspender", 0 },
            { "Vest", 0 },
            { "Coat", 0 },
            { "Poncho", 0 },
            { "Cloak", 0 },
            { "Glove", 0 },
            { "RingRh", 0 },
            { "RingLh", 0 },
            { "Bracelet", 0 },
            { "Gunbelt", 0 },
            { "Belt", 0 },
            { "Buckle", 0 },
            { "Holster", 0 },
            { "Pant", 0 },
            { "Skirt", 0 },
            { "Chap", 0 },
            { "Boots", 0 },
            { "Spurs", 0 },
            { "CoatClosed", 0 }
        };

        private void LoadYourCloths(string cloths, string skins)
        {
            ClothesDB = JObject.Parse(cloths);
            SkinsDB = JObject.Parse(skins);

            foreach (var a in ClothesDB)
            {
                clothesPlayer[a.Key] = ConvertValue(a.Value.ToString());
            }
        }

        public static void DeleteOutfit(int currentIndex)
        {
            TriggerServerEvent("vorpclothingstore:deleteOutfit", MyOutfits.ElementAt(currentIndex).Key);
            MyOutfits.Remove(MyOutfits.ElementAt(currentIndex).Key);
        }

        private void LoadYourOutfits(dynamic outfits_db)
        {
            MyOutfits.Clear();
            foreach (var outf in outfits_db)
            {
                MyOutfits.Add(outf.id, new Tuple<string, string>(outf.title, outf.comps));
            }
        }

        public static uint ConvertValue(string s)
        {
            uint result;

            if (uint.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                int interesante = int.Parse(s);
                result = (uint)interesante;
                return result;
            }
        }

        public static async Task MoveToCoords(int shop)
        {
            ActuallyShop = shop;
            TriggerServerEvent("vorpclothingstore:getPlayerCloths");

            float Pedx = float.Parse(GetConfig.Config["Stores"][shop]["NPCStore"][0].ToString());
            float Pedy = float.Parse(GetConfig.Config["Stores"][shop]["NPCStore"][1].ToString());
            float Pedz = float.Parse(GetConfig.Config["Stores"][shop]["NPCStore"][2].ToString());
            float Pedheading = float.Parse(GetConfig.Config["Stores"][shop]["NPCStore"][3].ToString());
            float Doorx = float.Parse(GetConfig.Config["Stores"][shop]["DoorRoom"][0].ToString());
            float Doory = float.Parse(GetConfig.Config["Stores"][shop]["DoorRoom"][1].ToString());
            float Doorz = float.Parse(GetConfig.Config["Stores"][shop]["DoorRoom"][2].ToString());
            float Playerx = float.Parse(GetConfig.Config["Stores"][shop]["StoreRoom"][0].ToString());
            float Playery = float.Parse(GetConfig.Config["Stores"][shop]["StoreRoom"][1].ToString());
            float Playerz = float.Parse(GetConfig.Config["Stores"][shop]["StoreRoom"][2].ToString());
            float Playerheading = float.Parse(GetConfig.Config["Stores"][shop]["StoreRoom"][3].ToString());
            float Camerax = float.Parse(GetConfig.Config["Stores"][shop]["CameraMain"][0].ToString());
            float Cameray = float.Parse(GetConfig.Config["Stores"][shop]["CameraMain"][1].ToString());
            float Cameraz = float.Parse(GetConfig.Config["Stores"][shop]["CameraMain"][2].ToString());
            float CameraRotx = float.Parse(GetConfig.Config["Stores"][shop]["CameraMain"][3].ToString());
            float CameraRoty = float.Parse(GetConfig.Config["Stores"][shop]["CameraMain"][4].ToString());
            float CameraRotz = float.Parse(GetConfig.Config["Stores"][shop]["CameraMain"][5].ToString());

            TriggerEvent("vorp:setInstancePlayer", true);

            ClearPedTasksImmediately(vorpclothingstore.StorePeds[shop], 1, 1);
            FreezeEntityPosition(vorpclothingstore.StorePeds[shop], false);

            string ped = "S_M_M_Tailor_01";

            await Delay(1000);
            NetworkSetInSpectatorMode(true, PlayerPedId());

            int HashPed = GetHashKey(ped);
            int PedWardrobe = CreatePed((uint)HashPed, Doorx, Doory, Doorz, 0.0f, false, true, true, true);
            Function.Call((Hash)0x283978A15512B2FE, PedWardrobe, true);
            SetModelAsNoLongerNeeded((uint)HashPed);
            SetEntityAlpha(PedWardrobe, 0, true);
            await Delay(1000);
            TaskGoToEntity(vorpclothingstore.StorePeds[shop], PedWardrobe, 15000, 0.5f, 1.1f, 1.0f, 1);
            await Delay(4000);
            TaskGoToEntity(PlayerPedId(), PedWardrobe, 10000, 0.5f, 0.8f, 1.0f, 1);
            await Delay(6500);

            DoScreenFadeOut(1800);
            await Delay(2000);
            ClearPedTasksImmediately(PlayerPedId(), 1, 1);

            SetEntityCoords(PlayerPedId(), Playerx, Playery, Playerz, false, false, false, false);
            SetEntityHeading(PlayerPedId(), Playerheading);
            await Delay(100);
            FreezeEntityPosition(PlayerPedId(), true);
            SetEntityCoords(vorpclothingstore.StorePeds[shop], Pedx, Pedy, Pedz, false, false, false, false);
            SetEntityHeading(vorpclothingstore.StorePeds[shop], Pedheading);
            await Delay(2000);

            CamWardrove = CreateCamWithParams("DEFAULT_SCRIPTED_CAMERA", Camerax, Cameray, Cameraz, CameraRotx, CameraRoty, CameraRotz, 50.00f, false, 0);
            SetCamActive(CamWardrove, true);
            RenderScriptCams(true, true, 500, true, true, 0);
            FreezeEntityPosition(vorpclothingstore.StorePeds[shop], true);
            await Delay(1000);
            DoScreenFadeIn(1000);
            DeletePed(ref PedWardrobe);
            NetworkSetInSpectatorMode(false, PlayerPedId());
            model = SkinsDB["sex"].ToString();
            Menus.MainMenu.GetMenu().OpenMenu();

        }

        public static async void FinishBuy(bool buy, double cost)
        {
            if (buy)
            {
                bool saveOutfit = false;
                string outfitName = "";
                TriggerEvent("vorpinputs:getInput", GetConfig.Langs["ButtonNewOutfit"], GetConfig.Langs["PlaceHolderNewOutfit"], new Action<dynamic>((data) =>
                {
                    string result = data;
                    if (!result.StartsWith("close"))
                    {
                        outfitName = result.Trim();
                        saveOutfit = true;
                    }

                    string JsonCloths = Newtonsoft.Json.JsonConvert.SerializeObject(clothesPlayer);

                    TriggerServerEvent("vorpclothingstore:buyPlayerCloths", cost, JsonCloths, saveOutfit, outfitName);
                }));
            }
        }

        public static void startBuyCloths(bool completed)
        {
            completeBuy(completed);
        }

        public static async Task completeBuy(bool completedBuy)
        {
            if (!completedBuy)
            {
                await Delay(3000);
                if (MenuController.IsAnyMenuOpen())
                {
                    return;
                }
                else
                {
                    TriggerEvent("vorpcharacter:refreshPlayerSkin");
                }
            }
            else
            {
                TriggerServerEvent("vorpcharacter:getPlayerSkin");
            }

            float PedExitx = float.Parse(GetConfig.Config["Stores"][ActuallyShop]["ExitWardrobe"][0].ToString());
            float PedExity = float.Parse(GetConfig.Config["Stores"][ActuallyShop]["ExitWardrobe"][1].ToString());
            float PedExitz = float.Parse(GetConfig.Config["Stores"][ActuallyShop]["ExitWardrobe"][2].ToString());
            float PedExitheading = float.Parse(GetConfig.Config["Stores"][ActuallyShop]["ExitWardrobe"][3].ToString());

            DoScreenFadeOut(1000);
            await Delay(2000);

            SetCamActive(CamWardrove, false);
            RenderScriptCams(false, true, 1000, true, true, 0);
            await Delay(1000);
            DestroyCam(CamWardrove, true);
            FreezeEntityPosition(PlayerPedId(), false);

            SetEntityCoords(PlayerPedId(), PedExitx, PedExity, PedExitz, false, false, false, false);
            SetEntityHeading(PlayerPedId(), PedExitheading);


            TriggerEvent("vorp:setInstancePlayer", false);

            DoScreenFadeIn(1000);
        }

        public static void SetPlayerComponent(string model, int _newIndex, uint category, string idlist, List<uint> male_components, List<uint> female_components)
        {
            int pPID = PlayerPedId();
            if (model == "mp_male")
            {
                if (_newIndex == 0)
                {
                    //Coats is a really shit
                    if (category == 0xE06D30CE)
                    {
                        Function.Call((Hash)0xD710A5007C2AC539, pPID, 0x662AC34, 0);
                    }
                    //end
                    Function.Call((Hash)0xD710A5007C2AC539, pPID, category, 0);
                    Function.Call((Hash)0xCC8CA3E88256E58F, pPID, 0, 1, 1, 1, 0);
                    clothesPlayer[idlist] = -1;
                }
                else
                {
                    //Coats is a really shit
                    if (category == 0xE06D30CE)
                    {
                        Function.Call((Hash)0xD710A5007C2AC539, pPID, 0x662AC34, 0);
                        Function.Call((Hash)0xCC8CA3E88256E58F, pPID, 0, 1, 1, 1, 0);
                    }
                    //end
                    Function.Call((Hash)0x59BD177A1A48600A, pPID, category);
                    Function.Call((Hash)0xD3A7B003ED343FD9, pPID, male_components[_newIndex - 1], true, false, false);
                    Function.Call((Hash)0xD3A7B003ED343FD9, pPID, male_components[_newIndex - 1], true, true, false);
                    clothesPlayer[idlist] = male_components[_newIndex - 1];
                }
            }
            else
            {
                if (_newIndex == 0)
                {
                    Function.Call((Hash)0xD710A5007C2AC539, pPID, category, 0);
                    Function.Call((Hash)0xCC8CA3E88256E58F, pPID, 0, 1, 1, 1, 0);
                    clothesPlayer[idlist] = -1;
                }
                else
                {
                    //Miscellanea.LoadModel(male_components[_newIndex - 1]);
                    Function.Call((Hash)0x59BD177A1A48600A, pPID, category);
                    Function.Call((Hash)0xD3A7B003ED343FD9, pPID, female_components[_newIndex - 1], true, false, true);
                    Function.Call((Hash)0xD3A7B003ED343FD9, pPID, female_components[_newIndex - 1], true, true, true);
                    clothesPlayer[idlist] = female_components[_newIndex - 1];
                }
            }
            Function.Call((Hash)0xCC8CA3E88256E58F, pPID, 0, 1, 1, 1, false);
        }

        private void getCamCoords()
        {
            Vector3 cc = GetGameplayCamCoord();
            Vector3 ccr = GetGameplayCamRot(0);
            Debug.WriteLine(cc.ToString() + "\n" + ccr.ToString());
        }
    }
}
