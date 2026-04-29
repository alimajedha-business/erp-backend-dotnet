using NGErp.Base.Service.Services;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Services;

public static class UnitConversionEquationBuilder
{
    private static readonly HashSet<string> AllowedOperators = ["+", "-", "*", "/"];

    public static string? Build(UnitConversionEquationDto dto, int maxUnitOrder)
    {
        var operands = new[]
        {
        dto.Operand1,
        dto.Operand2,
        dto.Operand3,
        dto.Operand4
    };

        var operators = new[]
        {
        dto.Op1,
        dto.Op2,
        dto.Op3
    };

        if (operands.All(IsEmpty) && operators.All(string.IsNullOrWhiteSpace))
            return null;

        if (!IsEmpty(operands[0]) &&
            operands.Skip(1).All(IsEmpty) &&
            operators.All(string.IsNullOrWhiteSpace))
        {
            ValidateOperand(operands[0], 1, maxUnitOrder, required: true);

            return FormatOperand(operands[0]);
        }

        ValidateOperand(operands[0], 1, maxUnitOrder, required: true);
        ValidateOperand(operands[1], 2, maxUnitOrder, required: true);
        ValidateOperator(operators[0], 1, required: true);

        var equation = $"{FormatOperand(operands[0])} {operators[0]} {FormatOperand(operands[1])}";

        for (var i = 1; i < operators.Length; i++)
        {
            var op = operators[i];
            var operand = operands[i + 1];

            var hasOp = !string.IsNullOrWhiteSpace(op);
            var hasOperand = !IsEmpty(operand);

            if (hasOp != hasOperand)
                throw new ArgumentException($"op{i + 1} and operand{i + 2} must either both be provided or both be empty.");

            if (!hasOp)
                continue;

            ValidateOperator(op, i + 1, required: true);
            ValidateOperand(operand, i + 2, maxUnitOrder, required: true);

            equation = $"({equation}) {op} {FormatOperand(operand)}";
        }

        if (equation.Length > 100)
            throw new ArgumentException("Conversion equation is too long. Max length is 100 characters.");

        return equation;
    }

    private static void ValidateOperand(object? value, int index, int maxUnitOrder, bool required)
    {
        if (IsEmpty(value))
        {
            if (required)
                throw new ArgumentException($"operand{index} is required.");

            return;
        }

        var str = value!.ToString()!.Trim();

        if (RegexHelpers.UnitOfMeasurementRefRegex().IsMatch(str))
        {
            var unitOrder = int.Parse(str[2..]);

            if (unitOrder > maxUnitOrder)
                throw new ArgumentException($"operand{index} references {str}, but only @u1 to @u{maxUnitOrder} are allowed.");

            return;
        }

        if (!decimal.TryParse(str, out _))
            throw new ArgumentException($"operand{index} must be a number or unit reference like @u1.");
    }

    private static void ValidateOperator(string? op, int index, bool required)
    {
        if (string.IsNullOrWhiteSpace(op))
        {
            if (required)
                throw new ArgumentException($"op{index} is required.");

            return;
        }

        if (!AllowedOperators.Contains(op.Trim()))
            throw new ArgumentException($"op{index} must be one of: +, -, *, /.");
    }

    private static string FormatOperand(object? value)
    {
        return value!.ToString()!.Trim();
    }

    private static bool IsEmpty(object? value)
    {
        return value is null || string.IsNullOrWhiteSpace(value.ToString());
    }
}
