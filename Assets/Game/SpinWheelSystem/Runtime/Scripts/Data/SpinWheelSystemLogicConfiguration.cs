using Utils.Singleton;

namespace Game.SpinWheelSystem.Runtime.Scripts.Data
{
	public static class SpinWheelSystemLogicConfiguration
	{
		public const int SpinWheelItemsCount = 8;
		public const int SpinWheelZonesCount = 120;
		public const int SuperZoneInterval = 30;
		public const int SafeZoneInterval = 5;

		public static readonly RandomWeighedItemBag<SpinWheelItem> BasicItems = new(new[]
																			 {
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.AviatorGlassesEaster, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.BaseballCapEaster, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.Cash, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.ChestBig, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.ChestBronze, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.ChestSilver, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.ChestSmall, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.ChestStandard, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.Gold, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.MleBayonetEasterTime, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.MleBayonetSummerVice, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.GrenadeM26, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.GrenadeM67, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.HealthShot2NeuroStim, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.HealthShot2Regenerator, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.Molotov, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.ArmorPoints, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.KnifePoints, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.PistolPoints, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.RiflePoints, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.ShotgunPoints, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.SmgPoints, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.SniperPoints, 1),
																				 new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.VestPoints, 1)
																			 }
																			);

		public static readonly RandomWeighedItemBag<SpinWheelItem> SuperItems = new(new[]
																					{
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.ChestGold, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.ChestSuper, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.HelmetPumpkin, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.Tier1Shotgun, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.Tier2Mle, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.Tier2Rifle, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.Tier3Shotgun, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.Tier3Smg, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.Tier3Sniper, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.ArmorPoints, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.KnifePoints, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.PistolPoints, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.RiflePoints, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.ShotgunPoints, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.SmgPoints, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.SniperPoints, 1),
																						new RandomWeightedItemBagEntry<SpinWheelItem>(SpinWheelItem.VestPoints, 1)
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
