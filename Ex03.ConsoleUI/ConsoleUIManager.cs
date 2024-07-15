using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic;
using System.Linq;
using System.Text;
using System;
using System.Globalization;

namespace Ex03.GarageLogic
{
    public class ConsoleUIManager
    {
        private readonly Garage r_Garage = new Garage();
        private VehicleBuilder m_vehicleBuilder = new VehicleBuilder();
        private const int k_LicenseLength = 9;
        private short m_NumOfOptions = 0;
        private string m_UserChoose;

        public void RunProgram()
        {
            this.WelcomeMessage();
            while (m_UserChoose != "8")
            {
                this.printMenu();
                this.getUserChoose();
                this.MenuExecution();
            }

            Console.WriteLine("Bye Bye");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }

        public void WelcomeMessage()
        {
            string draw = string.Format(
                @" WELCOME TO:
                                                                                                
           ************         ***               ***********        **********                
           *                   *   *              *                       *                  _______________          
           *                  *     *             *                       *                  |             |
           ***********       *********            ***********             *            ______|             |
           *                *         *                     *             *            |     |             | 
           *               *           *                    *             *            |_____|_____________|
           *              *             *         ***********             *               O              O
 
           ************         ***           *******            ***            ************     ***********
           *                   *   *          *      *          *   *           *                *
           *                  *     *         *********        *     *          *                *
           *       ****      *********        *               *********         *        ***     **********
           *          *     *         *       *  *           *         *        *          *     * 
           *          *    *           *      *   *         *           *       *          *     *
           ************   *             *     *     *      *             *      ************     ***********                               
");

            Console.WriteLine(draw);
        }

        public void MenuExecution()
        {
            switch (m_UserChoose)
            {
                case "1":
                    this.InsertVehicle();
                    break;
                case "2":
                    this.dispalyVehicle();
                    break;
                case "3":
                    this.changeVehicleState();
                    break;
                case "4":
                    this.inflatingToMax();
                    break;
                case "5":
                    this.refulingVehicleGas();
                    break;
                case "6":
                    this.chargeBattery();
                    break;
                case "7":
                    this.displayVehicleByLicense();
                    break;
            }
        }

        private void printMenu()
        {
            StringBuilder sb = new StringBuilder("", 60);
            sb.AppendLine("Please choose one of the following options:");
            sb.AppendLine("---------------------------------------------------");
            sb.AppendLine("1) Inserting a new vehicle to garage.");
            sb.AppendLine("2) Displaying license number of vehicles by filter.");
            sb.AppendLine("3) Change vehicle state.");
            sb.AppendLine("4) Inflating air in wheels.");
            sb.AppendLine("5) Refueling gas in tank.");
            sb.AppendLine("6) Charging battery.");
            sb.AppendLine("7) Displaying vehicle data.");
            sb.AppendLine("8) Exit.");
            sb.AppendLine("----------------------------------------------------");

            Console.WriteLine(sb);
            m_NumOfOptions = 8;
        }

        private void getUserChoose()
        {
            bool checkUserInput = false;
            while (checkUserInput != true)
            {
                try
                {
                    m_UserChoose = Console.ReadLine();
                    if (isUserChooseValid() == true)
                    {
                        checkUserInput = true;
                    }
                }
                catch (Exception ex)
                {
                    if (ex is ValueOutOfRangeException)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    else
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private bool isUserChooseValid()
        {
            bool isValidChoose = false;

            if (m_UserChoose.All(Char.IsDigit) == false)
            {
                throw new FormatException("You must insert only numbers!");
            }
            else
            {
                double userChooseNumber = Convert.ToDouble(m_UserChoose);
                if (1 <= userChooseNumber && userChooseNumber <= m_NumOfOptions)
                {
                    isValidChoose = true;
                }
                if (isValidChoose == false)
                {
                    throw new ValueOutOfRangeException(new Exception(), (float)m_NumOfOptions, (float)1);
                }
            }

            return isValidChoose;
        }

        private void InsertVehicle()
        {
            bool isVehicleValid = false;

            while (isVehicleValid != true)
            {
                this.getLicenseNum();
                try
                {
                    if (r_Garage.IsVehicleExistsInGarage(m_vehicleBuilder.m_LicenseNumber) != true)
                    {
                        m_vehicleBuilder.m_VehicleType = this.getVehicleChooseFromUser();
                        m_vehicleBuilder.m_EnergySource = this.getEnergyChooseFromUser();
                        this.getNameAndPhone();
                        this.getInfoFromUser();
                        r_Garage.InsertNewVehicle(m_vehicleBuilder);
                        isVehicleValid = true;
                        Console.WriteLine("Vehicle added.");
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine($"Vehicle number {m_vehicleBuilder.m_LicenseNumber} is already in the garage");
                        Console.WriteLine($"Switching the state of the vehicle to 'under rapair'");
                        r_Garage.ChangeVehicleState(m_vehicleBuilder.m_LicenseNumber, eVehicleStates.UnderRepair);
                        isVehicleValid = true;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{exception.Message}");
                    break;
                }
            }
        }

        private void getInfoFromUser()
        {
            Console.WriteLine("Please enter model name:");
            m_vehicleBuilder.m_ModelName = Console.ReadLine();
            Console.WriteLine("Please enter wheels manufacturer:");
            m_vehicleBuilder.m_WheelManufacturer = Console.ReadLine();
            Console.WriteLine("Please enter current wheels air pressure:");
            m_vehicleBuilder.m_CurrentTirePressure = this.checkInfo();

            switch (m_vehicleBuilder.m_VehicleType)
            {
                case eVehicleTypes.Motorcycle:
                    Console.WriteLine("Please enter engine volume in CC:");
                    m_vehicleBuilder.m_EngineVolume = (int)Math.Ceiling(this.checkInfo());
                    m_vehicleBuilder.m_LicenseType = this.chooseLicenseType();

                    break;

                case eVehicleTypes.Car:
                    Console.WriteLine("Please enter num of doors:");
                    m_vehicleBuilder.m_Doors = this.chooseNumOfDoors();
                    m_vehicleBuilder.m_Color = this.chooseCarColor();
                    break;

                case eVehicleTypes.Truck:
                    Console.WriteLine("Please enter cargo volume:");
                    m_vehicleBuilder.m_CargoVolume = this.checkInfo();
                    m_vehicleBuilder.m_IsTransportChemicals = this.isTransportChemicals();
                    break;
            }
            switch (m_vehicleBuilder.m_EnergySource)
            {
                case eEnergySource.Gas:
                    Console.WriteLine("Please enter current fuel amount:");
                    m_vehicleBuilder.m_CurrentVehicleEnergy = this.checkInfo();
                    break;

                case eEnergySource.Electric:
                    Console.WriteLine("Please enter current hours amount of battery:");
                    m_vehicleBuilder.m_CurrentVehicleEnergy = this.checkInfo();
                    break;
            }
        }
        // $G$ DSN-001 (-12) The UI must not know specific types and their properties concretely! It means that when adding a new type you'll have to change the code here too!
        private eVehicleTypes getVehicleChooseFromUser()
        {
            m_NumOfOptions = 3;
            StringBuilder sb = new StringBuilder("", 50);
            sb.AppendLine("Select the type of vehicle you want to put in the garage:");
            sb.AppendLine("1) Motorcycle.");
            sb.AppendLine("2) Car.");
            sb.AppendLine("3) Truck.");

            Console.WriteLine(sb);
            this.getUserChoose();

            eVehicleTypes eVehicleType;
            switch (m_UserChoose)
            {
                case "1":
                    eVehicleType = eVehicleTypes.Motorcycle;
                    break;
                case "2":
                    eVehicleType = eVehicleTypes.Car;
                    break;
                case "3":
                    eVehicleType = eVehicleTypes.Truck;
                    break;
                default:
                    eVehicleType = eVehicleTypes.Car;
                    break;
            }

            return eVehicleType;
        }

        private eEnergySource getEnergyChooseFromUser()
        {
            StringBuilder sb = new StringBuilder("", 50);
            sb.AppendLine("Select the type of energy");
            sb.AppendLine("1) Gas");
            sb.AppendLine("2) Electric");

            Console.WriteLine(sb);
            this.getUserChoose();

            eEnergySource eEnergyType;
            switch (m_UserChoose)
            {
                case "1":
                    eEnergyType = eEnergySource.Gas;
                    break;
                case "2":
                    eEnergyType = eEnergySource.Electric;
                    break;
                default:
                    eEnergyType = eEnergySource.Gas;
                    break;
            }

            return eEnergyType;
        }

        private void getNameAndPhone()
        {
            Console.WriteLine("Please enter client's name:");
            m_vehicleBuilder.m_VehicleOwnerName = Console.ReadLine();

            while (m_vehicleBuilder.m_VehicleOwnerName.Length < 3 || m_vehicleBuilder.m_VehicleOwnerName.All(Char.IsLetter) != true)
            {
                Console.WriteLine("Name must be at least 3 letters.");
                m_vehicleBuilder.m_VehicleOwnerName = Console.ReadLine();
            }

            Console.WriteLine("Please enter phone number (05xxxxxxxx):");
            m_vehicleBuilder.m_VehicleOwnerPhone = Console.ReadLine();

            while ((m_vehicleBuilder.m_VehicleOwnerPhone.Length != 10) || (!m_vehicleBuilder.m_VehicleOwnerPhone.All(Char.IsDigit)) || (!m_vehicleBuilder.m_VehicleOwnerPhone.StartsWith("05")))
            {
                Console.WriteLine("Phone number is isnvalid. Try again:");
                m_vehicleBuilder.m_VehicleOwnerPhone = Console.ReadLine();
            }
        }

        private void getLicenseNum()
        {
            bool checkUserInput = false;
            Console.WriteLine("Please enter license number:");

            while (checkUserInput != true)
            {
                try
                {
                    m_vehicleBuilder.m_LicenseNumber = Console.ReadLine();
                    this.checkLicenseInput();
                    checkUserInput = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void checkLicenseInput()
        {
            if (m_vehicleBuilder.m_LicenseNumber.Length != k_LicenseLength)
            {
                throw new ArgumentException($"License number should be {k_LicenseLength} digits!");
            }

            bool isLetterOrDigit = m_vehicleBuilder.m_LicenseNumber.All(Char.IsLetterOrDigit);

            if (isLetterOrDigit == false)
            {
                throw new ArgumentException("License number should be letters or digits!");
            }
        }

        private void dispalyVehicle()
        {
            StringBuilder sb = new StringBuilder("", 30);
            sb.AppendLine("Please choose an option:");
            sb.AppendLine("1) Display under repair vehicles.");
            sb.AppendLine("2) Display fixed vehicles.");
            sb.AppendLine("3) Display paid off vehicles.");
            sb.AppendLine("4) Display all Vehicles");

            Console.WriteLine(sb);
            m_NumOfOptions = 4;
            this.getUserChoose();

            eVehicleStates eVState = 0;
            switch (m_UserChoose)
            {
                case "1":
                    eVState = eVehicleStates.UnderRepair;
                    break;
                case "2":
                    eVState = eVehicleStates.Fixed;
                    break;
                case "3":
                    eVState = eVehicleStates.PaidUp;
                    break;
                case "4":
                    eVState = 0;
                    break;
            }

            List<string> vehicleLicenseNumbers = r_Garage.VehicleFilterByState(eVState);
            try
            {
                if (vehicleLicenseNumbers.Count == 0)
                {
                    Console.WriteLine("No vehicles");
                }
                else
                {
                    foreach (var license in vehicleLicenseNumbers)
                    {
                        Console.WriteLine($" - {license} - ");
                    }
                    Console.WriteLine("");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"{error.Message}");
            }
        }

        private void changeVehicleState()
        {
            this.getLicenseNum();

            StringBuilder sb = new StringBuilder("", 50);
            sb.AppendLine("Please choose a state:");
            sb.AppendLine("1) Under - repair.");
            sb.AppendLine("2) Fixed.");
            sb.AppendLine("3) Paid up.");

            Console.WriteLine(sb);
            m_NumOfOptions = 3;
            this.getUserChoose();

            eVehicleStates eVState = 0;
            switch (m_UserChoose)
            {
                case "1":
                    eVState = eVehicleStates.UnderRepair;
                    break;
                case "2":
                    eVState = eVehicleStates.Fixed;
                    break;
                case "3":
                    eVState = eVehicleStates.PaidUp;
                    break;
            }

            try
            {
                r_Garage.ChangeVehicleState(m_vehicleBuilder.m_LicenseNumber, eVState);
                Console.WriteLine("State changed.");
            }
            catch (Exception error)
            {
                Console.WriteLine($"{error.Message}");
            }
        }

        private void inflatingToMax()
        {
            this.getLicenseNum();

            try
            {
                r_Garage.InflatingToMax(m_vehicleBuilder.m_LicenseNumber);
                Console.WriteLine("Wheels fulled.");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void refulingVehicleGas()
        {
            this.getLicenseNum();

            StringBuilder sb = new StringBuilder("", 50);
            sb.AppendLine("Choose desired fuel type: ");
            sb.AppendLine("1) Octan95");
            sb.AppendLine("2) Octan96");
            sb.AppendLine("3) Octan98");
            sb.AppendLine("4) Soler");

            Console.WriteLine(sb);
            m_NumOfOptions = 4;
            this.getUserChoose();

            eFuelTypes eFType = 0;
            switch (m_UserChoose)
            {
                case "1":
                    eFType = eFuelTypes.Octan95;
                    break;
                case "2":
                    eFType = eFuelTypes.Octan96;
                    break;
                case "3":
                    eFType = eFuelTypes.Octan98;
                    break;
                case "4":
                    eFType = eFuelTypes.Soler;
                    break;
            }

            float amountOfGas = -1f;
            Console.WriteLine("Choose the amount of gas you want to add: ");
            while (!float.TryParse(Console.ReadLine(), out amountOfGas) || amountOfGas < 0f)
            {
                Console.WriteLine("Invalid input. Choose again:");
            }

            try
            {
                r_Garage.RefulingCarGas(m_vehicleBuilder.m_LicenseNumber, eFType, amountOfGas);
                Console.WriteLine("Tank Filled");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void chargeBattery()
        {
            this.getLicenseNum();

            float amountOfMinutes = -1f;
            Console.WriteLine("Choose the amount of minutes you want to add:");
            float.TryParse(Console.ReadLine(), out amountOfMinutes);

            while (amountOfMinutes <= 0)
            {
                Console.WriteLine("Invalid input. Choose again:");
                float.TryParse(Console.ReadLine(), out amountOfMinutes);
            }

            try
            {
                r_Garage.ChargeBattery(m_vehicleBuilder.m_LicenseNumber, amountOfMinutes);
                Console.WriteLine("Battery charged.");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void displayVehicleByLicense()
        {
            this.getLicenseNum();

            try
            {
                Console.WriteLine(r_Garage.DisplayVehicleInfo(m_vehicleBuilder.m_LicenseNumber));
            }
            catch (Exception vehicleNotExists)
            {
                Console.WriteLine(vehicleNotExists.Message);
            }
        }

        private eLicenseType chooseLicenseType()
        {
            StringBuilder sb = new StringBuilder("", 20);
            sb.AppendLine("Choose motorcycle license type:");
            sb.AppendLine("1) " + eLicenseType.A1.ToString());
            sb.AppendLine("2) " + eLicenseType.A2.ToString());
            sb.AppendLine("3) " + eLicenseType.AB.ToString());
            sb.AppendLine("4) " + eLicenseType.B2.ToString());

            Console.WriteLine(sb);
            m_NumOfOptions = 4;
            this.getUserChoose();

            eLicenseType eLicense_Type = 0;
            switch (m_UserChoose)
            {
                case "1":
                    eLicense_Type = eLicenseType.A1;
                    break;
                case "2":
                    eLicense_Type = eLicenseType.A2;
                    break;
                case "3":
                    eLicense_Type = eLicenseType.AB;
                    break;
                case "4":
                    eLicense_Type = eLicenseType.B2;
                    break;
            }
            return eLicense_Type;
        }

        private eDoors chooseNumOfDoors()
        {
            StringBuilder sb = new StringBuilder("", 20);
            sb.AppendLine("Choose number of doors:");
            sb.AppendLine("1) 2 doors");
            sb.AppendLine("2) 3 doors");
            sb.AppendLine("3) 4 doors");
            sb.AppendLine("4) 5 doors");

            Console.WriteLine(sb);
            m_NumOfOptions = 4;
            this.getUserChoose();

            eDoors eNumOfDoors = 0;
            switch (m_UserChoose)
            {
                case "1":
                    eNumOfDoors = eDoors.Two;
                    break;
                case "2":
                    eNumOfDoors = eDoors.Three;
                    break;
                case "3":
                    eNumOfDoors = eDoors.Four;
                    break;
                case "4":
                    eNumOfDoors = eDoors.Five;
                    break;
            }
            return eNumOfDoors;
        }

        private eColors chooseCarColor()
        {
            StringBuilder sb = new StringBuilder("", 20);
            sb.AppendLine("Choose car color:");
            sb.AppendLine("1) " + eColors.Blue.ToString());
            sb.AppendLine("2) " + eColors.White.ToString());
            sb.AppendLine("3) " + eColors.Red.ToString());
            sb.AppendLine("4) " + eColors.Yellow.ToString());

            Console.WriteLine(sb);
            m_NumOfOptions = 4;
            this.getUserChoose();

            eColors eColor = 0;
            switch (m_UserChoose)
            {
                case "1":
                    eColor = eColors.Blue;
                    break;
                case "2":
                    eColor = eColors.White;
                    break;
                case "3":
                    eColor = eColors.Red;
                    break;
                case "4":
                    eColor = eColors.Yellow;
                    break;
            }
            return eColor;
        }

        private bool isTransportChemicals()
        {
            StringBuilder sb = new StringBuilder("", 20);
            sb.AppendLine("Is the truck transporting hazardous materials?:");
            sb.AppendLine("1) Yes");
            sb.AppendLine("2) No");

            Console.WriteLine(sb);
            m_NumOfOptions = 2;
            this.getUserChoose();

            bool choose;
            choose = m_UserChoose == "1" ? true : false;

            return choose;
        }
        private float checkInfo()
        {
            string choose = "";
            float userChoose = 0;
            bool isValid = false;

            while (isValid != true)
            {
                try
                {
                    choose = Console.ReadLine();
                    if (float.TryParse(choose, out userChoose) == true)
                    {
                        isValid = true;
                    }
                    else { throw new FormatException("You must insert only numbers!"); }
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }
            return userChoose;
        }
    }
}

