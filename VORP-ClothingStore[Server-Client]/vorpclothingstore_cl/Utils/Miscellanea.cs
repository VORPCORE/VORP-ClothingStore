using CitizenFX.Core;
using CitizenFX.Core.Native;
using System.Threading.Tasks;

namespace vorpclothingstore_cl.Utils
{
    class Miscellanea : BaseScript
    {
        public static async Task<bool> LoadModel(uint hash)
        {
            if (Function.Call<bool>(Hash.IS_MODEL_VALID, hash))
            {
                Function.Call(Hash.REQUEST_MODEL, hash);
                while (!Function.Call<bool>(Hash.HAS_MODEL_LOADED, hash))
                {
                    Debug.WriteLine($"Waiting for model {hash} load!");
                    await Delay(100);
                }
                return true;
            }
            else
            {
                Debug.WriteLine($"Model {hash} is not valid!");
                return false;
            }
        }
    }
}
