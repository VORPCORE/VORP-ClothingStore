using CitizenFX.Core;
using CitizenFX.Core.Native;
using MenuAPI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vorpclothingstore_cl.Utils;

namespace vorpclothingstore_cl.Menus
{
    class MainMenu
    {
        private static Menu mainMenu = new Menu(GetConfig.Langs["TitleMenuClothes"], GetConfig.Langs["SubTitleMenuClothes"]);
        private static bool setupDone = false;

        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(mainMenu);

            MenuController.EnableMenuToggleKeyOnController = false;
            MenuController.MenuToggleKey = (Control)0;

            Utils.Commands.isBuy = false;

            List<string> hatType = new List<string>();
            hatType.Add(GetConfig.Langs["NoHatsValue"]);
            int indexHat = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.HATS_MALE.Count + 1; i++)
                {
                    hatType.Add(GetConfig.Langs["HatsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.HATS_FEMALE.Count + 1; i++)
                {
                    hatType.Add(GetConfig.Langs["HatsValue"] + i);
                }

            }
            MenuListItem mListHats = new MenuListItem(GetConfig.Langs["Hats"], hatType, indexHat, GetConfig.Langs["HatsDesc"]);
            mainMenu.AddMenuItem(mListHats);

            List<string> eyeWearType = new List<string>();
            eyeWearType.Add(GetConfig.Langs["NoGlassesValue"]);
            int indexEyeWears = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.EYEWEAR_MALE.Count + 1; i++)
                {
                    eyeWearType.Add(GetConfig.Langs["GlassesValue"] + i);
                }
            }
            else
            {
                //Cabellos de Mujer
                for (float i = 1; i < ClothesUtils.EYEWEAR_FEMALE.Count + 1; i++)
                {
                    eyeWearType.Add(GetConfig.Langs["GlassesValue"] + i);
                }

            }
            MenuListItem mListEyeWear = new MenuListItem(GetConfig.Langs["Glasses"], eyeWearType, indexEyeWears, GetConfig.Langs["GlassesDesc"]);
            mainMenu.AddMenuItem(mListEyeWear);

            List<string> neckWearType = new List<string>();
            neckWearType.Add(GetConfig.Langs["NoNeckwearValue"]);
            int indexNeckWear = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.NECKWEAR_MALE.Count + 1; i++)
                {
                    neckWearType.Add(GetConfig.Langs["NeckwearValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.NECKWEAR_FEMALE.Count + 1; i++)
                {
                    neckWearType.Add(GetConfig.Langs["NeckwearValue"] + i);
                }

            }
            MenuListItem mListNeckWear = new MenuListItem(GetConfig.Langs["Neckwear"], neckWearType, indexNeckWear, GetConfig.Langs["NeckwearDesc"]);
            mainMenu.AddMenuItem(mListNeckWear);

            List<string> maskType = new List<string>();
            maskType.Add(GetConfig.Langs["NoMaskValue"]);
            int indexMaskType = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.MASK_MALE.Count + 1; i++)
                {
                    maskType.Add(GetConfig.Langs["MaskValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.MASK_FEMALE.Count + 1; i++)
                {
                    maskType.Add(GetConfig.Langs["MaskValue"] + i);
                }

            }
            MenuListItem mListMask = new MenuListItem(GetConfig.Langs["Masks"], maskType, indexMaskType, GetConfig.Langs["MasksDesc"]);
            mainMenu.AddMenuItem(mListMask);

            List<string> neckTiesType = new List<string>();
            neckTiesType.Add(GetConfig.Langs["NoTiesValue"]);
            int indexNeckTies = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.NECKTIES_MALE.Count + 1; i++)
                {
                    neckTiesType.Add(GetConfig.Langs["TiesValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.NECKTIES_FEMALE.Count + 1; i++)
                {
                    neckTiesType.Add(GetConfig.Langs["TiesValue"] + i);
                }
            }
            MenuListItem mListNeckTies = new MenuListItem(GetConfig.Langs["Ties"], neckTiesType, indexNeckTies, GetConfig.Langs["TiesDesc"]);
            mainMenu.AddMenuItem(mListNeckTies);

            List<string> shirtsType = new List<string>();
            shirtsType.Add(GetConfig.Langs["NoShirtsValue"]);
            int indexShirt = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.SHIRTS_MALE.Count + 1; i++)
                {
                    shirtsType.Add(GetConfig.Langs["ShirtsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.SHIRTS_FEMALE.Count + 1; i++)
                {
                    shirtsType.Add(GetConfig.Langs["ShirtsValue"] + i);
                }
            }
            MenuListItem mListShirts = new MenuListItem(GetConfig.Langs["Shirts"], shirtsType, indexShirt, GetConfig.Langs["ShirtsDesc"]);
            mainMenu.AddMenuItem(mListShirts);

            List<string> suspendersType = new List<string>();
            suspendersType.Add(GetConfig.Langs["NoSuspendersValue"]);
            int indexSuspender = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.SUSPENDERS_MALE.Count + 1; i++)
                {
                    suspendersType.Add(GetConfig.Langs["SuspendersValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.SUSPENDERS_FEMALE.Count + 1; i++)
                {
                    suspendersType.Add(GetConfig.Langs["SuspendersValue"] + i);
                }

            }
            MenuListItem mListSuspenders = new MenuListItem(GetConfig.Langs["Suspenders"], suspendersType, indexSuspender, GetConfig.Langs["SuspendersDesc"]);
            mainMenu.AddMenuItem(mListSuspenders);

            List<string> vestType = new List<string>();
            vestType.Add(GetConfig.Langs["NoVestsValue"]);
            int indexVest = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.VEST_MALE.Count + 1; i++)
                {
                    vestType.Add(GetConfig.Langs["VestsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.VEST_FEMALE.Count + 1; i++)
                {
                    vestType.Add(GetConfig.Langs["VestsValue"] + i);
                }

            }
            MenuListItem mListVest = new MenuListItem(GetConfig.Langs["Vests"], vestType, indexVest, GetConfig.Langs["VestsDesc"]);
            mainMenu.AddMenuItem(mListVest);

            List<string> coatsType = new List<string>();
            coatsType.Add(GetConfig.Langs["NoCoatsValue"]);
            int indexCoat = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.COATS_MALE.Count + 1; i++)
                {
                    coatsType.Add(GetConfig.Langs["CoatsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.COATS_FEMALE.Count + 1; i++)
                {
                    coatsType.Add(GetConfig.Langs["CoatsValue"] + i);
                }

            }
            MenuListItem mListCoats = new MenuListItem(GetConfig.Langs["Coats"], coatsType, indexCoat, GetConfig.Langs["CoatsDesc"]);
            mainMenu.AddMenuItem(mListCoats);

            List<string> coatsClosedType = new List<string>();
            coatsClosedType.Add(GetConfig.Langs["NoCoatsValue"]);

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.COATS_CLOSED_MALE.Count + 1; i++)
                {
                    coatsClosedType.Add(GetConfig.Langs["CoatsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.COATS_CLOSED_FEMALE.Count + 1; i++)
                {
                    coatsClosedType.Add(GetConfig.Langs["CoatsValue"] + i);
                }
            }
            MenuListItem mListCoatsClosed = new MenuListItem(GetConfig.Langs["CoatsClosed"], coatsClosedType, 0, GetConfig.Langs["CoatsDesc"]);
            mainMenu.AddMenuItem(mListCoatsClosed);

            List<string> ponchosType = new List<string>();
            ponchosType.Add(GetConfig.Langs["NoPonchosValue"]);
            int indexPoncho = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.PONCHOS_MALE.Count + 1; i++)
                {
                    ponchosType.Add(GetConfig.Langs["PonchosValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.PONCHOS_FEMALE.Count + 1; i++)
                {
                    ponchosType.Add(GetConfig.Langs["PonchosValue"] + i);
                }

            }
            MenuListItem mListPonchos = new MenuListItem(GetConfig.Langs["Ponchos"], ponchosType, indexPoncho, GetConfig.Langs["PonchosDesc"]);
            mainMenu.AddMenuItem(mListPonchos);

            List<string> cloakType = new List<string>();
            cloakType.Add(GetConfig.Langs["NoCloaksValue"]);
            int indexCloak = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.CLOAK_MALE.Count + 1; i++)
                {
                    cloakType.Add(GetConfig.Langs["CloaksValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.CLOAK_FEMALE.Count + 1; i++)
                {
                    cloakType.Add(GetConfig.Langs["CloaksValue"] + i);
                }

            }
            MenuListItem mListCloak = new MenuListItem(GetConfig.Langs["Cloaks"], cloakType, indexCloak, GetConfig.Langs["CloaksDesc"]);
            mainMenu.AddMenuItem(mListCloak);

            List<string> glovesType = new List<string>();
            glovesType.Add(GetConfig.Langs["NoGlovesValue"]);
            int indexGlove = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.GLOVES_MALE.Count + 1; i++)
                {
                    glovesType.Add(GetConfig.Langs["GlovesValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.GLOVES_FEMALE.Count + 1; i++)
                {
                    glovesType.Add(GetConfig.Langs["GlovesValue"] + i);
                }

            }
            MenuListItem mListGloves = new MenuListItem(GetConfig.Langs["Gloves"], glovesType, indexGlove, GetConfig.Langs["GlovesDesc"]);
            mainMenu.AddMenuItem(mListGloves); 

            List<string> ringsRhType = new List<string>();
            ringsRhType.Add(GetConfig.Langs["NoRingsValue"]);
            int indexRingRh = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.RINGS_RH_MALE.Count + 1; i++)
                {
                    ringsRhType.Add(GetConfig.Langs["RingsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.RINGS_RH_FEMALE.Count + 1; i++)
                {
                    ringsRhType.Add(GetConfig.Langs["RingsValue"] + i);
                }

            }
            MenuListItem mListRingsRhType = new MenuListItem(GetConfig.Langs["RightRings"], ringsRhType, indexRingRh, GetConfig.Langs["RightRingsDesc"]);
            mainMenu.AddMenuItem(mListRingsRhType);

            List<string> ringsLhType = new List<string>();
            ringsLhType.Add(GetConfig.Langs["NoRingsValue"]);
            int indexRingLh = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.RINGS_LH_MALE.Count + 1; i++)
                {
                    ringsLhType.Add(GetConfig.Langs["RingsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.RINGS_LH_FEMALE.Count + 1; i++)
                {
                    ringsLhType.Add(GetConfig.Langs["RingsValue"] + i);
                }

            }
            MenuListItem mListRingsLh = new MenuListItem(GetConfig.Langs["LeftRings"], ringsLhType, indexRingLh, GetConfig.Langs["LeftRingsDesc"]);
            mainMenu.AddMenuItem(mListRingsLh);

            List<string> braceletsType = new List<string>();
            braceletsType.Add(GetConfig.Langs["NoBraceletsValue"]);
            int indexBracelet = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.BRACELETS_MALE.Count + 1; i++)
                {
                    braceletsType.Add(GetConfig.Langs["BraceletsValue"] + i);
                }

            }
            else
            {
                for (float i = 1; i < ClothesUtils.BRACELETS_FEMALE.Count + 1; i++)
                {
                    braceletsType.Add(GetConfig.Langs["BraceletsValue"] + i);
                }

            }
            MenuListItem mListbracelets = new MenuListItem(GetConfig.Langs["Bracelets"], braceletsType, indexBracelet, GetConfig.Langs["BraceletsDesc"]);
            mainMenu.AddMenuItem(mListbracelets);

            List<string> gunbeltType = new List<string>();
            gunbeltType.Add(GetConfig.Langs["NoHolstersValue"]);
            int indexGunbelt = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.GUNBELT_MALE.Count + 1; i++)
                {
                    gunbeltType.Add(GetConfig.Langs["HolstersValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.GUNBELT_FEMALE.Count + 1; i++)
                {
                    gunbeltType.Add(GetConfig.Langs["HolstersValue"] + i);
                }

            }
            MenuListItem mListGunbelt = new MenuListItem(GetConfig.Langs["PrimaryHolsters"], gunbeltType, indexGunbelt, GetConfig.Langs["PrimaryHolstersDesc"]);
            mainMenu.AddMenuItem(mListGunbelt);

            List<string> beltType = new List<string>();
            beltType.Add(GetConfig.Langs["NoBeltsValue"]);
            int indexBelt = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.BELT_MALE.Count + 1; i++)
                {
                    beltType.Add(GetConfig.Langs["BeltsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.BELT_FEMALE.Count + 1; i++)
                {
                    beltType.Add(GetConfig.Langs["BeltsValue"] + i);
                }

            }
            MenuListItem mListBelt = new MenuListItem(GetConfig.Langs["Belts"], beltType, indexBelt, GetConfig.Langs["BeltsDesc"]);
            mainMenu.AddMenuItem(mListBelt);

            List<string> buckleType = new List<string>();
            buckleType.Add(GetConfig.Langs["NoBucklesValue"]);
            int indexBuckle = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.BUCKLE_MALE.Count + 1; i++)
                {
                    buckleType.Add(GetConfig.Langs["BucklesValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.BUCKLE_FEMALE.Count + 1; i++)
                {
                    buckleType.Add(GetConfig.Langs["BucklesValue"] + i);
                }

            }
            MenuListItem mListBuckle = new MenuListItem(GetConfig.Langs["Buckles"], buckleType, indexBuckle, GetConfig.Langs["BucklesDesc"]);
            mainMenu.AddMenuItem(mListBuckle);

            List<string> holstersSType = new List<string>();
            holstersSType.Add(GetConfig.Langs["NoHolstersValue"]);
            int indexHolster = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.HOLSTERS_S_MALE.Count + 1; i++)
                {
                    holstersSType.Add(GetConfig.Langs["HolstersValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.HOLSTERS_S_FEMALE.Count + 1; i++)
                {
                    holstersSType.Add(GetConfig.Langs["HolstersValue"] + i);
                }

            }
            MenuListItem mListSHolsters = new MenuListItem(GetConfig.Langs["SecondaryHolsters"], holstersSType, indexHolster, GetConfig.Langs["SecondaryHolstersDesc"]);
            mainMenu.AddMenuItem(mListSHolsters);

            List<string> pantsType = new List<string>();
            pantsType.Add(GetConfig.Langs["NoPantsValue"]);
            int indexPant = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.PANTS_MALE.Count + 1; i++)
                {
                    pantsType.Add(GetConfig.Langs["PantsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.PANTS_FEMALE.Count + 1; i++)
                {
                    pantsType.Add(GetConfig.Langs["PantsValue"] + i);
                }

            }
            MenuListItem mListPants = new MenuListItem(GetConfig.Langs["Pants"], pantsType, indexPant, GetConfig.Langs["PantsDesc"]);
            mainMenu.AddMenuItem(mListPants);

            List<string> skirtsType = new List<string>();
            skirtsType.Add(GetConfig.Langs["NoSkirtsValue"]);
            int indexSkirt = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
            }
            else
            {
                for (float i = 1; i < ClothesUtils.SKIRTS_FEMALE.Count + 1; i++)
                {
                    skirtsType.Add(GetConfig.Langs["SkirtsValue"] + i);
                }
            }
            MenuListItem mListSkirts = new MenuListItem(GetConfig.Langs["Skirts"], skirtsType, indexSkirt, GetConfig.Langs["SkirtsDesc"]);
            mainMenu.AddMenuItem(mListSkirts);

            List<string> chapsType = new List<string>();
            chapsType.Add(GetConfig.Langs["NoChapsValue"]);
            int indexChap = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.CHAPS_MALE.Count + 1; i++)
                {
                    chapsType.Add(GetConfig.Langs["ChapsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.CHAPS_FEMALE.Count + 1; i++)
                {
                    chapsType.Add(GetConfig.Langs["ChapsValue"] + i);
                }

            }
            MenuListItem mListChaps = new MenuListItem(GetConfig.Langs["Chaps"], chapsType, indexChap, GetConfig.Langs["ChapsDesc"]);
            mainMenu.AddMenuItem(mListChaps);

            List<string> bootsType = new List<string>();
            bootsType.Add(GetConfig.Langs["NoBootsValue"]);
            int indexBoots = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.BOOTS_MALE.Count + 1; i++)
                {
                    bootsType.Add(GetConfig.Langs["BootsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.BOOTS_FEMALE.Count + 1; i++)
                {
                    bootsType.Add(GetConfig.Langs["BootsValue"] + i);
                }
            }
            MenuListItem mListBoots = new MenuListItem(GetConfig.Langs["Boots"], bootsType, indexBoots, GetConfig.Langs["BootsDesc"]);
            mainMenu.AddMenuItem(mListBoots);

            List<string> spursType = new List<string>();
            spursType.Add(GetConfig.Langs["NoSpursValue"]);
            int indexSpurs = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.SPURS_MALE.Count + 1; i++)
                {
                    spursType.Add(GetConfig.Langs["SpursValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.SPURS_FEMALE.Count + 1; i++)
                {
                    spursType.Add(GetConfig.Langs["SpursValue"] + i);
                }
            }
            MenuListItem mListSpurs = new MenuListItem(GetConfig.Langs["Spurs"], spursType, indexSpurs, GetConfig.Langs["SpursDesc"]);
            mainMenu.AddMenuItem(mListSpurs);

            List<string> spatsType = new List<string>();
            spatsType.Add(GetConfig.Langs["NoSpatsValue"]);
            int indexSpats = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.SPATS_MALE.Count + 1; i++)
                {
                    spatsType.Add(GetConfig.Langs["SpatsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.SPATS_FEMALE.Count + 1; i++)
                {
                    spatsType.Add(GetConfig.Langs["SpatsValue"] + i);
                }
            }
            MenuListItem mListSpats = new MenuListItem(GetConfig.Langs["Spats"], spatsType, indexSpats, GetConfig.Langs["SpatsDesc"]);
            mainMenu.AddMenuItem(mListSpats);

            List<string> gauntletsType = new List<string>();
            gauntletsType.Add(GetConfig.Langs["NoGauntletsValue"]);
            int indexGauntlets = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.GAUNTLETS_MALE.Count + 1; i++)
                {
                    gauntletsType.Add(GetConfig.Langs["GauntletsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.GAUNTLETS_FEMALE.Count + 1; i++)
                {
                    gauntletsType.Add(GetConfig.Langs["GauntletsValue"] + i);
                }
            }
            MenuListItem mListGauntlets = new MenuListItem(GetConfig.Langs["Gauntlets"], gauntletsType, indexGauntlets, GetConfig.Langs["GauntletsDesc"]);
            mainMenu.AddMenuItem(mListGauntlets);

            List<string> loadoutsType = new List<string>();
            loadoutsType.Add(GetConfig.Langs["NoLoadoutsValue"]);
            int indexLoadouts = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.LOADOUTS_MALE.Count + 1; i++)
                {
                    loadoutsType.Add(GetConfig.Langs["LoadoutsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.LOADOUTS_FEMALE.Count + 1; i++)
                {
                    loadoutsType.Add(GetConfig.Langs["LoadoutsValue"] + i);
                }
            }
            MenuListItem mListLoadouts = new MenuListItem(GetConfig.Langs["Loadouts"], loadoutsType, indexLoadouts, GetConfig.Langs["LoadoutsDesc"]);
            mainMenu.AddMenuItem(mListLoadouts);

            List<string> accessoriesType = new List<string>();
            accessoriesType.Add(GetConfig.Langs["NoAccessoriesValue"]);
            int indexAccessories = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.ACCESSORIES_MALE.Count + 1; i++)
                {
                    accessoriesType.Add(GetConfig.Langs["AccessoriesValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.ACCESSORIES_FEMALE.Count + 1; i++)
                {
                    accessoriesType.Add(GetConfig.Langs["AccessoriesValue"] + i);
                }
            }
            MenuListItem mListAccessories = new MenuListItem(GetConfig.Langs["Accessories"], accessoriesType, indexAccessories, GetConfig.Langs["AccessoriesDesc"]);
            mainMenu.AddMenuItem(mListAccessories);

            List<string> satchelsType = new List<string>();
            satchelsType.Add(GetConfig.Langs["NoSatchelsValue"]);
            int indexSatchels = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.SATCHELS_MALE.Count + 1; i++)
                {
                    satchelsType.Add(GetConfig.Langs["SatchelsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.SATCHELS_FEMALE.Count + 1; i++)
                {
                    satchelsType.Add(GetConfig.Langs["SatchelsValue"] + i);
                }
            }
            MenuListItem mListSatchels = new MenuListItem(GetConfig.Langs["Satchels"], satchelsType, indexSatchels, GetConfig.Langs["SatchelsDesc"]);
            mainMenu.AddMenuItem(mListSatchels);

            List<string> gunbeltAccsType = new List<string>();
            gunbeltAccsType.Add(GetConfig.Langs["NoGunbeltAccsValue"]);
            int indexGunbeltAccs = 0;

            if (API.IsPedMale(API.PlayerPedId()))
            {
                for (float i = 1; i < ClothesUtils.GUNBELTACCS_MALE.Count + 1; i++)
                {
                    gunbeltAccsType.Add(GetConfig.Langs["GunbeltAccsValue"] + i);
                }
            }
            else
            {
                for (float i = 1; i < ClothesUtils.GUNBELTACCS_FEMALE.Count + 1; i++)
                {
                    gunbeltAccsType.Add(GetConfig.Langs["GunbeltAccsValue"] + i);
                }
            }
            MenuListItem mListGunbeltAccs = new MenuListItem(GetConfig.Langs["GunbeltAccs"], gunbeltAccsType, indexGunbeltAccs, GetConfig.Langs["GunbeltAccsDesc"]);
            mainMenu.AddMenuItem(mListGunbeltAccs);

            //Outfits
            MenuController.AddSubmenu(mainMenu, OutfitsMenu.GetMenu());

            MenuItem subMenuOutfitBtn = new MenuItem(GetConfig.Langs["TitleMenuOutfits"], GetConfig.Langs["SubTitleMenuOutfits"])
            {
                RightIcon = MenuItem.Icon.STAR
            };

            mainMenu.AddMenuItem(subMenuOutfitBtn);
            MenuController.BindMenuItem(mainMenu, OutfitsMenu.GetMenu(), subMenuOutfitBtn);

            //Terminamos y confirmamos
            MenuItem finishButton = new MenuItem(GetConfig.Langs["Finish"], GetConfig.Langs["FinishDesc"]);
            finishButton.Enabled = false;
            finishButton.LeftIcon = MenuItem.Icon.TICK;

            mainMenu.AddMenuItem(finishButton);

            mainMenu.OnMenuOpen += (_menu) =>
            {
                if (API.IsPedMale(API.PlayerPedId()))
                {
                    if (ClothesUtils.HATS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Hat"].ToString())) != -1)
                    {
                        mListHats.ListIndex = ClothesUtils.HATS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Hat"].ToString())) + 1;
                    }
                    if (ClothesUtils.EYEWEAR_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["EyeWear"].ToString())) != -1)
                    {
                        mListEyeWear.ListIndex = ClothesUtils.EYEWEAR_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["EyeWear"].ToString())) + 1;
                    }
                    if (ClothesUtils.NECKWEAR_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["NeckWear"].ToString())) != -1)
                    {
                        mListNeckWear.ListIndex = ClothesUtils.NECKWEAR_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["NeckWear"].ToString())) + 1;
                    }
                    if (ClothesUtils.MASK_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Mask"].ToString())) != -1)
                    {
                        mListMask.ListIndex = ClothesUtils.MASK_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Mask"].ToString())) + 1;
                    }
                    if (ClothesUtils.NECKTIES_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["NeckTies"].ToString())) != -1)
                    {
                        mListNeckTies.ListIndex = ClothesUtils.NECKTIES_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["NeckTies"].ToString())) + 1;
                    }
                    if (ClothesUtils.SHIRTS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Shirt"].ToString())) != -1)
                    {
                        mListShirts.ListIndex = ClothesUtils.SHIRTS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Shirt"].ToString())) + 1;
                    }
                    if (ClothesUtils.SUSPENDERS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Suspender"].ToString())) != -1)
                    {
                        mListSuspenders.ListIndex = ClothesUtils.SUSPENDERS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Suspender"].ToString())) + 1;
                    }
                    if (ClothesUtils.VEST_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Vest"].ToString())) != -1)
                    {
                        mListVest.ListIndex = ClothesUtils.VEST_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Vest"].ToString())) + 1;
                    }
                    if (ClothesUtils.COATS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Coat"].ToString())) != -1)
                    {
                        mListCoats.ListIndex = ClothesUtils.COATS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Coat"].ToString())) + 1;
                    }
                    if (ClothesUtils.COATS_CLOSED_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["CoatClosed"].ToString())) != -1)
                    {
                        mListCoatsClosed.ListIndex = ClothesUtils.COATS_CLOSED_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["CoatClosed"].ToString())) + 1;
                    }
                    if (ClothesUtils.PONCHOS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Poncho"].ToString())) != -1)
                    {
                        mListPonchos.ListIndex = ClothesUtils.PONCHOS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Poncho"].ToString())) + 1;
                    }
                    if (ClothesUtils.CLOAK_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Cloak"].ToString())) != -1)
                    {
                        mListCloak.ListIndex = ClothesUtils.CLOAK_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Cloak"].ToString())) + 1;
                    }
                    if (ClothesUtils.GLOVES_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Glove"].ToString())) != -1)
                    {
                        mListGloves.ListIndex = ClothesUtils.GLOVES_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Glove"].ToString())) + 1;
                    }
                    if (ClothesUtils.RINGS_RH_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["RingRh"].ToString())) != -1)
                    {
                        mListRingsRhType.ListIndex = ClothesUtils.RINGS_RH_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["RingRh"].ToString())) + 1;
                    }
                    if (ClothesUtils.RINGS_LH_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["RingLh"].ToString())) != -1)
                    {
                        mListRingsLh.ListIndex = ClothesUtils.RINGS_LH_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["RingLh"].ToString())) + 1;
                    }
                    if (ClothesUtils.BRACELETS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Bracelet"].ToString())) != -1)
                    {
                        mListbracelets.ListIndex = ClothesUtils.BRACELETS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Bracelet"].ToString())) + 1;
                    }
                    if (ClothesUtils.GUNBELT_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Gunbelt"].ToString())) != -1)
                    {
                        mListGunbelt.ListIndex = ClothesUtils.GUNBELT_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Gunbelt"].ToString())) + 1;
                    }
                    if (ClothesUtils.BELT_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Belt"].ToString())) != -1)
                    {
                        mListBelt.ListIndex = ClothesUtils.BELT_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Belt"].ToString())) + 1;
                    }
                    if (ClothesUtils.BUCKLE_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Buckle"].ToString())) != -1)
                    {
                        mListBuckle.ListIndex = ClothesUtils.BUCKLE_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Buckle"].ToString())) + 1;
                    }
                    if (ClothesUtils.HOLSTERS_S_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Holster"].ToString())) != -1)
                    {
                        mListSHolsters.ListIndex = ClothesUtils.HOLSTERS_S_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Holster"].ToString())) + 1;
                    }
                    if (ClothesUtils.PANTS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Pant"].ToString())) != -1)
                    {
                        mListPants.ListIndex = ClothesUtils.PANTS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Pant"].ToString())) + 1;
                    }
                    if (ClothesUtils.CHAPS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Chap"].ToString())) != -1)
                    {
                        mListChaps.ListIndex = ClothesUtils.CHAPS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Chap"].ToString())) + 1;
                    }
                    if (ClothesUtils.BOOTS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Boots"].ToString())) != -1)
                    {
                        mListBoots.ListIndex = ClothesUtils.BOOTS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Boots"].ToString())) + 1;
                    }
                    if (ClothesUtils.SPURS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Spurs"].ToString())) != -1)
                    {
                        mListSpurs.ListIndex = ClothesUtils.SPURS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Spurs"].ToString())) + 1;
                    }
                    if (ClothesUtils.SPATS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Spats"].ToString())) != -1)
                    {
                        mListSpats.ListIndex = ClothesUtils.SPATS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Spats"].ToString())) + 1;
                    }
                    if (ClothesUtils.GAUNTLETS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Gauntlets"].ToString())) != -1)
                    {
                        mListGauntlets.ListIndex = ClothesUtils.GAUNTLETS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Gauntlets"].ToString())) + 1;
                    }
                    if (ClothesUtils.LOADOUTS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Loadouts"].ToString())) != -1)
                    {
                        mListLoadouts.ListIndex = ClothesUtils.LOADOUTS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Loadouts"].ToString())) + 1;
                    }
                    if (ClothesUtils.ACCESSORIES_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Accessories"].ToString())) != -1)
                    {
                        mListAccessories.ListIndex = ClothesUtils.ACCESSORIES_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Accessories"].ToString())) + 1;
                    }
                    if (ClothesUtils.SATCHELS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Satchels"].ToString())) != -1)
                    {
                        mListSatchels.ListIndex = ClothesUtils.SATCHELS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Satchels"].ToString())) + 1;
                    }
                    if (ClothesUtils.GUNBELTACCS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["GunbeltAccs"].ToString())) != -1)
                    {
                        mListGunbeltAccs.ListIndex = ClothesUtils.GUNBELTACCS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["GunbeltAccs"].ToString())) + 1;
                    }

                }
                else
                {
                    if (ClothesUtils.HATS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Hat"].ToString())) != -1)
                    {
                        mListHats.ListIndex = ClothesUtils.HATS_MALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Hat"].ToString())) + 1;
                    }
                    if (ClothesUtils.EYEWEAR_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["EyeWear"].ToString())) != -1)
                    {
                        mListEyeWear.ListIndex = ClothesUtils.EYEWEAR_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["EyeWear"].ToString())) + 1;
                    }
                    if (ClothesUtils.NECKWEAR_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["NeckWear"].ToString())) != -1)
                    {
                        mListNeckWear.ListIndex = ClothesUtils.NECKWEAR_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["NeckWear"].ToString())) + 1;
                    }
                    if (ClothesUtils.MASK_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Mask"].ToString())) != -1)
                    {
                        mListMask.ListIndex = ClothesUtils.MASK_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Mask"].ToString())) + 1;
                    }
                    if (ClothesUtils.NECKTIES_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["NeckTies"].ToString())) != -1)
                    {
                        mListNeckTies.ListIndex = ClothesUtils.NECKTIES_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["NeckTies"].ToString())) + 1;
                    }
                    if (ClothesUtils.SHIRTS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Shirt"].ToString())) != -1)
                    {
                        mListShirts.ListIndex = ClothesUtils.SHIRTS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Shirt"].ToString())) + 1;
                    }
                    if (ClothesUtils.SUSPENDERS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Suspender"].ToString())) != -1)
                    {
                        mListSuspenders.ListIndex = ClothesUtils.SUSPENDERS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Suspender"].ToString())) + 1;
                    }
                    if (ClothesUtils.VEST_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Vest"].ToString())) != -1)
                    {
                        mListVest.ListIndex = ClothesUtils.VEST_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Vest"].ToString())) + 1;
                    }
                    if (ClothesUtils.COATS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Coat"].ToString())) != -1)
                    {
                        mListCoats.ListIndex = ClothesUtils.COATS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Coat"].ToString())) + 1;
                    }
                    if (ClothesUtils.COATS_CLOSED_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["CoatClosed"].ToString())) != -1)
                    {
                        mListCoatsClosed.ListIndex = ClothesUtils.COATS_CLOSED_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["CoatClosed"].ToString())) + 1;
                    }
                    if (ClothesUtils.PONCHOS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Poncho"].ToString())) != -1)
                    {
                        mListPonchos.ListIndex = ClothesUtils.PONCHOS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Poncho"].ToString())) + 1;
                    }
                    if (ClothesUtils.CLOAK_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Cloak"].ToString())) != -1)
                    {
                        mListCloak.ListIndex = ClothesUtils.CLOAK_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Cloak"].ToString())) + 1;
                    }
                    if (ClothesUtils.GLOVES_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Glove"].ToString())) != -1)
                    {
                        mListGloves.ListIndex = ClothesUtils.GLOVES_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Glove"].ToString())) + 1;
                    }
                    if (ClothesUtils.RINGS_RH_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["RingRh"].ToString())) != -1)
                    {
                        mListRingsRhType.ListIndex = ClothesUtils.RINGS_RH_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["RingRh"].ToString())) + 1;
                    }
                    if (ClothesUtils.RINGS_LH_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["RingLh"].ToString())) != -1)
                    {
                        mListRingsLh.ListIndex = ClothesUtils.RINGS_LH_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["RingLh"].ToString())) + 1;
                    }
                    if (ClothesUtils.BRACELETS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Bracelet"].ToString())) != -1)
                    {
                        mListbracelets.ListIndex = ClothesUtils.BRACELETS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Bracelet"].ToString())) + 1;
                    }
                    if (ClothesUtils.GUNBELT_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Gunbelt"].ToString())) != -1)
                    {
                        mListGunbelt.ListIndex = ClothesUtils.GUNBELT_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Gunbelt"].ToString())) + 1;
                    }
                    if (ClothesUtils.BELT_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Belt"].ToString())) != -1)
                    {
                        mListBelt.ListIndex = ClothesUtils.BELT_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Belt"].ToString())) + 1;
                    }
                    if (ClothesUtils.BUCKLE_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Buckle"].ToString())) != -1)
                    {
                        mListBuckle.ListIndex = ClothesUtils.BUCKLE_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Buckle"].ToString())) + 1;
                    }
                    if (ClothesUtils.HOLSTERS_S_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Holster"].ToString())) != -1)
                    {
                        mListSHolsters.ListIndex = ClothesUtils.HOLSTERS_S_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Holster"].ToString())) + 1;
                    }
                    if (ClothesUtils.PANTS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Pant"].ToString())) != -1)
                    {
                        mListPants.ListIndex = ClothesUtils.PANTS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Pant"].ToString())) + 1;
                    }
                    if (ClothesUtils.SKIRTS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Skirt"].ToString())) != -1)
                    {
                        mListSkirts.ListIndex = ClothesUtils.SKIRTS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Skirt"].ToString())) + 1;
                    }
                    if (ClothesUtils.CHAPS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Chap"].ToString())) != -1)
                    {
                        mListChaps.ListIndex = ClothesUtils.CHAPS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Chap"].ToString())) + 1;
                    }
                    if (ClothesUtils.BOOTS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Boots"].ToString())) != -1)
                    {
                        mListBoots.ListIndex = ClothesUtils.BOOTS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Boots"].ToString())) + 1;
                    }
                    if (ClothesUtils.SPURS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Spurs"].ToString())) != -1)
                    {
                        mListSpurs.ListIndex = ClothesUtils.SPURS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Spurs"].ToString())) + 1;
                    }
                    if (ClothesUtils.SPATS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Spats"].ToString())) != -1)
                    {
                        mListSpats.ListIndex = ClothesUtils.SPATS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Spats"].ToString())) + 1;
                    }
                    if (ClothesUtils.GAUNTLETS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Gauntlets"].ToString())) != -1)
                    {
                        mListGauntlets.ListIndex = ClothesUtils.GAUNTLETS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Gauntlets"].ToString())) + 1;
                    }
                    if (ClothesUtils.LOADOUTS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Loadouts"].ToString())) != -1)
                    {
                        mListLoadouts.ListIndex = ClothesUtils.LOADOUTS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Loadouts"].ToString())) + 1;
                    }
                    if (ClothesUtils.ACCESSORIES_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Accessories"].ToString())) != -1)
                    {
                        mListAccessories.ListIndex = ClothesUtils.ACCESSORIES_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Accessories"].ToString())) + 1;
                    }
                    if (ClothesUtils.SATCHELS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Satchels"].ToString())) != -1)
                    {
                        mListSatchels.ListIndex = ClothesUtils.SATCHELS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Satchels"].ToString())) + 1;
                    }
                    if (ClothesUtils.GUNBELTACCS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["GunbeltAccs"].ToString())) != -1)
                    {
                        mListGunbeltAccs.ListIndex = ClothesUtils.GUNBELTACCS_FEMALE.IndexOf(Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["GunbeltAccs"].ToString())) + 1;
                    }
                }
            };

            mainMenu.OnIndexChange += (_menu, _oldItem, _newItem, _oldIndex, _newIndex) =>
            {
                Debug.WriteLine($"OnIndexChange: [{_menu}, {_oldItem}, {_newItem}, {_oldIndex}, {_newIndex}]");
            };

            double totalCost = 0;

            mainMenu.OnListIndexChange += (_menu, _listItem, _oldIndex, _newIndex, _itemIndex) =>
            {

                // Code in here would get executed whenever the selected value of a list item changes (when left/right key is pressed).
                Debug.WriteLine($"Cambios del menu: [{_menu}, {_listItem}, {_oldIndex}, {_newIndex}, {_itemIndex}]");

                switch (_itemIndex)
                {
                    // New System more simplificated
                    case 0:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x9925C067, "Hat", ClothesUtils.HATS_MALE, ClothesUtils.HATS_FEMALE);
                        break;
                    case 1:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x5E47CA6, "EyeWear", ClothesUtils.EYEWEAR_MALE, ClothesUtils.EYEWEAR_FEMALE);
                        break;
                    case 2:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x5FC29285, "NeckWear", ClothesUtils.NECKWEAR_MALE, ClothesUtils.NECKWEAR_FEMALE);
                        break;
                    case 3:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x7505EF42, "Mask", ClothesUtils.MASK_MALE, ClothesUtils.MASK_FEMALE);
                        break;
                    case 4:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x7A96FACA, "NeckTies", ClothesUtils.NECKTIES_MALE, ClothesUtils.NECKTIES_FEMALE);
                        break;
                    case 5:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x2026C46D, "Shirt", ClothesUtils.SHIRTS_MALE, ClothesUtils.SHIRTS_FEMALE);
                        break;
                    case 6:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x877A2CF7, "Suspender", ClothesUtils.SUSPENDERS_MALE, ClothesUtils.SUSPENDERS_FEMALE);
                        break;
                    case 7:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x485EE834, "Vest", ClothesUtils.VEST_MALE, ClothesUtils.VEST_FEMALE);
                        break;
                    case 8:
                        mListCoatsClosed.ListIndex = 0;
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, 0, 0x0662AC34, "CoatClosed", ClothesUtils.COATS_CLOSED_MALE, ClothesUtils.COATS_CLOSED_FEMALE);
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0xE06D30CE, "Coat", ClothesUtils.COATS_MALE, ClothesUtils.COATS_FEMALE);
                        break;
                    case 9:
                        mListCoats.ListIndex = 0;
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, 0, 0xE06D30CE, "Coat", ClothesUtils.COATS_MALE, ClothesUtils.COATS_FEMALE);
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x0662AC34, "CoatClosed", ClothesUtils.COATS_CLOSED_MALE, ClothesUtils.COATS_CLOSED_FEMALE);
                        break;
                    case 10:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0xAF14310B, "Poncho", ClothesUtils.PONCHOS_MALE, ClothesUtils.PONCHOS_FEMALE);
                        break;
                    case 11:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x3C1A74CD, "Cloak", ClothesUtils.CLOAK_MALE, ClothesUtils.CLOAK_FEMALE);
                        break;
                    case 12:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0xEABE0032, "Glove", ClothesUtils.GLOVES_MALE, ClothesUtils.GLOVES_FEMALE);
                        break;
                    case 13:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x7A6BBD0B, "RingRh", ClothesUtils.RINGS_RH_MALE, ClothesUtils.RINGS_RH_FEMALE);
                        break;
                    case 14:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0xF16A1D23, "RingLh", ClothesUtils.RINGS_LH_MALE, ClothesUtils.RINGS_LH_FEMALE);
                        break;
                    case 15:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x7BC10759, "Bracelet", ClothesUtils.BRACELETS_MALE, ClothesUtils.BRACELETS_FEMALE);
                        break;
                    case 16:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x9B2C8B89, "Gunbelt", ClothesUtils.GUNBELT_MALE, ClothesUtils.GUNBELT_FEMALE);
                        break;
                    case 17:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0xA6D134C6, "Belt", ClothesUtils.BELT_MALE, ClothesUtils.BELT_FEMALE);
                        break;
                    case 18:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0xFAE9107F, "Buckle", ClothesUtils.BUCKLE_MALE, ClothesUtils.BUCKLE_FEMALE);
                        break;
                    case 19:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0xB6B6122D, "Holster", ClothesUtils.HOLSTERS_S_MALE, ClothesUtils.HOLSTERS_S_FEMALE);
                        break;
                    case 20:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x1D4C528A, "Pant", ClothesUtils.PANTS_MALE, ClothesUtils.PANTS_FEMALE);
                        break;
                    case 21:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0xA0E3AB7F, "Skirt", ClothesUtils.SKIRTS_FEMALE, ClothesUtils.SKIRTS_FEMALE);
                        break;
                    case 22:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x3107499B, "Chap", ClothesUtils.CHAPS_MALE, ClothesUtils.CHAPS_FEMALE);
                        break;
                    case 23:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x777EC6EF, "Boots", ClothesUtils.BOOTS_MALE, ClothesUtils.BOOTS_FEMALE);
                        break;
                    case 24:
                        mListSpats.ListIndex = 0;
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, 0, 0x514ADCEA, "Spats", ClothesUtils.SPATS_MALE, ClothesUtils.SPATS_FEMALE);
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x18729F39, "Spurs", ClothesUtils.SPURS_MALE, ClothesUtils.SPURS_FEMALE);
                        break;
                    case 25:
                        mListSpurs.ListIndex = 0;
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, 0, 0x18729F39, "Spurs", ClothesUtils.SPURS_MALE, ClothesUtils.SPURS_FEMALE);
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x514ADCEA, "Spats", ClothesUtils.SPATS_MALE, ClothesUtils.SPATS_FEMALE);
                        break;
                    case 26:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x91CE9B20, "Gauntlets", ClothesUtils.GAUNTLETS_MALE, ClothesUtils.GAUNTLETS_FEMALE);
                        break;
                    case 27:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x83887E88, "Loadouts", ClothesUtils.LOADOUTS_MALE, ClothesUtils.LOADOUTS_FEMALE);
                        break;
                    case 28:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x79D7DF96, "Accessories", ClothesUtils.ACCESSORIES_MALE, ClothesUtils.ACCESSORIES_FEMALE);
                        break;
                    case 29:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0x94504D26, "Satchels", ClothesUtils.SATCHELS_MALE, ClothesUtils.SATCHELS_FEMALE);
                        break;
                    case 30:
                        Utils.Commands.SetPlayerComponent(Utils.Commands.model, _newIndex, 0xF1542D11, "GunbeltAccs", ClothesUtils.GUNBELTACCS_MALE, ClothesUtils.GUNBELTACCS_FEMALE);
                        break;
                }

                totalCost = 0;

                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Hat"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Hat"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costHats"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["EyeWear"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["EyeWear"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costEyeWear"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["NeckWear"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["NeckWear"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costNeckWear"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Mask"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Mask"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costMask"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["NeckTies"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["NeckTies"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costNeckTies"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Shirt"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Shirt"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costShirt"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Suspender"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Suspender"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costSuspender"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Vest"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Vest"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costVest"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Coat"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Coat"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costCoat"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["CoatClosed"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["CoatClosed"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costCoatClosed"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Poncho"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Poncho"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costPoncho"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Cloak"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Cloak"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costCloak"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Glove"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Glove"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costGlove"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["RingRh"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["RingRh"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costRingRh"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["RingLh"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["RingLh"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costRingLh"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Bracelet"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Bracelet"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costBracelet"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Gunbelt"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Gunbelt"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costGunbelt"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Belt"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Belt"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costBelt"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Buckle"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Buckle"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costBuckle"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Holster"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Holster"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costHolster"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Pant"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Pant"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costPant"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Skirt"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Skirt"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costSkirt"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Chap"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Chap"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costChap"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Boots"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Boots"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costBoots"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Spurs"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Spurs"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costSpurs"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Spats"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Spats"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costSpats"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Gauntlets"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Gauntlets"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costGauntlets"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Loadouts"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Loadouts"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costLoadouts"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Accessories"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Accessories"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costAccessories"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["Satchels"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["Satchels"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costSatchels"].ToString());
                }
                if (Utils.Commands.ConvertValue(Utils.Commands.ClothesDB["GunbeltAccs"].ToString()) != Utils.Commands.ConvertValue(Utils.Commands.clothesPlayer["GunbeltAccs"].ToString()))
                {
                    totalCost += double.Parse(GetConfig.Config["costGunbeltAccs"].ToString());
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

            mainMenu.OnItemSelect += (_menu, _item, _index) =>
            {
                // Code in here would get executed whenever an item is pressed.
                Debug.WriteLine($"OnItemSelect: [{_menu}, {_item}, {_index}]");
                if (_index == 32)
                {
                    Utils.Commands.isBuy = true;
                    Utils.Commands.FinishBuy(Utils.Commands.isBuy, totalCost);
                    mainMenu.CloseMenu();
                }
            };

            mainMenu.OnMenuClose += async (_menu) =>
            {
                // Code in here gets triggered whenever the menu is closed.
                Debug.WriteLine($"OnMenuClose: [{_menu}]");
                if (!Utils.Commands.isBuy)
                {
                    Utils.Commands.startBuyCloths(Utils.Commands.isBuy);
                }
            };

        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return mainMenu;
        }
    }
}
