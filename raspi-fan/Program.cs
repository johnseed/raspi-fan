// See https://aka.ms/new-console-template for more information
using System.Device.Gpio;




int fanGPIO = 14;

using GpioController controller = new();
controller.OpenPin(fanGPIO);


while (true)
{
    var temp = GetTemperature();
    Console.WriteLine($"CPU Temperature : {temp}");
    var pinMode = controller.GetPinMode(fanGPIO);
    Console.WriteLine($"Current PinMode : {pinMode}");
    ControlFan(50, temp);
    await Task.Delay(1000 * 60);
}

//Console.WriteLine("Hello, World!");

static double GetTemperature()
{
    string result = CommandHelper.Execute("cat", "/sys/class/thermal/thermal_zone0/temp");
    double.TryParse(result, out double temperature);
    return temperature / 1000;
}

void ControlFan(double threshold, double temperature)
{
    PinMode pinMode = temperature >= threshold ? PinMode.Input : PinMode.Output;
    Console.WriteLine($"Setting pin mode to {pinMode}");
    controller.SetPinMode(fanGPIO, pinMode);
}
