using Utilities;

namespace Game.SpinWheelSystem.Runtime.Scripts.Data
{
	public static class SpinWheelSystemLogicConfiguration
	{
		public const int SpinWheelItemsCount = 8;
		public const int SpinWheelZonesCount = 120;
		public const int SuperZoneInterval = 30;
		public const int SafeZoneInterval = 5;
		public const int RevivePrice = 200;

		public static readonly RandomWeighedItemBag<SpinWheelItemId> BasicItems = new(new[]
																			 {
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.AviatorGlassesEaster, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.BaseballCapEaster, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.Cash, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.ChestBig, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.ChestBronze, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.ChestSilver, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.ChestSmall, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.ChestStandard, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.Gold, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.MleBayonetEasterTime, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.MleBayonetSummerVice, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.GrenadeM26, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.GrenadeM67, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.HealthShot2NeuroStim, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.HealthShot2Regenerator, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.Molotov, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.ArmorPoints, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.KnifePoints, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.PistolPoints, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.RiflePoints, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.ShotgunPoints, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.SmgPoints, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.SniperPoints, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.VestPoints, 1)
																			 }
																			);

		public static readonly RandomWeighedItemBag<SpinWheelItemId> SuperItems = new(new[]
																					{
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.ChestGold, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.ChestSuper, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.HelmetPumpkin, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.Tier1Shotgun, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.Tier2Mle, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.Tier2Rifle, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.Tier3Shotgun, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.Tier3Smg, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.Tier3Sniper, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.ArmorPoints, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.KnifePoints, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.PistolPoints, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.RiflePoints, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.ShotgunPoints, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.SmgPoints, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.SniperPoints, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItemId>(SpinWheelItemId.VestPoints, 1)
																					}
																				   );

		public static readonly RandomWeighedItemBag<int> BasicItemCounts = new(new[]
																	   {
																		   new RandomWeightedItemBagEntry<int>(5, 1),
																		   new RandomWeightedItemBagEntry<int>(10, 1),
																		   new RandomWeightedItemBagEntry<int>(50, 1),
																		   new RandomWeightedItemBagEntry<int>(100, 1),
																		   new RandomWeightedItemBagEntry<int>(200, 1)
																	   }
																	  );

		public static readonly RandomWeighedItemBag<int> SuperItemCounts = new(new[]
																	   {
																		   new RandomWeightedItemBagEntry<int>(1, 1),
																		   new RandomWeightedItemBagEntry<int>(5, 1),
																		   new RandomWeightedItemBagEntry<int>(10, 1),
																		   new RandomWeightedItemBagEntry<int>(50, 1),
																		   new RandomWeightedItemBagEntry<int>(100, 1),
																		   new RandomWeightedItemBagEntry<int>(200, 1),
																		   new RandomWeightedItemBagEntry<int>(500, 1)
																	   }
																	  );
	}
}
