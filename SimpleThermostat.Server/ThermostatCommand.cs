using System.Text.Json.Serialization;

[JsonDerivedType(typeof(SetThermostatModeCommand), "setMode")]
public class ThermostatCommand;

public class SetThermostatModeCommand : ThermostatCommand
{
    public SetThermostatModeCommand(ThermostatMode mode)
    {
        Mode = mode;
    }

    public ThermostatMode Mode { get; }
}

public enum ThermostatMode
{
    Off,
    Heat,
    Cool
}
