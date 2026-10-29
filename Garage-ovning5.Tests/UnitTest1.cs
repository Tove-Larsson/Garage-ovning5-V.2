using Garage_ovning5.Vehicles;

namespace Garage_ovning5.Tests
{
	public class VehicleTests
	{
		[Theory]
		[InlineData("abc123", "ABC123")]
		[InlineData(" abc123 ", "ABC123")]
		[InlineData("ABC123", "ABC123")]
		[InlineData("aBc12d", "ABC12D")]
		[InlineData(null, "")]
		public void NormalizeLicensePlate_TrimsAndUppercases(string? input, string expected)
		{
			//Act
			string result = Vehicle.NormalizeLicensePlate(input);

			//Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(-22)]
		public void Constructor_WithNegativeNumberOfWheels_Throws(int wheels)
		{
			Assert.Throws<ArgumentException>(() =>
				new Car("ABC123", VehicleColor.Red, wheels, FuelType.Diesel));
		}

		[Theory]
		[InlineData(0)]
		[InlineData(4)]
		public void Constructor_WithValidNumberOfWheels_StoresValue(int wheels)
		{
			Car car = new Car("ABC123", VehicleColor.Red, wheels, FuelType.Diesel);

			Assert.Equal(wheels, car.NumberOfWheels);
		}
	}
}
