using Garage_ovning5.Vehicles;
using System;
using System.Collections.Generic;

namespace Garage_ovning5.Garages
{
	internal class Garage
	{

		public const int MaximumCapacity = 2000;
		private readonly int _capacity;
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

		public int Capacity
		{
			get { return _capacity; }
		}

		public int FreeSpaces
		{
			get { return _capacity - _vehicles.Count; }
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
			string searchedPlate = Vehicle.NormalizeLicensePlate(licensePlateNumber);

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

			if (vehicle is not null)
			{
				_vehicles.Remove(vehicle);
			}

			return vehicle;
		}

		public void ListAllVehicles()
		{
			if (_vehicles.Count == 0)
			{
				Console.WriteLine("There are no vehicles parked in the garage.\n");
				return;
			}

			foreach (Vehicle vehicle in _vehicles)
			{
				Console.WriteLine($"{vehicle}\n");
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
			string searchedPlate = Vehicle.NormalizeLicensePlate(licensePlateNumber);

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
			if (color is not null && vehicle.Color != color)
			{
				return false;
			}

			if (wheels is not null && vehicle.NumberOfWheels != wheels)
			{
				return false;
			}

			if (type is not null && vehicle.VehicleType != type)
			{
				return false;
			}

			return true;

		}

		public List<Vehicle> SearchByProperties(VehicleColor? color, int? numberOfWheels, VehicleType? vehicleType)
		{
			List<Vehicle> matches = new List<Vehicle>();

			foreach (Vehicle vehicle in _vehicles)
			{
				if (MatchesSearch(vehicle, color, numberOfWheels, vehicleType))
				{
					matches.Add(vehicle);
				}
			}

			return matches;
		}
	}
}
