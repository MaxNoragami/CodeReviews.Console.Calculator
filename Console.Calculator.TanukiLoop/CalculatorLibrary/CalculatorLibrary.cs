﻿using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    private JsonWriter _writer;
    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculator.log");
        logFile.AutoFlush = true;
        _writer = new JsonTextWriter(logFile);
        _writer.Formatting = Formatting.Indented;
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operations");
        _writer.WriteStartArray();
    }

    public virtual double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default is NAN if an operation, such as division could result in an error.
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteValue(num1);
        _writer.WritePropertyName("Operand2");
        _writer.WriteValue(num2);
        _writer.WritePropertyName("Operation");

        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                _writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                _writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                _writer.WriteValue("Multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor

                if (num2 != 0)
                {
                    result = num1 / num2;
                    _writer.WriteValue("Divide");
                }

                break;
            default:
                break;

        }

        _writer.WritePropertyName("Result");
        _writer.WriteValue(result);
        _writer.WriteEndObject();
        return result;
    }

    public void Finish()
    {
        _writer.WriteEndArray();
        _writer.WriteEndObject();
        _writer.Close();



    }
}
