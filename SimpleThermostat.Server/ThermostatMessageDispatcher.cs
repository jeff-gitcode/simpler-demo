using SimpleR;

public class ThermostatMessageDispatcher : IWebSocketMessageDispatcher<ThermostatMetric, ThermostatCommand>
{
    private float _targetTemp = 22;
    
    public Task OnConnectedAsync(IWebsocketConnectionContext<ThermostatCommand> connection)
    {
        return Task.CompletedTask;
    }

    public Task OnDisconnectedAsync(IWebsocketConnectionContext<ThermostatCommand> connection, Exception? exception)
    {
        return Task.CompletedTask;
    }

    public async Task DispatchMessageAsync(IWebsocketConnectionContext<ThermostatCommand> connection, ThermostatMetric message)
    {
        if(message is ThermostatTemperatureMetric temperatureMetric)
        {
            // update temperature
            
            if (temperatureMetric.Temperature < _targetTemp)
            {
                // If the temperature is below the target temperature, set the thermostat to heat mode
                await connection.WriteAsync(new SetThermostatModeCommand(ThermostatMode.Heat));
            }
            else if (temperatureMetric.Temperature > _targetTemp)
            {
                // If the temperature is above the target temperature, set the thermostat to cool mode
                await connection.WriteAsync(new SetThermostatModeCommand(ThermostatMode.Cool));
            }
            else
            {
                // If the temperature is at the target temperature, turn off the thermostat
                await connection.WriteAsync(new SetThermostatModeCommand(ThermostatMode.Off));
            }
        }
    }
}