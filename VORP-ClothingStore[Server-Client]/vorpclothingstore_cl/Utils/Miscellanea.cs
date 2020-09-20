using CitizenFX.Core;
using CitizenFX.Core.Native;
using System.Threading.Tasks;

namespace vorpclothingstore_cl.Utils
{
    class Miscellanea : BaseScript
    {
        public static async Task<uint> GetHash(string model)
        {
            uint hash = (uint)API.GetHashKey(model);
            if (API.IsModelValid(hash))
            {
                API.RequestModel(hash, true);
                while (!API.HasModelLoaded(hash) || !API.HasCollisionForModelLoaded(hash))
                {
                    await BaseScript.Delay(200);
                }

            }
            else
            {
                Debug.WriteLine($"Model {model} is not valid!");
            }
            return hash;
        }
        public static async Task<uint> GetHash(int hash)
        {
            if (API.IsModelValid((uint)hash))
            {
                API.RequestModel((uint)hash, true);
                while (!API.HasModelLoaded((uint)hash))
                {
                    await BaseScript.Delay(200);
                }
                return (uint)hash;
            }
            else
            {
                Debug.WriteLine($"Model {hash} is not valid!");
                return (uint)hash;
            }
        }

        public static async Task<uint> GetHash(uint hash)
        {
            if (API.IsModelValid((uint)hash))
            {
                API.RequestModel((uint)hash, true);
                while (!API.HasModelLoaded((uint)hash))
                {
                    await BaseScript.Delay(200);
                }
                return (uint)hash;
            }
            else
            {
                Debug.WriteLine($"Model {hash} is not valid!");
                return (uint)hash;
            }
        }
    }
}
