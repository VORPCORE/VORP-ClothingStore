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
        private static Menu outfitsSubMenu = new Menu("", GetConfig.Langs["SubTitleMenuOutfits"]);
        private static bool setupDone = false;

        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(outfitsMenu);

            MenuController.EnableMenuToggleKeyOnController = false;
            MenuController.MenuToggleKey = (Control)0;


            //Outfits
            MenuController.AddSubmenu(outfitsMenu, outfitsSubMenu);

            MenuItem subMenuOutfitUseBtn = new MenuItem(GetConfig.Langs["TitleMenuOutfitsUseBtn"], GetConfig.Langs["TitleMenuOutfitsUseBtn"])
            {
                RightIcon = MenuItem.Icon.TICK
            };

            MenuItem subMenuOutfitDeleteBtn = new MenuItem(GetConfig.Langs["TitleMenuOutfitsDeleteBtn"], GetConfig.Langs["TitleMenuOutfitsDeleteBtn"])
            {
                
            };

            outfitsSubMenu.AddMenuItem(subMenuOutfitUseBtn);
            outfitsSubMenu.AddMenuItem(subMenuOutfitDeleteBtn);

            outfitsMenu.OnMenuOpen += (_menu) => 
            {
                outfitsMenu.ClearMenuItems();
                foreach (var a in Utils.Commands.MyOutfits)
                {
                    MenuItem _itemMenu = new MenuItem(a.Value.Item1)
                    {

                    };
                    outfitsMenu.AddMenuItem(_itemMenu);
                    MenuController.BindMenuItem(outfitsMenu, outfitsSubMenu, _itemMenu);
                }
            };

            outfitsSubMenu.OnItemSelect += async (_menu, _item, _index) =>
            {
                if(_index == 0)
                {
                    Utils.Commands.SetOutfit(outfitsMenu.CurrentIndex);
                }
                else
                {
                    outfitsMenu.RemoveMenuItem(outfitsMenu.CurrentIndex);
                    Utils.Commands.DeleteOutfit(outfitsMenu.CurrentIndex);
                    MenuController.CloseAllMenus();
                    MainMenu.GetMenu().OpenMenu();
                }
            };

        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return outfitsMenu;
        }
    }
}
