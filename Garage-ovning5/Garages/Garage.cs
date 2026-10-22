using Garage_ovning5.Vehicles;
using System;
using System.Collections.Generic;

namespace Garage_ovning5.Garages
{
	internal class Garage
	{

		private readonly int _capacity;
		public const int MaximumCapacity = 2000;

		private readonly List<Vehicle> _vehicles;

		public Garage(int capacity)
		{
			if (capacity < 1 || capacity > MaximumCapacity)
			{
				throw new ArgumentOutOfRangeException(nameof(capacity), $"Capacity must be between 1 and {MaximumCapacity}");
			}

			_capacity = capacity;
			_vehicles = new List<Vehicle>(capacity);
		}


		public bool ParkVehicle(Vehicle vehicle)
		{
			if (!HasFreeParkingSpot())
			{
				return false;
			}

			if (IsLicensePlateTaken(vehicle.LicensePlateNumber))
			{
				return false;
			}
			
			_vehicles.Add(vehicle);
			return true;

		}

		public bool HasFreeParkingSpot()
		{
			if (_vehicles.Count < _capacity)
			{
				return true;
			}

			return false;
		}

		public bool IsLicensePlateTaken(string licensePlateNumber)
		{
			string searchedPlate = licensePlateNumber.Trim().ToUpper();

			foreach (Vehicle vehicle in _vehicles)
			{
				if (vehicle.LicensePlateNumber == searchedPlate)
				{
					return true;
				}
			}

			return false;
		}

		public Vehicle? RemoveVehicle(string licensePlateNumber)
		{
			Vehicle? vehicle = SearchByLicensePlate(licensePlateNumber);

			if (vehicle != null)
			{
				_vehicles.Remove(vehicle);
			}

			return vehicle;
		}

		public void ListAllVehicles()
		{
			bool vehicleFound = false;

			foreach (Vehicle vehicle in _vehicles)
			{
				Console.WriteLine($"{vehicle}\n");
				vehicleFound = true;
			}

			if (!vehicleFound)
			{
				Console.WriteLine("There are no vehicles parked in the garage.\n");
			}
		}

		public int CountOfVehicleType(VehicleType type)
		{
			int count = 0;

			foreach (Vehicle vehicle in _vehicles)
			{
				if (vehicle.VehicleType == type)
				{
					count++;
				}
			}

			return count;
		}

		public Vehicle? SearchByLicensePlate(string licensePlateNumber)
		{
			string searchedPlate = licensePlateNumber.Trim().ToUpper();

			for (int i = 0; i < _vehicles.Count; i++)
			{
				Vehicle vehicle = _vehicles[i];

				if (vehicle.LicensePlateNumber == searchedPlate)
				{
					return vehicle;
				}

			}

			return null;
		}

		private static bool MatchesSearch(Vehicle vehicle, VehicleColor? color, int? wheels, VehicleType? type)
		{
			if (color != null && vehicle.Color != color)
			{
				return false;
			}

			if (wheels != null && vehicle.NumberOfWheels != wheels)
			{
				return false;
			}

			if (type != null && vehicle.VehicleType != type)
			{
				return false;
			}

			return true;

		}

		public List<Vehicle>? SearchByProperties(VehicleColor? color, int? numberOfWheels, VehicleType? vehicleType)
		{
			int matchedSearchedCount = 0;

			for (int i = 0; i < _vehicles.Count; i++)
			{
				Vehicle vehicle = _vehicles[i];

				if (MatchesSearch(vehicle, color, numberOfWheels, vehicleType))
				{
					matchedSearchedCount++;
				}
			}

			if (matchedSearchedCount == 0)
			{
				return null;
			}

			List<Vehicle> resultArray = new List<Vehicle>(matchedSearchedCount);

			int resultIndex = 0;

			for (int j = 0; j < _vehicles.Count; j++)
			{
				Vehicle vehicle = _vehicles[j];

				if (MatchesSearch(vehicle, color, numberOfWheels, vehicleType))
				{
					resultArray.Add(vehicle);

					resultIndex++;
				}
			}
			return resultArray;
		}
	}
}
