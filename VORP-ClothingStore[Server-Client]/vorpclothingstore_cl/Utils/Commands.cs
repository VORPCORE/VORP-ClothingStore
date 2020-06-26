using CitizenFX.Core;
using CitizenFX.Core.Native;
using MenuAPI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace vorpclothingstore_cl.Utils
{
    class Commands : BaseScript
    {
        public static int PedShop = new int();
        public static JObject ClothesDB = new JObject();
        public static JObject SkinsDB = new JObject();
        private static double priceTotal = 0.0;
        private static bool isBuy = false;
        private static int CamWardrove;
        private static int ActuallyShop;

        public Commands()
        {
            RegisterCommand("cc", new Action(getCamCoords), false);
            EventHandlers[$"{GetCurrentResourceName()}:LoadYourCloths"] += new Action<string, string>(LoadYourCloths);

            EventHandlers[$"vorpclothingstore:startBuyCloths"] += new Action<bool>(startBuyCloths);
        }

        static Dictionary<string, object> clothesPlayer = new Dictionary<string, object>() {
            { "Hat", 0 },
            { "EyeWear", 0 },
            { "NeckWear", 0 },
            { "NeckTies", 0 },
            { "Shirt", 0 }, // oh shit this need a refactor
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
            MenuDressUpCharacter(SkinsDB["sex"].ToString());

        }

        public static void MenuDressUpCharacter(string model)
        {
            MenuController.Menus.Clear();
            //Definimos el nombre y subtitlo del menu con un constructor
            Menu mdu = new Menu(GetConfig.Langs["TitleMenuClothes"], GetConfig.Langs["SubTitleMenuClothes"]);
            MenuController.AddMenu(mdu);
            MenuController.MenuToggleKey = (Control)0;
            isBuy = false;

            List<string> hatType = new List<string>();
            hatType.Add(GetConfig.Langs["NoHatsValue"]);
            int indexHat = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.HATS_MALE.Count + 1; i++)
                {
                    hatType.Add(GetConfig.Langs["HatsValue"] + i);
                }
                if (ClothesUtils.HATS_MALE.IndexOf(ConvertValue(ClothesDB["Hat"].ToString())) != -1)
                {
                    indexHat = ClothesUtils.HATS_MALE.IndexOf(ConvertValue(ClothesDB["Hat"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.HATS_FEMALE.Count + 1; i++)
                {
                    hatType.Add(GetConfig.Langs["HatsValue"] + i);
                }
                if (ClothesUtils.HATS_FEMALE.IndexOf(ConvertValue(ClothesDB["Hat"].ToString())) != -1)
                {
                    indexHat = ClothesUtils.HATS_MALE.IndexOf(ConvertValue(ClothesDB["Hat"].ToString())) + 1;
                }
            }
            MenuListItem mListHats = new MenuListItem(GetConfig.Langs["Hats"], hatType, indexHat, GetConfig.Langs["HatsDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListHats); // Lo añadimos al menu

            List<string> eyeWearType = new List<string>();
            eyeWearType.Add(GetConfig.Langs["NoGlassesValue"]);
            int indexEyeWears = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.EYEWEAR_MALE.Count + 1; i++)
                {
                    eyeWearType.Add(GetConfig.Langs["GlassesValue"] + i);
                }
                if (ClothesUtils.EYEWEAR_MALE.IndexOf(ConvertValue(ClothesDB["EyeWear"].ToString())) != -1)
                {
                    indexEyeWears = ClothesUtils.EYEWEAR_MALE.IndexOf(ConvertValue(ClothesDB["EyeWear"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.EYEWEAR_FEMALE.Count + 1; i++)
                {
                    eyeWearType.Add(GetConfig.Langs["GlassesValue"] + i);
                }
                if (ClothesUtils.EYEWEAR_FEMALE.IndexOf(ConvertValue(ClothesDB["EyeWear"].ToString())) != -1)
                {
                    indexEyeWears = ClothesUtils.EYEWEAR_FEMALE.IndexOf(ConvertValue(ClothesDB["EyeWear"].ToString())) + 1;
                }
            }
            MenuListItem mListEyeWear = new MenuListItem(GetConfig.Langs["Glasses"], eyeWearType, indexEyeWears, GetConfig.Langs["GlassesDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListEyeWear); // Lo añadimos al menu

            List<string> neckWearType = new List<string>();
            neckWearType.Add(GetConfig.Langs["NoNeckwearValue"]);
            int indexNeckWear = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.NECKWEAR_MALE.Count + 1; i++)
                {
                    neckWearType.Add(GetConfig.Langs["NeckwearValue"] + i);
                }
                if (ClothesUtils.NECKWEAR_MALE.IndexOf(ConvertValue(ClothesDB["NeckWear"].ToString())) != -1)
                {
                    indexNeckWear = ClothesUtils.NECKWEAR_MALE.IndexOf(ConvertValue(ClothesDB["NeckWear"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.NECKWEAR_FEMALE.Count + 1; i++)
                {
                    neckWearType.Add(GetConfig.Langs["NeckwearValue"] + i);
                }
                if (ClothesUtils.NECKWEAR_FEMALE.IndexOf(ConvertValue(ClothesDB["NeckWear"].ToString())) != -1)
                {
                    indexNeckWear = ClothesUtils.NECKWEAR_FEMALE.IndexOf(ConvertValue(ClothesDB["NeckWear"].ToString())) + 1;
                }
            }
            MenuListItem mListNeckWear = new MenuListItem(GetConfig.Langs["Neckwear"], neckWearType, indexNeckWear, GetConfig.Langs["NeckwearDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListNeckWear); // Lo añadimos al menu

            List<string> maskType = new List<string>();
            maskType.Add(GetConfig.Langs["NoMaskValue"]);
            int indexMaskType = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.MASK_MALE.Count + 1; i++)
                {
                    maskType.Add(GetConfig.Langs["MaskValue"] + i);
                }
                if (ClothesUtils.MASK_MALE.IndexOf(ConvertValue(ClothesDB["Mask"].ToString())) != -1)
                {
                    indexMaskType = ClothesUtils.MASK_MALE.IndexOf(ConvertValue(ClothesDB["Mask"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.MASK_FEMALE.Count + 1; i++)
                {
                    maskType.Add(GetConfig.Langs["MaskValue"] + i);
                }
                if (ClothesUtils.MASK_FEMALE.IndexOf(ConvertValue(ClothesDB["Mask"].ToString())) != -1)
                {
                    indexMaskType = ClothesUtils.MASK_FEMALE.IndexOf(ConvertValue(ClothesDB["Mask"].ToString())) + 1;
                }
            }
            MenuListItem mListMask = new MenuListItem(GetConfig.Langs["Masks"], maskType, indexMaskType, GetConfig.Langs["MasksDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListMask); // Lo añadimos al menu

            List<string> neckTiesType = new List<string>();
            neckTiesType.Add(GetConfig.Langs["NoTiesValue"]);
            int indexNeckTies = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.NECKTIES_MALE.Count + 1; i++)
                {
                    neckTiesType.Add(GetConfig.Langs["TiesValue"] + i);
                }
                if (ClothesUtils.NECKTIES_MALE.IndexOf(ConvertValue(ClothesDB["NeckTies"].ToString())) != -1)
                {
                    indexNeckTies = ClothesUtils.NECKTIES_MALE.IndexOf(ConvertValue(ClothesDB["NeckTies"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.NECKTIES_FEMALE.Count + 1; i++)
                {
                    neckTiesType.Add(GetConfig.Langs["TiesValue"] + i);
                }
                if (ClothesUtils.NECKTIES_FEMALE.IndexOf(ConvertValue(ClothesDB["NeckTies"].ToString())) != -1)
                {
                    indexNeckTies = ClothesUtils.NECKTIES_FEMALE.IndexOf(ConvertValue(ClothesDB["NeckTies"].ToString())) + 1;
                }
            }
            MenuListItem mListNeckTies = new MenuListItem(GetConfig.Langs["Ties"], neckTiesType, indexNeckTies, GetConfig.Langs["TiesDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListNeckTies); // Lo añadimos al menu

            List<string> shirtsType = new List<string>();
            shirtsType.Add(GetConfig.Langs["NoShirtsValue"]);
            int indexShirt = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.SHIRTS_MALE.Count + 1; i++)
                {
                    shirtsType.Add(GetConfig.Langs["ShirtsValue"] + i);
                }
                if (ClothesUtils.SHIRTS_MALE.IndexOf(ConvertValue(ClothesDB["Shirt"].ToString())) != -1)
                {
                    indexShirt = ClothesUtils.SHIRTS_MALE.IndexOf(ConvertValue(ClothesDB["Shirt"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.SHIRTS_FEMALE.Count + 1; i++)
                {
                    shirtsType.Add(GetConfig.Langs["ShirtsValue"] + i);
                }
                if (ClothesUtils.SHIRTS_FEMALE.IndexOf(ConvertValue(ClothesDB["Shirt"].ToString())) != -1)
                {
                    indexShirt = ClothesUtils.SHIRTS_FEMALE.IndexOf(ConvertValue(ClothesDB["Shirt"].ToString())) + 1;
                }
            }
            MenuListItem mListShirts = new MenuListItem(GetConfig.Langs["Shirts"], shirtsType, indexShirt, GetConfig.Langs["ShirtsDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListShirts); // Lo añadimos al menu

            List<string> suspendersType = new List<string>();
            suspendersType.Add(GetConfig.Langs["NoSuspendersValue"]);
            int indexSuspender = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.SUSPENDERS_MALE.Count + 1; i++)
                {
                    suspendersType.Add(GetConfig.Langs["SuspendersValue"] + i);
                }
                if (ClothesUtils.SUSPENDERS_MALE.IndexOf(ConvertValue(ClothesDB["Suspender"].ToString())) != -1)
                {
                    indexSuspender = ClothesUtils.SUSPENDERS_MALE.IndexOf(ConvertValue(ClothesDB["Suspender"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.SUSPENDERS_FEMALE.Count + 1; i++)
                {
                    suspendersType.Add(GetConfig.Langs["SuspendersValue"] + i);
                }
                if (ClothesUtils.SUSPENDERS_FEMALE.IndexOf(ConvertValue(ClothesDB["Suspender"].ToString())) != -1)
                {
                    indexSuspender = ClothesUtils.SUSPENDERS_FEMALE.IndexOf(ConvertValue(ClothesDB["Suspender"].ToString())) + 1;
                }
            }
            MenuListItem mListSuspenders = new MenuListItem(GetConfig.Langs["Suspenders"], suspendersType, indexSuspender, GetConfig.Langs["SuspendersDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListSuspenders); // Lo añadimos al menu

            List<string> vestType = new List<string>();
            vestType.Add(GetConfig.Langs["NoVestsValue"]);
            int indexVest = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.VEST_MALE.Count + 1; i++)
                {
                    vestType.Add(GetConfig.Langs["VestsValue"] + i);
                }
                if (ClothesUtils.VEST_MALE.IndexOf(ConvertValue(ClothesDB["Vest"].ToString())) != -1)
                {
                    indexVest = ClothesUtils.VEST_MALE.IndexOf(ConvertValue(ClothesDB["Vest"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.VEST_FEMALE.Count + 1; i++)
                {
                    vestType.Add(GetConfig.Langs["VestsValue"] + i);
                }
                if (ClothesUtils.VEST_FEMALE.IndexOf(ConvertValue(ClothesDB["Vest"].ToString())) != -1)
                {
                    indexVest = ClothesUtils.VEST_FEMALE.IndexOf(ConvertValue(ClothesDB["Vest"].ToString())) + 1;
                }
            }
            MenuListItem mListVest = new MenuListItem(GetConfig.Langs["Vests"], vestType, indexVest, GetConfig.Langs["VestsDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListVest); // Lo añadimos al menu

            List<string> coatsType = new List<string>();
            coatsType.Add(GetConfig.Langs["NoCoatsValue"]);
            int indexCoat = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.COATS_MALE.Count + 1; i++)
                {
                    coatsType.Add(GetConfig.Langs["CoatsValue"] + i);
                }
                if (ClothesUtils.COATS_MALE.IndexOf(ConvertValue(ClothesDB["Coat"].ToString())) != -1)
                {
                    indexCoat = ClothesUtils.COATS_MALE.IndexOf(ConvertValue(ClothesDB["Coat"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.COATS_FEMALE.Count + 1; i++)
                {
                    coatsType.Add(GetConfig.Langs["CoatsValue"] + i);
                }
                if (ClothesUtils.COATS_FEMALE.IndexOf(ConvertValue(ClothesDB["Coat"].ToString())) != -1)
                {
                    indexCoat = ClothesUtils.COATS_FEMALE.IndexOf(ConvertValue(ClothesDB["Coat"].ToString())) + 1;
                }
            }
            MenuListItem mListCoats = new MenuListItem(GetConfig.Langs["Coats"], coatsType, indexCoat, GetConfig.Langs["CoatsDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListCoats); // Lo añadimos al menu

            List<string> ponchosType = new List<string>();
            ponchosType.Add(GetConfig.Langs["NoPonchosValue"]);
            int indexPoncho = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.PONCHOS_MALE.Count + 1; i++)
                {
                    ponchosType.Add(GetConfig.Langs["PonchosValue"] + i);
                }
                if (ClothesUtils.PONCHOS_MALE.IndexOf(ConvertValue(ClothesDB["Poncho"].ToString())) != -1)
                {
                    indexPoncho = ClothesUtils.PONCHOS_MALE.IndexOf(ConvertValue(ClothesDB["Poncho"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.PONCHOS_FEMALE.Count + 1; i++)
                {
                    ponchosType.Add(GetConfig.Langs["PonchosValue"] + i);
                }
                if (ClothesUtils.PONCHOS_FEMALE.IndexOf(ConvertValue(ClothesDB["Poncho"].ToString())) != -1)
                {
                    indexPoncho = ClothesUtils.PONCHOS_FEMALE.IndexOf(ConvertValue(ClothesDB["Poncho"].ToString())) + 1;
                }
            }
            MenuListItem mListPonchos = new MenuListItem(GetConfig.Langs["Ponchos"], ponchosType, indexPoncho, GetConfig.Langs["PonchosDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListPonchos); // Lo añadimos al menu

            List<string> cloakType = new List<string>();
            cloakType.Add(GetConfig.Langs["NoCloaksValue"]);
            int indexCloak = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.CLOAK_MALE.Count + 1; i++)
                {
                    cloakType.Add(GetConfig.Langs["CloaksValue"] + i);
                }
                if (ClothesUtils.CLOAK_MALE.IndexOf(ConvertValue(ClothesDB["Cloak"].ToString())) != -1)
                {
                    indexCloak = ClothesUtils.CLOAK_MALE.IndexOf(ConvertValue(ClothesDB["Cloak"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.CLOAK_FEMALE.Count + 1; i++)
                {
                    cloakType.Add(GetConfig.Langs["CloaksValue"] + i);
                }
                if (ClothesUtils.CLOAK_FEMALE.IndexOf(ConvertValue(ClothesDB["Cloak"].ToString())) != -1)
                {
                    indexCloak = ClothesUtils.CLOAK_FEMALE.IndexOf(ConvertValue(ClothesDB["Cloak"].ToString())) + 1;
                }
            }
            MenuListItem mListCloak = new MenuListItem(GetConfig.Langs["Cloaks"], cloakType, indexCloak, GetConfig.Langs["CloaksDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListCloak); // Lo añadimos al menu

            List<string> glovesType = new List<string>();
            glovesType.Add(GetConfig.Langs["NoGlovesValue"]);
            int indexGlove = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.GLOVES_MALE.Count + 1; i++)
                {
                    glovesType.Add(GetConfig.Langs["GlovesValue"] + i);
                }
                if (ClothesUtils.GLOVES_MALE.IndexOf(ConvertValue(ClothesDB["Glove"].ToString())) != -1)
                {
                    indexGlove = ClothesUtils.GLOVES_MALE.IndexOf(ConvertValue(ClothesDB["Glove"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.GLOVES_FEMALE.Count + 1; i++)
                {
                    glovesType.Add(GetConfig.Langs["GlovesValue"] + i);
                }
                if (ClothesUtils.GLOVES_FEMALE.IndexOf(ConvertValue(ClothesDB["Glove"].ToString())) != -1)
                {
                    indexGlove = ClothesUtils.GLOVES_FEMALE.IndexOf(ConvertValue(ClothesDB["Glove"].ToString())) + 1;
                }
            }
            MenuListItem mListGloves = new MenuListItem(GetConfig.Langs["Gloves"], glovesType, indexGlove, GetConfig.Langs["GlovesDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListGloves); // Lo añadimos al menu

            List<string> ringsRhType = new List<string>();
            ringsRhType.Add(GetConfig.Langs["NoRingsValue"]);
            int indexRingRh = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.RINGS_RH_MALE.Count + 1; i++)
                {
                    ringsRhType.Add(GetConfig.Langs["RingsValue"] + i);
                }
                if (ClothesUtils.RINGS_RH_MALE.IndexOf(ConvertValue(ClothesDB["RingRh"].ToString())) != -1)
                {
                    indexRingRh = ClothesUtils.RINGS_RH_MALE.IndexOf(ConvertValue(ClothesDB["RingRh"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.RINGS_RH_FEMALE.Count + 1; i++)
                {
                    ringsRhType.Add(GetConfig.Langs["RingsValue"] + i);
                }
                if (ClothesUtils.RINGS_RH_FEMALE.IndexOf(ConvertValue(ClothesDB["RingRh"].ToString())) != -1)
                {
                    indexRingRh = ClothesUtils.RINGS_RH_FEMALE.IndexOf(ConvertValue(ClothesDB["RingRh"].ToString())) + 1;
                }
            }
            MenuListItem mListRingsRhType = new MenuListItem(GetConfig.Langs["RightRings"], ringsRhType, indexRingRh, GetConfig.Langs["RightRingsDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListRingsRhType); // Lo añadimos al menu

            List<string> ringsLhType = new List<string>();
            ringsLhType.Add(GetConfig.Langs["NoRingsValue"]);
            int indexRingLh = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.RINGS_LH_MALE.Count + 1; i++)
                {
                    ringsLhType.Add(GetConfig.Langs["RingsValue"] + i);
                }
                if (ClothesUtils.RINGS_LH_MALE.IndexOf(ConvertValue(ClothesDB["RingLh"].ToString())) != -1)
                {
                    indexRingLh = ClothesUtils.RINGS_LH_MALE.IndexOf(ConvertValue(ClothesDB["RingLh"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.RINGS_LH_FEMALE.Count + 1; i++)
                {
                    ringsLhType.Add(GetConfig.Langs["RingsValue"] + i);
                }
                if (ClothesUtils.RINGS_LH_FEMALE.IndexOf(ConvertValue(ClothesDB["RingLh"].ToString())) != -1)
                {
                    indexRingLh = ClothesUtils.RINGS_LH_FEMALE.IndexOf(ConvertValue(ClothesDB["RingLh"].ToString())) + 1;
                }
            }
            MenuListItem mListRingsLh = new MenuListItem(GetConfig.Langs["LeftRings"], ringsLhType, indexRingLh, GetConfig.Langs["LeftRingsDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListRingsLh); // Lo añadimos al menu

            List<string> braceletsType = new List<string>();
            braceletsType.Add(GetConfig.Langs["NoBraceletsValue"]);
            int indexBracelet = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.BRACELETS_MALE.Count + 1; i++)
                {
                    braceletsType.Add(GetConfig.Langs["BraceletsValue"] + i);
                }
                if (ClothesUtils.BRACELETS_MALE.IndexOf(ConvertValue(ClothesDB["Bracelet"].ToString())) != -1)
                {
                    indexBracelet = ClothesUtils.BRACELETS_MALE.IndexOf(ConvertValue(ClothesDB["Bracelet"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.BRACELETS_FEMALE.Count + 1; i++)
                {
                    braceletsType.Add(GetConfig.Langs["BraceletsValue"] + i);
                }
                if (ClothesUtils.BRACELETS_FEMALE.IndexOf(ConvertValue(ClothesDB["Bracelet"].ToString())) != -1)
                {
                    indexBracelet = ClothesUtils.BRACELETS_FEMALE.IndexOf(ConvertValue(ClothesDB["Bracelet"].ToString())) + 1;
                }
            }
            MenuListItem mListbracelets = new MenuListItem(GetConfig.Langs["Bracelets"], braceletsType, indexBracelet, GetConfig.Langs["BraceletsDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListbracelets); // Lo añadimos al menu

            List<string> gunbeltType = new List<string>();
            gunbeltType.Add(GetConfig.Langs["NoHolstersValue"]);
            int indexGunbelt = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.GUNBELT_MALE.Count + 1; i++)
                {
                    gunbeltType.Add(GetConfig.Langs["HolstersValue"] + i);
                }
                if (ClothesUtils.GUNBELT_MALE.IndexOf(ConvertValue(ClothesDB["Gunbelt"].ToString())) != -1)
                {
                    indexGunbelt = ClothesUtils.GUNBELT_MALE.IndexOf(ConvertValue(ClothesDB["Gunbelt"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.GUNBELT_FEMALE.Count + 1; i++)
                {
                    gunbeltType.Add(GetConfig.Langs["HolstersValue"] + i);
                }
                if (ClothesUtils.GUNBELT_FEMALE.IndexOf(ConvertValue(ClothesDB["Gunbelt"].ToString())) != -1)
                {
                    indexGunbelt = ClothesUtils.GUNBELT_FEMALE.IndexOf(ConvertValue(ClothesDB["Gunbelt"].ToString())) + 1;
                }
            }
            MenuListItem mListGunbelt = new MenuListItem(GetConfig.Langs["PrimaryHolsters"], gunbeltType, indexGunbelt, GetConfig.Langs["PrimaryHolstersDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListGunbelt); // Lo añadimos al menu

            List<string> beltType = new List<string>();
            beltType.Add(GetConfig.Langs["NoBeltsValue"]);
            int indexBelt = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.BELT_MALE.Count + 1; i++)
                {
                    beltType.Add(GetConfig.Langs["BeltsValue"] + i);
                }
                if (ClothesUtils.BELT_MALE.IndexOf(ConvertValue(ClothesDB["Belt"].ToString())) != -1)
                {
                    indexBelt = ClothesUtils.BELT_MALE.IndexOf(ConvertValue(ClothesDB["Belt"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.BELT_FEMALE.Count + 1; i++)
                {
                    beltType.Add(GetConfig.Langs["BeltsValue"] + i);
                }
                if (ClothesUtils.BELT_FEMALE.IndexOf(ConvertValue(ClothesDB["Belt"].ToString())) != -1)
                {
                    indexBelt = ClothesUtils.BELT_FEMALE.IndexOf(ConvertValue(ClothesDB["Belt"].ToString())) + 1;
                }
            }
            MenuListItem mListBelt = new MenuListItem(GetConfig.Langs["Belts"], beltType, indexBelt, GetConfig.Langs["BeltsDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListBelt); // Lo añadimos al menu

            List<string> buckleType = new List<string>();
            buckleType.Add(GetConfig.Langs["NoBucklesValue"]);
            int indexBuckle = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.BUCKLE_MALE.Count + 1; i++)
                {
                    buckleType.Add(GetConfig.Langs["BucklesValue"] + i);
                }
                if (ClothesUtils.BUCKLE_MALE.IndexOf(ConvertValue(ClothesDB["Buckle"].ToString())) != -1)
                {
                    indexBuckle = ClothesUtils.BUCKLE_MALE.IndexOf(ConvertValue(ClothesDB["Buckle"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.BUCKLE_FEMALE.Count + 1; i++)
                {
                    buckleType.Add(GetConfig.Langs["BucklesValue"] + i);
                }
                if (ClothesUtils.BUCKLE_FEMALE.IndexOf(ConvertValue(ClothesDB["Buckle"].ToString())) != -1)
                {
                    indexBuckle = ClothesUtils.BUCKLE_FEMALE.IndexOf(ConvertValue(ClothesDB["Buckle"].ToString())) + 1;
                }
            }
            MenuListItem mListBuckle = new MenuListItem(GetConfig.Langs["Buckles"], buckleType, indexBuckle, GetConfig.Langs["BucklesDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListBuckle); // Lo añadimos al menu

            List<string> holstersSType = new List<string>();
            holstersSType.Add(GetConfig.Langs["NoHolstersValue"]);
            int indexHolster = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.HOLSTERS_S_MALE.Count + 1; i++)
                {
                    holstersSType.Add(GetConfig.Langs["HolstersValue"] + i);
                }
                if (ClothesUtils.HOLSTERS_S_MALE.IndexOf(ConvertValue(ClothesDB["Holster"].ToString())) != -1)
                {
                    indexHolster = ClothesUtils.HOLSTERS_S_MALE.IndexOf(ConvertValue(ClothesDB["Holster"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.HOLSTERS_S_FEMALE.Count + 1; i++)
                {
                    holstersSType.Add(GetConfig.Langs["HolstersValue"] + i);
                }
                if (ClothesUtils.HOLSTERS_S_FEMALE.IndexOf(ConvertValue(ClothesDB["Holster"].ToString())) != -1)
                {
                    indexHolster = ClothesUtils.HOLSTERS_S_FEMALE.IndexOf(ConvertValue(ClothesDB["Holster"].ToString())) + 1;
                }
            }
            MenuListItem mListSHolsters = new MenuListItem(GetConfig.Langs["SecondaryHolsters"], holstersSType, indexHolster, GetConfig.Langs["SecondaryHolstersDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListSHolsters); // Lo añadimos al menu

            List<string> pantsType = new List<string>();
            pantsType.Add(GetConfig.Langs["NoPantsValue"]);
            int indexPant = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.PANTS_MALE.Count + 1; i++)
                {
                    pantsType.Add(GetConfig.Langs["PantsValue"] + i);
                }
                if (ClothesUtils.PANTS_MALE.IndexOf(ConvertValue(ClothesDB["Pant"].ToString())) != -1)
                {
                    indexPant = ClothesUtils.PANTS_MALE.IndexOf(ConvertValue(ClothesDB["Pant"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.PANTS_FEMALE.Count + 1; i++)
                {
                    pantsType.Add(GetConfig.Langs["PantsValue"] + i);
                }
                if (ClothesUtils.PANTS_FEMALE.IndexOf(ConvertValue(ClothesDB["Pant"].ToString())) != -1)
                {
                    indexPant = ClothesUtils.PANTS_FEMALE.IndexOf(ConvertValue(ClothesDB["Pant"].ToString())) + 1;
                }
            }
            MenuListItem mListPants = new MenuListItem(GetConfig.Langs["Pants"], pantsType, indexPant, GetConfig.Langs["PantsDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListPants); // Lo añadimos al menu

            List<string> skirtsType = new List<string>();
            skirtsType.Add(GetConfig.Langs["NoSkirtsValue"]);
            int indexSkirt = 0;

            if (model == "mp_male")
            {
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.SKIRTS_FEMALE.Count + 1; i++)
                {
                    skirtsType.Add(GetConfig.Langs["SkirtsValue"] + i);
                }
                if (ClothesUtils.SKIRTS_FEMALE.IndexOf(ConvertValue(ClothesDB["Skirt"].ToString())) != -1)
                {
                    indexSkirt = ClothesUtils.SKIRTS_FEMALE.IndexOf(ConvertValue(ClothesDB["Skirt"].ToString())) + 1;
                }
            }
            MenuListItem mListSkirts = new MenuListItem(GetConfig.Langs["Skirts"], skirtsType, indexSkirt, GetConfig.Langs["SkirtsDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListSkirts); // Lo añadimos al menu

            List<string> chapsType = new List<string>();
            chapsType.Add(GetConfig.Langs["NoChapsValue"]);
            int indexChap = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.CHAPS_MALE.Count + 1; i++)
                {
                    chapsType.Add(GetConfig.Langs["ChapsValue"] + i);
                }
                if (ClothesUtils.CHAPS_MALE.IndexOf(ConvertValue(ClothesDB["Chap"].ToString())) != -1)
                {
                    indexChap = ClothesUtils.CHAPS_MALE.IndexOf(ConvertValue(ClothesDB["Chap"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.CHAPS_FEMALE.Count + 1; i++)
                {
                    chapsType.Add(GetConfig.Langs["ChapsValue"] + i);
                }
                if (ClothesUtils.CHAPS_FEMALE.IndexOf(ConvertValue(ClothesDB["Chap"].ToString())) != -1)
                {
                    indexChap = ClothesUtils.CHAPS_FEMALE.IndexOf(ConvertValue(ClothesDB["Chap"].ToString())) + 1;
                }
            }
            MenuListItem mListChaps = new MenuListItem(GetConfig.Langs["Chaps"], chapsType, indexChap, GetConfig.Langs["ChapsDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListChaps); // Lo añadimos al menu

            List<string> bootsType = new List<string>();
            bootsType.Add(GetConfig.Langs["NoBootsValue"]);
            int indexBoots = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.BOOTS_MALE.Count + 1; i++)
                {
                    bootsType.Add(GetConfig.Langs["BootsValue"] + i);
                }
                if (ClothesUtils.BOOTS_MALE.IndexOf(ConvertValue(ClothesDB["Boots"].ToString())) != -1)
                {
                    indexBoots = ClothesUtils.BOOTS_MALE.IndexOf(ConvertValue(ClothesDB["Boots"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.BOOTS_FEMALE.Count + 1; i++)
                {
                    bootsType.Add(GetConfig.Langs["BootsValue"] + i);
                }
                if (ClothesUtils.BOOTS_FEMALE.IndexOf(ConvertValue(ClothesDB["Boots"].ToString())) != -1)
                {
                    indexBoots = ClothesUtils.BOOTS_FEMALE.IndexOf(ConvertValue(ClothesDB["Boots"].ToString())) + 1;
                }
            }
            MenuListItem mListBoots = new MenuListItem(GetConfig.Langs["Boots"], bootsType, indexBoots, GetConfig.Langs["BootsDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListBoots); // Lo añadimos al menu

            List<string> spursType = new List<string>();
            spursType.Add(GetConfig.Langs["NoSpursValue"]);
            int indexSpurs = 0;

            if (model == "mp_male")
            {
                //Cabellos de Hombre
                for (float i = 1; i < ClothesUtils.SPURS_MALE.Count + 1; i++)
                {
                    spursType.Add(GetConfig.Langs["SpursValue"] + i);
                }
                if (ClothesUtils.SPURS_MALE.IndexOf(ConvertValue(ClothesDB["Spurs"].ToString())) != -1)
                {
                    indexSpurs = ClothesUtils.SPURS_MALE.IndexOf(ConvertValue(ClothesDB["Spurs"].ToString())) + 1;
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.SPURS_FEMALE.Count + 1; i++)
                {
                    spursType.Add(GetConfig.Langs["SpursValue"] + i);
                }
                if (ClothesUtils.SPURS_FEMALE.IndexOf(ConvertValue(ClothesDB["Spurs"].ToString())) != -1)
                {
                    indexSpurs = ClothesUtils.SPURS_FEMALE.IndexOf(ConvertValue(ClothesDB["Spurs"].ToString())) + 1;
                }
            }
            MenuListItem mListSpurs = new MenuListItem(GetConfig.Langs["Spurs"], spursType, indexSpurs, GetConfig.Langs["SpursDesc"]); // Añadimos la lista al boton
            mdu.AddMenuItem(mListSpurs); // Lo añadimos al menu

            //Terminamos y confirmamos
            MenuItem finishButton = new MenuItem(GetConfig.Langs["Finish"], GetConfig.Langs["FinishDesc"]);
            finishButton.Enabled = false;
            finishButton.LeftIcon = MenuItem.Icon.TICK;

            mdu.AddMenuItem(finishButton);

            mdu.OnIndexChange += (_menu, _oldItem, _newItem, _oldIndex, _newIndex) =>
            {
                Debug.WriteLine($"OnIndexChange: [{_menu}, {_oldItem}, {_newItem}, {_oldIndex}, {_newIndex}]");
            };

            double totalCost = 0;

            mdu.OnListIndexChange += (_menu, _listItem, _oldIndex, _newIndex, _itemIndex) =>
            {

                // Code in here would get executed whenever the selected value of a list item changes (when left/right key is pressed).
                Debug.WriteLine($"Cambios del menu: [{_menu}, {_listItem}, {_oldIndex}, {_newIndex}, {_itemIndex}]");
                int pPID = PlayerPedId();

                switch (_itemIndex)
                {
                    // New System more simplificated
                    case 0:
                        SetPlayerComponent(model, _newIndex, 0x9925C067, "Hat", ClothesUtils.HATS_MALE, ClothesUtils.HATS_FEMALE);
                        break;
                    case 1:
                        SetPlayerComponent(model, _newIndex, 0x5E47CA6, "EyeWear", ClothesUtils.EYEWEAR_MALE, ClothesUtils.EYEWEAR_FEMALE);
                        break;
                    case 2:
                        SetPlayerComponent(model, _newIndex, 0x5FC29285, "NeckWear", ClothesUtils.NECKWEAR_MALE, ClothesUtils.NECKWEAR_FEMALE);
                        break;
                    case 3:
                        SetPlayerComponent(model, _newIndex, 0x7505EF42, "Mask", ClothesUtils.MASK_MALE, ClothesUtils.MASK_FEMALE);
                        break;
                    case 4:
                        SetPlayerComponent(model, _newIndex, 0x7A96FACA, "NeckTies", ClothesUtils.NECKTIES_MALE, ClothesUtils.NECKTIES_FEMALE);
                        break;
                    case 5:
                        SetPlayerComponent(model, _newIndex, 0x2026C46D, "Shirt", ClothesUtils.SHIRTS_MALE, ClothesUtils.SHIRTS_FEMALE);
                        break;
                    case 6:
                        SetPlayerComponent(model, _newIndex, 0x877A2CF7, "Suspender", ClothesUtils.SUSPENDERS_MALE, ClothesUtils.SUSPENDERS_FEMALE);
                        break;
                    case 7:
                        SetPlayerComponent(model, _newIndex, 0x485EE834, "Vest", ClothesUtils.VEST_MALE, ClothesUtils.VEST_FEMALE);
                        break;
                    case 8:
                        SetPlayerComponent(model, _newIndex, 0xE06D30CE, "Coat", ClothesUtils.COATS_MALE, ClothesUtils.COATS_FEMALE);
                        break;
                    case 9:
                        SetPlayerComponent(model, _newIndex, 0xAF14310B, "Poncho", ClothesUtils.PONCHOS_MALE, ClothesUtils.PONCHOS_FEMALE);
                        break;
                    case 10:
                        SetPlayerComponent(model, _newIndex, 0x3C1A74CD, "Cloak", ClothesUtils.CLOAK_MALE, ClothesUtils.CLOAK_FEMALE);
                        break;
                    case 11:
                        SetPlayerComponent(model, _newIndex, 0xEABE0032, "Glove", ClothesUtils.GLOVES_MALE, ClothesUtils.GLOVES_FEMALE);
                        break;
                    case 12:
                        SetPlayerComponent(model, _newIndex, 0x7A6BBD0B, "RingRh", ClothesUtils.RINGS_RH_MALE, ClothesUtils.RINGS_RH_FEMALE);
                        break;
                    case 13:
                        SetPlayerComponent(model, _newIndex, 0xF16A1D23, "RingLh", ClothesUtils.RINGS_LH_MALE, ClothesUtils.RINGS_LH_FEMALE);
                        break;
                    case 14:
                        SetPlayerComponent(model, _newIndex, 0x7BC10759, "Bracelet", ClothesUtils.BRACELETS_MALE, ClothesUtils.BRACELETS_FEMALE);
                        break;
                    case 15:
                        SetPlayerComponent(model, _newIndex, 0x9B2C8B89, "Gunbelt", ClothesUtils.GUNBELT_MALE, ClothesUtils.GUNBELT_FEMALE);
                        break;
                    case 16:
                        SetPlayerComponent(model, _newIndex, 0xA6D134C6, "Belt", ClothesUtils.BELT_MALE, ClothesUtils.BELT_FEMALE);
                        break;
                    case 17:
                        SetPlayerComponent(model, _newIndex, 0xFAE9107F, "Buckle", ClothesUtils.BUCKLE_MALE, ClothesUtils.BUCKLE_FEMALE);
                        break;
                    case 18:
                        SetPlayerComponent(model, _newIndex, 0xB6B6122D, "Holster", ClothesUtils.HOLSTERS_S_MALE, ClothesUtils.HOLSTERS_S_FEMALE);
                        break;
                    case 19:
                        SetPlayerComponent(model, _newIndex, 0x1D4C528A, "Pant", ClothesUtils.PANTS_MALE, ClothesUtils.PANTS_FEMALE);
                        break;
                    case 20:
                        SetPlayerComponent(model, _newIndex, 0xA0E3AB7F, "Skirt", ClothesUtils.SKIRTS_FEMALE, ClothesUtils.SKIRTS_FEMALE);
                        break;
                    case 21:
                        SetPlayerComponent(model, _newIndex, 0x3107499B, "Chap", ClothesUtils.CHAPS_MALE, ClothesUtils.CHAPS_FEMALE);
                        break;
                    case 22:
                        SetPlayerComponent(model, _newIndex, 0x777EC6EF, "Boots", ClothesUtils.BOOTS_MALE, ClothesUtils.BOOTS_FEMALE);
                        break;
                    case 23:
                        SetPlayerComponent(model, _newIndex, 0x18729F39, "Spurs", ClothesUtils.SPURS_MALE, ClothesUtils.SPURS_FEMALE);
                        break;
                }

                totalCost = 0;

                if (ConvertValue(ClothesDB["Hat"].ToString()) != ConvertValue(clothesPlayer["Hat"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costHats"].ToString());
                }
                if (ConvertValue(ClothesDB["EyeWear"].ToString()) != ConvertValue(clothesPlayer["EyeWear"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costEyeWear"].ToString());
                }
                if (ConvertValue(ClothesDB["NeckWear"].ToString()) != ConvertValue(clothesPlayer["NeckWear"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costNeckWear"].ToString());
                }
                if (ConvertValue(ClothesDB["Mask"].ToString()) != ConvertValue(clothesPlayer["Mask"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costMask"].ToString());
                }
                if (ConvertValue(ClothesDB["NeckTies"].ToString()) != ConvertValue(clothesPlayer["NeckTies"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costNeckTies"].ToString());
                }
                if (ConvertValue(ClothesDB["Shirt"].ToString()) != ConvertValue(clothesPlayer["Shirt"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costShirt"].ToString());
                }
                if (ConvertValue(ClothesDB["Suspender"].ToString()) != ConvertValue(clothesPlayer["Suspender"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costSuspender"].ToString());
                }
                if (ConvertValue(ClothesDB["Vest"].ToString()) != ConvertValue(clothesPlayer["Vest"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costVest"].ToString());
                }
                if (ConvertValue(ClothesDB["Coat"].ToString()) != ConvertValue(clothesPlayer["Coat"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costCoat"].ToString());
                }
                if (ConvertValue(ClothesDB["Poncho"].ToString()) != ConvertValue(clothesPlayer["Poncho"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costPoncho"].ToString());
                }
                if (ConvertValue(ClothesDB["Cloak"].ToString()) != ConvertValue(clothesPlayer["Cloak"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costCloak"].ToString());
                }
                if (ConvertValue(ClothesDB["Glove"].ToString()) != ConvertValue(clothesPlayer["Glove"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costGlove"].ToString());
                }
                if (ConvertValue(ClothesDB["RingRh"].ToString()) != ConvertValue(clothesPlayer["RingRh"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costRingRh"].ToString());
                }
                if (ConvertValue(ClothesDB["RingLh"].ToString()) != ConvertValue(clothesPlayer["RingLh"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costRingLh"].ToString());
                }
                if (ConvertValue(ClothesDB["Bracelet"].ToString()) != ConvertValue(clothesPlayer["Bracelet"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costBracelet"].ToString());
                }
                if (ConvertValue(ClothesDB["Gunbelt"].ToString()) != ConvertValue(clothesPlayer["Gunbelt"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costGunbelt"].ToString());
                }
                if (ConvertValue(ClothesDB["Belt"].ToString()) != ConvertValue(clothesPlayer["Belt"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costBelt"].ToString());
                }
                if (ConvertValue(ClothesDB["Buckle"].ToString()) != ConvertValue(clothesPlayer["Buckle"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costBuckle"].ToString());
                }
                if (ConvertValue(ClothesDB["Holster"].ToString()) != ConvertValue(clothesPlayer["Holster"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costHolster"].ToString());
                }
                if (ConvertValue(ClothesDB["Pant"].ToString()) != ConvertValue(clothesPlayer["Pant"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costPant"].ToString());
                }
                if (ConvertValue(ClothesDB["Skirt"].ToString()) != ConvertValue(clothesPlayer["Skirt"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costSkirt"].ToString());
                }
                if (ConvertValue(ClothesDB["Chap"].ToString()) != ConvertValue(clothesPlayer["Chap"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costChap"].ToString());
                }
                if (ConvertValue(ClothesDB["Boots"].ToString()) != ConvertValue(clothesPlayer["Boots"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costBoots"].ToString());
                }
                if (ConvertValue(ClothesDB["Spurs"].ToString()) != ConvertValue(clothesPlayer["Spurs"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costSpurs"].ToString());
                }

                if (totalCost > 0.0)
                {
                    finishButton.Label = $" (${totalCost})";
                    finishButton.Enabled = true;
                }
                else
                {
                    finishButton.Label = "";
                    finishButton.Enabled = false;
                }

            };

            mdu.OnItemSelect += (_menu, _item, _index) =>
            {
                // Code in here would get executed whenever an item is pressed.
                Debug.WriteLine($"OnItemSelect: [{_menu}, {_item}, {_index}]");
                if (_index == 24)
                {
                    isBuy = true;
                    FinishBuy(isBuy, totalCost);
                    mdu.CloseMenu();
                }
            };

            mdu.OnMenuClose += (_menu) =>
            {
                // Code in here gets triggered whenever the menu is closed.
                Debug.WriteLine($"OnMenuClose: [{_menu}]");
                if (!isBuy)
                {
                    TriggerServerEvent("vorpcharacter:getPlayerSkin");
                    startBuyCloths(isBuy);
                }
            };

            mdu.OpenMenu();
        }


        public static void FinishBuy(bool buy, double cost)
        {
            if (buy)
            {
                string JsonCloths = Newtonsoft.Json.JsonConvert.SerializeObject(clothesPlayer);
                TriggerServerEvent("vorpclothingstore:buyPlayerCloths", cost, JsonCloths);
            }
        }

        private static void startBuyCloths(bool completed)
        {
            completeBuy(completed);
        }

        public static async Task completeBuy(bool completedBuy)
        {
            TriggerServerEvent("vorpcharacter:getPlayerSkin");
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

        private static void SetPlayerComponent(string model, int _newIndex, uint category, string idlist, List<uint> male_components, List<uint> female_components)
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
