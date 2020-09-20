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
    class OutfitsMenu
    {
        private static Menu outfitsMenu = new Menu(GetConfig.Langs["TitleMenuOutfits"], GetConfig.Langs["SubTitleMenuOutfits"]);
        private static bool setupDone = false;

        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(outfitsMenu);

            MenuController.EnableMenuToggleKeyOnController = false;
            MenuController.MenuToggleKey = (Control)0;

            outfitsMenu.OnMenuOpen += (_menu) => 
            {
                outfitsMenu.ClearMenuItems();
                foreach (var a in Utils.Commands.MyOutfits)
                {
                    outfitsMenu.AddMenuItem(new MenuItem(a.Value.Item1));
                }
            };

            outfitsMenu.OnItemSelect += async (_menu, _item, _index) =>
            {
                Utils.Commands.SetOutfit(_index);
            };
            

        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return outfitsMenu;
        }
    }
}
