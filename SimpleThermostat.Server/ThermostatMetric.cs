using System.Text.Json.Serialization;

[JsonDerivedType(typeof(ThermostatTemperatureMetric), "temperature")]

public class ThermostatMetric;

public class ThermostatTemperatureMetric : ThermostatMetric
{
    public ThermostatTemperatureMetric(float temperature)
    {
        Temperature = temperature;
    }

    public float Temperature { get; }
}
