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
	}
}
