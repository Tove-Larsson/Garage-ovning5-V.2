using Garage_ovning5.Garages;
using Garage_ovning5.Vehicles;
using System;
using System.Collections.Generic;

namespace Garage_ovning5.UI
{
	internal class Menu
	{
		private Garage? _garage;

		public void MainMenu()
		{
			bool running = true;

			while (running)
			{
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine("\n========= Main Menu =========\n");
				Console.ResetColor();
				Console.WriteLine("Please select what you want to do!\n");

				Console.WriteLine("1. Create a garage");
				Console.WriteLine("2. Create a garage with pre-parked vehicles");
				Console.WriteLine("0. Exit program");

				string? selectedChoice = Console.ReadLine();

				switch (selectedChoice)
				{
					case "1":
						HandleCreateGarage();
						break;
					case "2":
						HandleCreatePreParkedGarage();
						break;
					case "0":
						Console.WriteLine("You chose to end the application, goodbye!");
						running = false;
						break;
					default:
						Console.WriteLine("Invalid input, please try again");
						break;
				}
			}
		}

		private void HandleCreateGarage()
		{

			bool garageCreated = false;

			while (!garageCreated)
			{
				Console.WriteLine("\nHow many parking spaces should the garage have?\n");

				if (int.TryParse(Console.ReadLine(), out int parkingSpaces) && parkingSpaces > 0 && parkingSpaces <= Garage.MaximumCapacity)
				{
					Console.WriteLine($"\nA garage has been created with {parkingSpaces} parking spaces!\n");
					_garage = new Garage(parkingSpaces);
					garageCreated = true;
				}
				else
				{
					Console.WriteLine($"Invalid input, please enter a number between 1 and {Garage.MaximumCapacity}");
				}
			}
			GarageMenu();
		}

		private void HandleCreatePreParkedGarage()
		{
			Console.WriteLine("Create a pre-parked garage chosen");
			Console.WriteLine("\nOne vehicle per vehicle type has been created, plus 5 extra parking spaces!\n");

			Garage preParkedGarage = new Garage(10);

			preParkedGarage.ParkVehicle(new Car("Abc123", VehicleColor.Red, 4, FuelType.Gasoline));
			preParkedGarage.ParkVehicle(new Motorcycle("def345", VehicleColor.Blue, 2, 1000));
			preParkedGarage.ParkVehicle(new Bus("Did865", VehicleColor.Yellow, 4, 30));
			preParkedGarage.ParkVehicle(new Airplane("bob261", VehicleColor.Green, 3, 2));
			preParkedGarage.ParkVehicle(new Boat("hej729", VehicleColor.Silver, 200.5));

			_garage = preParkedGarage;
			GarageMenu();

		}

		private void GarageMenu()
		{
			bool running = true;

			while (running)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("\n========= Garage menu =========\n");
				Console.ResetColor();
				Console.WriteLine("Please select what you want to do!\n");

				if (_garage is not null)
				{
					Console.WriteLine($"The garage has {_garage.FreeSpaces} free spaces, {_garage.Capacity} in total\n");
				}

				Console.WriteLine("1. Park a vehicle");
				Console.WriteLine("2. Retrieve a vehicle");
				Console.WriteLine("3. List all parked vehicles");
				Console.WriteLine("4. List parked vehicle types");
				Console.WriteLine("5. Search for a vehicle with license plate number");
				Console.WriteLine("6. Search for a vehicle by its properties, one or multiple");
				Console.WriteLine("0. Back to main menu");

				string? selectedChoice = Console.ReadLine();

				switch (selectedChoice)
				{
					case "1":
						HandleParkVehicle();
						break;
					case "2":
						HandleRetrieveVehicle();
						break;
					case "3":
						HandleListParkedVehicles();
						break;
					case "4":
						HandleListVehicleTypes();
						break;
					case "5":
						HandleSearchByLicensePlate();
						break;
					case "6":
						HandleSearchByProperties();
						break;
					case "0":
						Console.WriteLine("Back to main menu!");
						running = false;
						break;
					default:
						Console.WriteLine("Invalid input, please try again");
						break;
				}

			}
		}

		private void HandleParkVehicle()
		{
			if (_garage == null)
			{
				Console.WriteLine("No garage exists");
				return;
			}

			if (!_garage.HasFreeParkingSpot())
			{
				Console.WriteLine("\nThe garage is full!\n");
				return;
			}

			string licensePlateNumber = VehicleInput.ReadLicensePlateNumber();

			if (_garage.IsLicensePlateTaken(licensePlateNumber))
			{
				Console.WriteLine($"\nA vehicle with licence plate number: {licensePlateNumber} already exists\n");
				return;
			}

			VehicleColor color = VehicleInput.ReadVehicleColor();

			VehicleType? type = VehicleInput.ReadVehicleType();
			if (type == null)
			{
				Console.WriteLine("\nParking cancelled\n");
				return;
			}


			try
			{

				Vehicle? vehicle = VehicleCreator.CreateVehicle(licensePlateNumber, color, type.Value);
				if (vehicle == null)
				{
					Console.WriteLine("\nParking cancelled\n");
					return;
				}

				bool parked = _garage.ParkVehicle(vehicle);

				if (parked)
				{
					Console.WriteLine($"\nThe {type} with {vehicle.LicensePlateNumber} license plate number was parked successfully!\n");
				}
				else
				{
					Console.WriteLine("\nThe vehicle could not be parked\n");
				}

			}
			catch (ArgumentException ex)
			{
				Console.WriteLine(ex.Message);
				return;
			}
		
		}

		private void HandleRetrieveVehicle()
		{
			if (_garage == null)
			{
				Console.WriteLine("No garage exists");
				return;
			}

			string licensePlateNumber = InputHelper.ReadString("\nPlease enter the license plate number of the vehicle you want to retrieve\n");

			Vehicle? retrievedVehicle = _garage.RemoveVehicle(licensePlateNumber);

			if (retrievedVehicle != null)
			{
				Console.WriteLine($"\nRetrieval of vehicle with license plate number: {retrievedVehicle.LicensePlateNumber} was successful!\n");
			}
			else
			{
				Console.WriteLine($"\nNo vehicle with license plate {licensePlateNumber} was found.\n");
			}
		}

		private void HandleListParkedVehicles()
		{
			if (_garage == null)
			{
				Console.WriteLine("No garage exists");
				return;
			}

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("\n========= Parked vehicles =========\n");
			Console.ResetColor();

			_garage.ListAllVehicles();
		}

		private void HandleListVehicleTypes()
		{
			if ( _garage == null)
			{
				Console.WriteLine("No garage exists");
				return;
			}

			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("\n========= Parked vehicle types =========\n");
			Console.ResetColor();

			Console.WriteLine($"Cars: {_garage.CountOfVehicleType(VehicleType.Car)}");
			Console.WriteLine($"Motorcycles: {_garage.CountOfVehicleType(VehicleType.Motorcycle)}");
			Console.WriteLine($"Buses: {_garage.CountOfVehicleType(VehicleType.Bus)}");
			Console.WriteLine($"Airplanes: {_garage.CountOfVehicleType(VehicleType.Airplane)}");
			Console.WriteLine($"Boats: {_garage.CountOfVehicleType(VehicleType.Boat)}\n");

		}

		private void HandleSearchByLicensePlate()
		{
			if (_garage == null)
			{
				Console.WriteLine("No garage exists");
				return;
			}
			string searchedLicensePlate = InputHelper.ReadString("Please enter a licence plate number to see if it is parked!");

			Vehicle? vehicle = _garage.SearchByLicensePlate(searchedLicensePlate);

			if (vehicle != null)
			{
				Console.WriteLine($"\nA parked vehicle with {vehicle.LicensePlateNumber} was found!\n");
				Console.WriteLine(vehicle);
			}
			else
			{
				Console.WriteLine($"\nNo vehicle with license plate {searchedLicensePlate} was found.\n");
			}
			
		}

		private void HandleSearchByProperties()
		{
			if (_garage == null)
			{
				Console.WriteLine("No garage exists");
				return;
			}

			VehicleColor? color = VehicleInput.ReadOptionalVehicleColor();

			int? numberOfWheels = VehicleInput.ReadOptionalNumberOfWheels();

			VehicleType? vehicleType = VehicleInput.ReadOptionalVehicleType();

			List<Vehicle> searchedProperties = _garage.SearchByProperties(color, numberOfWheels, vehicleType);

			if (searchedProperties.Count == 0)
			{
				Console.WriteLine("\nNo parked vehicle matched your search\n");
				return;
			}

			Console.WriteLine($"\nFound {searchedProperties.Count} vehicles matching your search!\n");

			foreach (Vehicle vehicle in searchedProperties)
			{
				Console.WriteLine(vehicle);
			}

		}

	}
}
