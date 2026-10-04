using Alta;
using Alta.Caves;
using Alta.Networking;
using HarmonyLib;
using MelonLoader;
using System.Collections;
using System.Reflection;
using UnityEngine;
using CustomDistributionAPI;

[assembly: MelonInfo(typeof(DifferentRopeMaterials.Core), "DifferentRopeMaterials", "1.0.0", "CGNik", null)]
[assembly: MelonGame("Alta", "A Township Tale")]

namespace DifferentRopeMaterials
{
    public class Core : MelonMod
    {
        public static Distribution ropeMaterialDistribution;

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
        }

        public override void OnLateInitializeMelon()
        {
            ropeMaterialDistribution = GameObject.Instantiate(Distribution.All.Where(dist => dist.Hash == 49220u).First());
            typeof(HashedGeneralValue<Distribution>).GetField("hash", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ropeMaterialDistribution, 49224);
            ropeMaterialDistribution.name = "Rope Material Distribution";
            CustomDistributionAPI.Core.RegisterDistribution(ropeMaterialDistribution);
            Distribution.Item item_rope = new Distribution.Item();
            typeof(Distribution.BaseItem).GetField("topic", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(item_rope, PhysicalMaterial.All.Where(mat => mat.Hash == 35204u).First());
            typeof(Distribution.BaseItem).GetField("baseValue", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(item_rope, 1f);
            typeof(Distribution.BaseItem).GetField("noAttributeValue", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(item_rope, 1f);
            typeof(Distribution.BaseItem).GetField("multipliers", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(item_rope, new AttributeCurveRange[] { });
            ropeMaterialDistribution.GetType().GetField("items", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ropeMaterialDistribution, new List<Distribution.Item> { item_rope });
            GameObject ropeClump = (GameObject)Resources.Load("network prefabs/crafting/crafting materials prefabs/Rope Clump");
            PhysicalMaterialPart physicalMaterialPart = ropeClump.GetComponent<PhysicalMaterialPart>();
            typeof(PhysicalMaterialPart).GetField("materialDistribution", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(physicalMaterialPart, Core.ropeMaterialDistribution);
        }
    }
}