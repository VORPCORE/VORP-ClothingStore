using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;

namespace vorpclothingstore_sv
{
    public class vorpclothingstore_sv : BaseScript
    {
        public static dynamic CORE;

        public vorpclothingstore_sv()
        {
            EventHandlers["vorpclothingstore:getPlayerCloths"] += new Action<Player>(getPlayerCloths);
            EventHandlers["vorpclothingstore:buyPlayerCloths"] += new Action<Player, double, string, bool, string>(buyPlayerCloths);
            EventHandlers["vorpclothingstore:setOutfit"] += new Action<Player, string>(LoadOutfit);
            EventHandlers["vorpclothingstore:deleteOutfit"] += new Action<Player, int>(DeleteOutfit);
            TriggerEvent("getCore", new Action<dynamic>((dic) => 
            {
                CORE = dic;
            }));
        }

        private void DeleteOutfit([FromSource] Player source, int id)
        {
            string sid = "steam:" + source.Identifiers["steam"];

            Exports["ghmattimysql"].execute("DELETE FROM outfits WHERE identifier=? AND id=?", new object[] { sid, id });
        }

        private void LoadOutfit([FromSource]Player source,  string json)
        {
            TriggerEvent("vorpcharacter:setPlayerCompChange", int.Parse(source.Handle), json);
        }

        private void buyPlayerCloths([FromSource]Player source, double totalCost, string jsonCloths, bool saveOut, string nameOut)
        {
            int _source = int.Parse(source.Handle);

            string sid = "steam:" + source.Identifiers["steam"];

            dynamic UserCharacter = CORE.getUser(_source).getUsedCharacter;
           
            double money = UserCharacter.money;

            if (totalCost <= money)
            {
                UserCharacter.removeCurrency(0, totalCost);

                TriggerEvent("vorpcharacter:setPlayerCompChange", _source, jsonCloths);

                int charIdentifier = UserCharacter.charIdentifier;

                if (saveOut)
                {
                    Exports["ghmattimysql"].execute($"INSERT INTO outfits (identifier,charidentifier,title,comps) VALUES (?,?,?,?)", new object[] { sid, charIdentifier, nameOut, jsonCloths });
                }

                source.TriggerEvent($"vorpclothingstore:startBuyCloths", true);
                source.TriggerEvent("vorp:Tip", LoadConfig.Langs["SuccessfulBuy"] + $" ${totalCost}", 4000);
            }
            else
            {
                source.TriggerEvent("vorp:Tip", LoadConfig.Langs["NoMoney"], 4000);
                source.TriggerEvent($"vorpclothingstore:startBuyCloths", false);
            }

        }

        private void getPlayerCloths([FromSource]Player source)
        {
            dynamic UserCharacter = CORE.getUser(int.Parse(source.Handle)).getUsedCharacter;
            int _source = int.Parse(source.Handle);
            int charIdentifier = UserCharacter.charIdentifier;

            string comps = UserCharacter.comps;
            string skin = UserCharacter.skin;

            source.TriggerEvent($"{API.GetCurrentResourceName()}:LoadYourCloths", comps, skin);

            string sid = "steam:" + source.Identifiers["steam"];

            Exports["ghmattimysql"].execute("SELECT * FROM outfits WHERE `identifier` = ? AND `charidentifier` = ?", new object[] { sid, charIdentifier }, new Action<dynamic>((result) =>
            {
                if (result.Count != 0)
                {
                    source.TriggerEvent($"{API.GetCurrentResourceName()}:LoadYourOutfits", result);
                }
            }));

        }
    }
}
